using NL_text_representation.ComponentMorphologicalRepresentation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.DEPRICATED
{
    public class WordCMR
    {
        private readonly string lexeme;
        private readonly LexemeClass @class;
        private readonly MorphologicalForm morph;

        public WordCMR(string lexeme, LexemeClass @class, MorphologicalForm morph)
        {
            this.lexeme = lexeme;
            this.@class = @class;
            this.morph = morph;
        }

        public string Unit { get => morph.Word; }
        public string Lexeme { get => lexeme; }
        public LexemeClass Class { get => @class; }
        public MorphologicalForm Morph { get => morph; }

        public bool HasClass { get => @class != null; }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.Append($"{Unit} | {Lexeme} |");
            if (HasClass)
            {
                sb.Append($" {Class.PartOfSpeech} : {Class.SubClass} |");
            }
            else
            {
                sb.Append($" ? :  |");
            }
            morph.Traits.Grams.ToList().ForEach(gram => sb.Append($" {gram}"));
            return sb.ToString();
        }
    }
}
