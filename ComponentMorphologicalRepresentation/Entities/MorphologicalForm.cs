using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DeepMorphy.Model;

namespace NL_text_representation.ComponentMorphologicalRepresentation.Entities
{
    public class MorphologicalForm
    {
        private readonly Token token;
        private readonly Tag traits;

        public MorphologicalForm(Token token, Tag tag)
        {
            this.token = token;
            traits = tag;
        }

        public string Word { get => token.Lexeme; }
        public string Lemma
        {
            get
            {
                if (traits.HasLemma) return traits.Lemma;
                else return token.Lexeme;
            }
        }
        public Token Token { get => token; }
        public Tag Traits { get => traits; }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append($"{Word} | {Lemma} :");
            Traits.Grams.ToList().ForEach(gram => sb.Append($" {gram}"));
            return sb.ToString();
        }
    }
}
