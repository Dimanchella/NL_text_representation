using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class TermComponent
    {
        public long IdComponent { get; set; }
        public long IdTerm { get; set; }
        public long IdLexeme { get; set; }
        public bool? IsMainLexeme { get; set; }
        public long PositionLexeme { get; set; }

        public virtual Lexeme IdLexemeNavigation { get; set; }
        public virtual Term IdTermNavigation { get; set; }
    }
}
