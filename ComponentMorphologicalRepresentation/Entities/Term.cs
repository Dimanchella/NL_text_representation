using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.ComponentMorphologicalRepresentation.Entities
{
    public class Term
    {
        private readonly List<TermComponent> components;
        private readonly string partOfSpeech;
        private readonly string subClass;

        public Term(IEnumerable<TermComponent> components, string partOfSpeech, string subClass)
        {
            this.components = components.ToList();
            this.partOfSpeech = partOfSpeech;
            this.subClass = subClass;
        }

        public IEnumerable<TermComponent> Components { get => components; }
        public string MainLexeme { get => components.Where(lexeme => lexeme.IsMain).First().Lexeme;  }
        public string PartOfSpeech { get => partOfSpeech; }
        public string SubClass { get => subClass; }

        public override string ToString()
        {
            StringBuilder sb = new();
            components.ForEach(lexeme => sb.Append($"{lexeme.Lexeme} "));
            return $"{sb}| {partOfSpeech}, {subClass}";
        }
    }
}
