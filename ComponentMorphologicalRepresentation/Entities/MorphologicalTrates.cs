using DeepMorphy.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NL_text_representation.ComponentMorphologicalRepresentation.Entities
{
    public class MorphologicalTrates
    {
        private readonly Tag tag;
        private readonly ReadOnlyDictionary<string, string> extraGrams;

        public MorphologicalTrates(Tag tag, Token token)
        {
            this.tag = tag;
            extraGrams = CreateExtraGrams(token);
        }

        private ReadOnlyDictionary<string, string> CreateExtraGrams(Token token)
        {
            Dictionary<string, string> extraGrams = new();
            if (tag["чр"].Equals("гл") || tag["чр"].Equals("инф_гл") || tag["чр"].Equals("прич"))
            {
                extraGrams.Add("возв", FindReflection(token));
            }
            return new ReadOnlyDictionary<string, string>(extraGrams);
        }

        private string FindReflection(Token token)
        {
            string reflection;
            if (token.Lexeme.Substring(token.Lexeme.Length - 2, 2).Equals("сь")
                || token.Lexeme.Substring(token.Lexeme.Length - 2, 2).Equals("ся"))
            {
                reflection = "вз";
            }
            else
            {
                reflection = "нвз";
            }
            return reflection;
        }

        public string this[string gramCatKey]
        {
            get 
            {
                if (extraGrams.ContainsKey(gramCatKey))
                {
                    return extraGrams[gramCatKey];
                }
                else
                {
                    return tag[gramCatKey];
                }
            }
        }
        public Tag Tag { get => tag; }
        public IEnumerable<string> Grams
        {
            get
            {
                List<string> grams = new();
                grams.AddRange(tag.Grams);
                grams.AddRange(extraGrams.Values);
                return grams;
            }
        }
    }
}
