using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.DEPRICATED
{
    public class LexemeClass
    {
        private readonly string lexeme;
        private readonly string partOfSpeech;
        private readonly string subClass;


        public LexemeClass(string lexeme, string partOfSpeech, string subClass)
        {
            this.lexeme = lexeme;
            this.partOfSpeech = partOfSpeech;
            this.subClass = subClass;
        }

        public string Lexeme { get => lexeme; }
        public string PartOfSpeech { get => partOfSpeech; }
        public string SubClass { get => subClass; }

        public override string ToString()
        {
            return $"{Lexeme} | {PartOfSpeech}, {SubClass}";
        }
    }
}
