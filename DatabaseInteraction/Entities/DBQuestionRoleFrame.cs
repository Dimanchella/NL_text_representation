using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.DatabaseInteraction.Entities
{
    public class DBQuestionRoleFrame
    {
        public long ID { get; set; }
        public long PrepositionTermID { get; set; }
        public long PronounTermID { get; set; }
        public string Meaning { get; set; }
    }
}
