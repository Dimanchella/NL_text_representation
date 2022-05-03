using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DeepMorphy;
using DeepMorphy.Model;
using NL_text_representation.DatabaseRequester;
using NL_text_representation.ComponentMorphologicalRepresentation;
using NL_text_representation.ComponentMorphologicalRepresentation.Entities;

namespace NL_text_representation.DEPRICATED
{
    public class LexemMatcher
    {
        private static readonly List<LexemeClass> dbLexemes = MorphologicalDBRequester.GetLexemes().ToList();
        private static readonly MorphAnalyzer ma = new(withLemmatization: true);
        private static readonly Dictionary<string, string> posTranslater = CreatePOSTranslater();

        private readonly Lexer lexer = new();
        private string text = "";

        public string Text { set => text = value; }

        private static Dictionary<string, string> CreatePOSTranslater()
        {
            Dictionary<string, string> posTranslater = new();
            posTranslater.Add("сущ", "сущ");
            posTranslater.Add("прил", "прилаг");
            posTranslater.Add("кр_прил", "прилаг");
            posTranslater.Add("гл", "глагол");
            posTranslater.Add("инф_гл", "глагол");
            posTranslater.Add("комп", "прилаг");
            posTranslater.Add("прич", "глагол");
            posTranslater.Add("кр_прич", "прич");
            posTranslater.Add("деепр", "глагол");
            posTranslater.Add("нареч", "наречие");
            posTranslater.Add("мест", "местоим");
            posTranslater.Add("предл", "предлог");
            posTranslater.Add("союз", "союз");
            posTranslater.Add("част", "част");
            posTranslater.Add("межд", "?");
            posTranslater.Add("числ", "числит");
            posTranslater.Add("предик", "?");
            posTranslater.Add("пункт", "?");
            posTranslater.Add("цифра", "числит");
            posTranslater.Add("рим_цифр", "числит");
            posTranslater.Add("неизв", "?");
            return posTranslater;
        }


        public IEnumerable<WordRepList> GetUsedLexemes()
        {
            List<WordRepList> wordReps = new();
            
            lexer.ResearchedString = text;
            lexer.FindAllTokens();

            foreach (var token in lexer.AllTokens)
            {
                if (!token.Name.Equals("eos") && !token.Name.Equals("punctuation"))
                {
                    List<WordCMR> cmrs = new();
                    foreach (var wordForm in GetDeepMorphRep(token))
                    {
                        cmrs.AddRange(GetCMR(wordForm));
                    }
                    wordReps.Add(new(token.Lexeme, cmrs));
                }
            }

            return wordReps;
        }


        private IEnumerable<MorphologicalForm> GetDeepMorphRep(Token token)
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

        private IEnumerable<WordCMR> GetCMR(MorphologicalForm wordForm)
        {
            List<WordCMR> cmrs = new();
            if (wordForm.Token.Name.Equals("number"))
            {
                dbLexemes
                    .Where(lexemeClass => lexemeClass.Lexeme.Equals("#число#")).ToList()
                    .ForEach(lexemeClass => cmrs.Add(new(wordForm.Lemma, lexemeClass, wordForm)));
            }
            else if (wordForm.Token.Name.Equals("date"))
            {
                dbLexemes
                    .Where(lexemeClass => lexemeClass.Lexeme.Equals("#дата#")).ToList()
                    .ForEach(lexemeClass => cmrs.Add(new(wordForm.Lemma, lexemeClass, wordForm)));
            }
            else if (wordForm.Token.Name.Equals("time"))
            {
                dbLexemes
                    .Where(lexemeClass => lexemeClass.Lexeme.Equals("#время#")).ToList()
                    .ForEach(lexemeClass => cmrs.Add(new(wordForm.Lemma, lexemeClass, wordForm)));
            }
            else
            {
                bool isFound = false;
                dbLexemes
                    .Where(lexemeClass => lexemeClass.Lexeme.ToLower().Equals(wordForm.Lemma.ToLower())).ToList()
                    .ForEach(lexemeClass =>
                    {
                        cmrs.Add(new(lexemeClass.Lexeme, lexemeClass, wordForm));
                        isFound = true;
                    });
                if (!isFound)
                {
                    cmrs.Add(new(wordForm.Lemma, null, wordForm));
                }
            }
            return cmrs;
        }




        private IEnumerable<IEnumerable<WordCMR>> SelectSimilarClasses(IEnumerable<IEnumerable<WordCMR>> cmrsOfLexemes)
        {
            List<List<WordCMR>> newCMRsOfLexemes = new();
            foreach (var cmrs in cmrsOfLexemes)
            {
                List<WordCMR> newCMRs = new();
                foreach (var cmr in cmrs)
                {
                    if (cmr.HasClass)
                    {
                        if (posTranslater[cmr.Morph.Traits.GramsDic["чр"]].Equals(cmr.Class.PartOfSpeech)
                        || posTranslater[cmr.Morph.Traits.GramsDic["чр"]].Equals("числит"))
                        {
                            newCMRs.Add(cmr);
                        }
                    }
                    else
                    {
                        newCMRs.Add(cmr);
                    }
                }
                newCMRsOfLexemes.Add(newCMRs);
            }
            return newCMRsOfLexemes;
        }


        private static double FindProportionOfMatches(string str1, string str2)
        {
            int maxLen = Math.Max(str1.Length, str2.Length);
            return ((double) maxLen - DamerauLevensteinDistance(str1, str2)) / maxLen;
        }

        private static int DamerauLevensteinDistance(string str1, string str2)
        {
            int[,] d = new int[str1.Length + 1, str2.Length + 1];

            d[0, 0] = 0;

            for (int i = 1; i < str1.Length + 1; i++)
            {
                d[i, 0] = d[i - 1, 0] + 1;
            }

            for (int j = 1; j < str2.Length + 1; j++)
            {
                d[0, j] = d[0, j - 1] + 1;
            }

            for (int i = 1; i < str1.Length + 1; i++)
            {
                for (int j = 1; j < str2.Length + 1; j++)
                {
                    d[i, j] = Math.Min(d[i - 1, j], Math.Min(d[i, j - 1], d[i - 1, j - 1]));
                    if (str1[i - 1] != str2[j - 1])
                    {
                        d[i, j]++;
                    }
                }
            }

            return d[str1.Length, str2.Length];
        }
    }
}
