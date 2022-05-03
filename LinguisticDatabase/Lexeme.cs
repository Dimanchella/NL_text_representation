using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class Lexeme
    {
        public Lexeme()
        {
            TermComponents = new HashSet<TermComponent>();
            WordForms = new HashSet<WordForm>();
        }

        public long IdLexeme { get; set; }
        public string Lexeme1 { get; set; }

        public virtual ICollection<TermComponent> TermComponents { get; set; }
        public virtual ICollection<WordForm> WordForms { get; set; }
    }
}
