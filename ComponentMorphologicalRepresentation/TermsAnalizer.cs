using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DeepMorphy;
using NL_text_representation.DatabaseRequester;
using NL_text_representation.ComponentMorphologicalRepresentation.Entities;

namespace NL_text_representation.ComponentMorphologicalRepresentation
{
    public class TermsAnalizer
    {
        private static readonly MorphAnalyzer ma = new(withLemmatization: true);
        private static readonly Dictionary<string, string> tokenNamesToLexemes = LinkTokenNamesToLexemes();

        private readonly Lexer lexer = new();
        private int lastPos = 0;
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

        public IEnumerable<VariableCMR> GetCMR()
        {
            lexer.ResearchedString = text;
            lexer.FindAllTokens();
            tokens = lexer.AllTokens.ToList();
            return GetVariableCMR();
        }

        private IEnumerable<WordForm> GetDeepMorphyRep(Token token)
        {
            var morphsInfo = ma.Parse(token.Lexeme).ToArray();
            List<WordForm> wordForms = new();
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

        private IEnumerable<VariableCMR> GetVariableCMR()
        {
            List<VariableCMR> variableCMRs = new();
            for (int i = 0; i < tokens.Count && !tokens[i].IsEOS; i++)
            {
                List<WordTerm> wordTerms = new();
                foreach (var wordForm in GetDeepMorphyRep(tokens[i]))
                {
                    List<Term> terms;
                    if (tokenNamesToLexemes.ContainsKey(tokens[i].Name))
                    {
                        terms = MorphologicalDBRequester.GetTermsOnLexeme(tokenNamesToLexemes[tokens[i].Name])
                            .OrderByDescending(term => term.Components.Count()).ToList();
                    }
                    else
                    {
                        terms = MorphologicalDBRequester.GetTermsOnLexeme(wordForm.Lemma)
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

        private List<WordTerm> EnumerateConponents(WordForm firstForm, Term term, int indexToken)
        {
            List<WordTerm> wordTerms = new();
            List<List<WordForm>> combinationsWordForms = new();
            List<WordForm> firstComb = new List<WordForm>();
            firstComb.Add(firstForm);
            combinationsWordForms.Add(firstComb);
            for (int i = 1; i < term.Components.Count(); i++)
            {
                List<WordForm> varForms;
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
                    List<List<WordForm>> newCombinations = new();
                    foreach(var varForm in varForms)
                    {
                        foreach(var combination in combinationsWordForms)
                        {
                            combination.Add(varForm);
                            newCombinations.Add(combination);
                        }
                    }
                    combinationsWordForms = newCombinations;
                }
            }
            combinationsWordForms.ForEach(combination => wordTerms.Add(new(term, combination)));
            return wordTerms;
        }

        private List<WordForm> CompareComponentWithToken(TermComponent component, Token token, bool firstMatch)
        {
            List<WordForm> forms = new();
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
