using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.DatabaseInteraction.Entities
{
    public class DBAddMeaning
    {
        public long ID { get; set; }
        public long MainMeaningID { get; set; }
        public string Meaning { get; set; }
    }
}
