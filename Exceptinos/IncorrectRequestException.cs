using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nli_to_lod.Exceptinos
{
    public class IncorrectRequestException : Exception
    {
        public IncorrectRequestException(string msg) : base(msg) { }
    }
}
