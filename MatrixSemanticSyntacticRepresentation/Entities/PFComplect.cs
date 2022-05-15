using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities
{
    public class PFComplect
    {
        private readonly string word;
        private readonly PrepositionFrame[] pfs;

        public PFComplect(string word, IEnumerable<PrepositionFrame> pfs)
        {
            this.word = word;
            this.pfs = pfs.ToArray();
        }

        public string Unit { get => word; }
        public IEnumerable<PrepositionFrame> PFs { get => pfs; }
    }
}
