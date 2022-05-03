using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.ComponentMorphologicalRepresentation.Entities
{
    public class Token
    {
        private readonly string name;
        private readonly string lexeme;

        public Token(string name, string lexeme)
        {
            this.name = name;
            this.lexeme = lexeme;
        }

        public string Name { get => name; }
        public string Lexeme { get => lexeme; }
        public bool IsEOS { get => name.Equals("eos"); }

        public override string ToString() { return $"({name} : {lexeme})"; }
    }
}
