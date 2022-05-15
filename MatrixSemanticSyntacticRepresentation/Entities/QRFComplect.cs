using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities
{
    public class QRFComplect
    {
        private readonly string word;
        private readonly QuestionRoleFrame[] qrfs;

        public QRFComplect(string word, IEnumerable<QuestionRoleFrame> qrfs)
        {
            this.word = word;
            this.qrfs = qrfs.ToArray();
        }

        public string Unit { get => word; }
        public IEnumerable<QuestionRoleFrame> QRFs { get => qrfs; }
    }
}
