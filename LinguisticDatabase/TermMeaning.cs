using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class TermMeaning
    {
        public long? IdTerm { get; set; }
        public string Lexeme { get; set; }
        public long? IdMeaningMain { get; set; }
        public string Meaning { get; set; }
    }
}
