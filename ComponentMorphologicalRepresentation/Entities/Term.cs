using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NL_text_representation.ComponentMorphologicalRepresentation.Entities
{
    public class Term
    {
        private readonly long id;
        private readonly List<TermComponent> components;
        private readonly string partOfSpeech;
        private readonly string subClass;

        public Term(long id, IEnumerable<TermComponent> components, string partOfSpeech, string subClass)
        {
            this.id = id;
            this.components = components.ToList();
            this.partOfSpeech = partOfSpeech;
            this.subClass = subClass;
        }

        public long ID { get => id; }
        public IEnumerable<TermComponent> Components { get => components; }
        public string MainLexeme { get => components.Where(lexeme => lexeme.IsMain).First().Lexeme; }
        public string AllLexemes 
        { 
            get
            {
                StringBuilder sb = new();
                components.ForEach(lexeme => sb.Append($"{lexeme.Lexeme} "));
                return sb.ToString().Trim();
            }
        }
        public string PartOfSpeech { get => partOfSpeech; }
        public string SubClass { get => subClass; }

        public override string ToString()
        {
            return $"{AllLexemes} | {partOfSpeech}, {subClass}";
        }
    }
}
