using NL_text_representation.ComponentMorphologicalRepresentation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities
{
    public abstract class MeaningUnit
    {
        private static readonly Dictionary<string, string> tokenNamesToLexemes = LinkTokenNamesToLexemes();
        
        protected readonly ComponentMorphologicalUnit cmu;
        protected readonly string meaning;

        protected MeaningUnit(ComponentMorphologicalUnit cmu, string meaning)
        {
            this.cmu = cmu;
            this.meaning = CreateMeaning(cmu, meaning);
        }

        private static Dictionary<string, string> LinkTokenNamesToLexemes()
        {
            Dictionary<string, string> tokenNamesToLexemes = new();
            tokenNamesToLexemes.Add("number", "#число#");
            tokenNamesToLexemes.Add("date", "#дата#");
            tokenNamesToLexemes.Add("time", "#время#");
            return tokenNamesToLexemes;
        }
        private static string CreateMeaning(ComponentMorphologicalUnit cmu, string meaning)
        {
            foreach (var token in cmu.Tokens)
            {
                if (tokenNamesToLexemes.ContainsKey(token.Name))
                {
                    Regex regex = new Regex(tokenNamesToLexemes[token.Name]);
                    meaning = regex.Replace(meaning, token.Lexeme, 1);
                }
            }
            return meaning;
        }

        public ComponentMorphologicalUnit CMU { get => cmu; }
        public string Meaning { get => meaning; }
    }
}
