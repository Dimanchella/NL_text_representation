using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.DatabaseRequester.Entities
{
    public class DBTerms
    {
        public long ID { get; set; }
        public string PartOfSpeech { get; set; }
        public string SubClass { get; set; }
    }
}
