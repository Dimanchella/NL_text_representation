using DeepMorphy.Model;
using System.Linq;
using System.Text;

namespace NL_text_representation.ComponentMorphologicalRepresentation.Entities
{
    public class MorphologicalForm
    {
        private readonly Token token;
        private readonly MorphologicalTrates traits;

        public MorphologicalForm(Token token, Tag tag)
        {
            this.token = token;
            traits = new MorphologicalTrates(tag, token);
        }



        public string Word { get => token.Lexeme; }
        public string Lemma
        {
            get
            {
                if (traits.Tag.HasLemma) return traits.Tag.Lemma;
                else return token.Lexeme;
            }
        }
        public Token Token { get => token; }
        public MorphologicalTrates Traits { get => traits; }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append($"{Word} | {Lemma} :");
            Traits.Grams.ToList().ForEach(gram => sb.Append($" {gram}"));
            return sb.ToString();
        }
    }
}
