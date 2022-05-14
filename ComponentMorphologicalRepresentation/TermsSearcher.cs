using System.Collections.Generic;
using System.Linq;

using DeepMorphy;
using NL_text_representation.ComponentMorphologicalRepresentation.Entities;
using NL_text_representation.DatabaseInteraction;

namespace NL_text_representation.ComponentMorphologicalRepresentation
{
    public class TermsSearcher
    {
        private static readonly MorphAnalyzer ma = new(withLemmatization: true);
        private static readonly Dictionary<string, string> tokenNamesToLexemes = LinkTokenNamesToLexemes();

        private readonly Lexer lexer = new();
        private string text = "";

        private List<Token> tokens;

        private static Dictionary<string, string> LinkTokenNamesToLexemes()
        {
            Dictionary<string, string> tokenNamesToLexemes = new();
            tokenNamesToLexemes.Add("number", "#число#");
            tokenNamesToLexemes.Add("date", "#дата#");
            tokenNamesToLexemes.Add("time", "#время#");
            return tokenNamesToLexemes;
        }

        public string Text { set => text = value; }

        public IEnumerable<ComplectCMR> GetCMR()
        {
            lexer.ResearchedString = text;
            lexer.FindAllTokens();
            tokens = lexer.AllTokens.ToList();
            return GetVariableCMR();
        }

        private IEnumerable<MorphologicalForm> GetDeepMorphyRep(Token token)
        {
            var morphsInfo = ma.Parse(token.Lexeme).ToArray();
            List<MorphologicalForm> wordForms = new();
            foreach (var morph in morphsInfo)
            {
                if (token.Name.Equals("word"))
                {
                    morph.Tags.ToList()
                        .ForEach(tag => wordForms.Add(new(token, tag)));
                }
                else if (token.Name.Equals("number") || token.Name.Equals("date") || token.Name.Equals("time"))
                {
                    var tag = ma.TagHelper.CreateTag(post: "цифра");
                    wordForms.Add(new(token, tag));
                }
                else if (token.Name.Equals("alias"))
                {
                    var tag = ma.TagHelper.CreateTag(post: "сущ", gndr: "муж", nmbr: "ед", @case: "им");
                    wordForms.Add(new(token, tag));
                }
                else
                {
                    var tag = ma.TagHelper.CreateTag(post: "неизв");
                    wordForms.Add(new(token, tag));
                }
            }
            return wordForms;
        }

        private IEnumerable<ComplectCMR> GetVariableCMR()
        {
            List<ComplectCMR> variableCMRs = new();
            for (int i = 0; i < tokens.Count && !tokens[i].IsEOS; i++)
            {
                List<ComponentMorphologicalUnit> wordTerms = new();
                foreach (var wordForm in GetDeepMorphyRep(tokens[i]))
                {
                    List<Term> terms;
                    if (tokenNamesToLexemes.ContainsKey(tokens[i].Name))
                    {
                        terms = DatabaseRequester.GetTermsOnLexeme(tokenNamesToLexemes[tokens[i].Name])
                            .OrderByDescending(term => term.Components.Count()).ToList();
                    }
                    else
                    {
                        terms = DatabaseRequester.GetTermsOnLexeme(wordForm.Lemma)
                            .OrderByDescending(term => term.Components.Count()).ToList();
                    }

                    if (terms.Count > 0)
                    {
                        int maxComponents = terms[0].Components.Count();
                        for (int j = 0; j < terms.Count && terms[j].Components.Count() >= maxComponents; j++)
                        {
                            var tempWT = EnumerateConponents(wordForm, terms[j], i);
                            if (tempWT.Count == 0 && j + 1 < terms.Count)
                            {
                                maxComponents = terms[j + 1].Components.Count();
                            }
                            else
                            {
                                wordTerms.AddRange(tempWT);
                                i += maxComponents - 1;
                            }
                        }
                    }
                }
                int maxLength = 0;
                foreach (var wt in wordTerms)
                {
                    if (maxLength < wt.Length)
                    {
                        maxLength = wt.Length;
                    }
                }
                variableCMRs.Add(new(tokens[i].Lexeme, wordTerms.Where(wt => wt.Length == maxLength)));
            }
            return variableCMRs;
        }

        private List<ComponentMorphologicalUnit> EnumerateConponents(MorphologicalForm firstForm, Term term, int indexToken)
        {
            List<ComponentMorphologicalUnit> wordTerms = new();
            List<List<MorphologicalForm>> combinationsWordForms = new();
            List<MorphologicalForm> firstComb = new List<MorphologicalForm>();
            firstComb.Add(firstForm);
            combinationsWordForms.Add(firstComb);
            for (int i = 1; i < term.Components.Count(); i++)
            {
                List<MorphologicalForm> varForms;
                varForms = CompareComponentWithToken(
                    term.Components.ToArray()[i],
                    tokens[indexToken + i],
                    !term.Components.ToArray()[i].IsMain
                    );
                if (varForms.Count == 0)
                {
                    return wordTerms;
                }
                else
                {
                    List<List<MorphologicalForm>> newCombinations = new();
                    foreach(var varForm in varForms)
                    {
                        foreach(var combination in combinationsWordForms)
                        {
                            List<MorphologicalForm> tempComb = new();
                            tempComb.AddRange(combination);
                            tempComb.Add(varForm);
                            newCombinations.Add(tempComb);
                        }
                    }
                    combinationsWordForms = newCombinations;
                }
            }
            combinationsWordForms.ForEach(combination => wordTerms.Add(new(term, combination)));
            return wordTerms;
        }

        private List<MorphologicalForm> CompareComponentWithToken(TermComponent component, Token token, bool firstMatch)
        {
            List<MorphologicalForm> forms = new();
            if (!token.IsEOS)
            {
                var tempForms = GetDeepMorphyRep(token)
                    .Where(form => form.Lemma.Equals(component.Lexeme.ToLower()))
                    .ToList();
                if (firstMatch)
                {
                    if (tempForms.Count > 0)
                    {
                        forms.Add(tempForms.First());
                    }
                }
                else
                {
                    forms = tempForms;
                }
            }
            return forms;
        }
    }
}
