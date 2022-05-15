using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NL_text_representation.ComponentMorphologicalRepresentation.Entities;

namespace NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities
{
    public class Concept
    {
        private readonly LexicalSemanticUnit nounLSU;
        private readonly List<LexicalSemanticUnit> adjectiveLSUs;
        private readonly string meaning;

        public Concept(LexicalSemanticUnit nounLSU, IEnumerable<LexicalSemanticUnit> adjectiveLSUs)
        {
            this.nounLSU = nounLSU;
            this.adjectiveLSUs = adjectiveLSUs.ToList();
            this.meaning = ConstructMeaning();
        }

        private string ConstructMeaning()
        {
            StringBuilder sb = new();
            sb.Append(nounLSU.Meaning);
            if (adjectiveLSUs.Count > 0)
            {
                sb.Append(" * ");
                adjectiveLSUs.ForEach(adjective => sb.Append(adjective.Meaning));
            }
            return sb.ToString();
        }

        public LexicalSemanticUnit NounLSU { get => nounLSU; }
        public IEnumerable<LexicalSemanticUnit> AdjectiveLSUs { get => adjectiveLSUs; }
        public string Meaning { get => meaning; }
    }
}
