using NL_text_representation.ComponentMorphologicalRepresentation.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities
{
    public class LexicalSemanticUnit : MeaningUnit
    {
        private readonly List<string> addMeanings;

        public LexicalSemanticUnit(ComponentMorphologicalUnit cmu, string meaning, IEnumerable<string> addMeanings) : base(cmu, meaning)
        {
            this.addMeanings = addMeanings.ToList();
        }

        public IEnumerable<string> AddMeanings { get => addMeanings; }

        public override string ToString() 
        {
            StringBuilder sb = new();
            sb.Append($"{cmu}\n    {meaning}");
            if (addMeanings.Count > 0)
            {
                sb.Append(" |");
                addMeanings.ForEach(meaning => sb.Append($" {meaning}"));
            }
            return sb.ToString(); 
        }
    }
}
