using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities
{
    public class VPFComplect
    {
        private readonly string word;
        private readonly VerbPrepositionFrame[] vpfs;

        public VPFComplect(string word, IEnumerable<VerbPrepositionFrame> vpfs)
        {
            this.word = word;
            this.vpfs = vpfs.ToArray();
        }

        public string Unit { get => word; }
        public IEnumerable<VerbPrepositionFrame> VPFs { get => vpfs; }
    }
}
