using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities
{
    public class LSUComplect
    {
        private readonly string word;
        private readonly LexicalSemanticUnit[] lsus;

        public LSUComplect(string word, IEnumerable<LexicalSemanticUnit> lsus)
        {
            this.word = word;
            this.lsus = lsus.ToArray();
        }

        public string Unit { get => word; }
        public IEnumerable<LexicalSemanticUnit> LSUs { get => lsus; }
    }
}
