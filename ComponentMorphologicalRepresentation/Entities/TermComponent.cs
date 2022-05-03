using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.ComponentMorphologicalRepresentation.Entities
{
    public class TermComponent
    {
        private readonly bool isMain;
        private readonly string lexeme;

        public TermComponent(string lexeme, bool isMain)
        {
            this.lexeme = lexeme;
            this.isMain = isMain;
        }

        public bool IsMain { get => isMain; }
        public string Lexeme { get => lexeme; }
    }
}
