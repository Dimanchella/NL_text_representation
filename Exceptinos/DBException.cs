using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nli_to_lod.Exceptinos
{
    public class DBException: Exception
    {
        public DBException(string msg) : base(msg) { }
    }
}
