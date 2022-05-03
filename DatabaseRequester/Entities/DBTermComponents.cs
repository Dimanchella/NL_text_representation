using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.DatabaseRequester.Entities
{
    public class DBTermComponents
    {
        public long ID { get; set; }
        public string Lexeme { get; set; }
        public bool IsMain { get; set; }
        public long Position { get; set; }
    }
}
