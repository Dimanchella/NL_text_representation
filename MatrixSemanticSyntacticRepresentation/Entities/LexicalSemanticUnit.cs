using NL_text_representation.ComponentMorphologicalRepresentation.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities
{
    public class LexicalSemanticUnit
    {
        private readonly ComponentMorphologicalUnit cmu;
        private readonly string mainMeaning;
        private readonly List<string> addMeanings;

        public LexicalSemanticUnit(ComponentMorphologicalUnit cmu, string mainMeaning, IEnumerable<string> addMeanings)
        {
            this.cmu = cmu;
            this.mainMeaning = mainMeaning;
            this.addMeanings = addMeanings.ToList();
        }

        public ComponentMorphologicalUnit CMU { get => cmu; }
        public string MainMeaning { get => mainMeaning; }
        public IEnumerable<string> AddMeanings { get => addMeanings; }

        public override string ToString() 
        {
            StringBuilder sb = new();
            sb.Append($"{cmu} ^ {mainMeaning}");
            if (addMeanings.Count > 0)
            {
                sb.Append(" |");
                addMeanings.ForEach(meaning => sb.Append($" {meaning}"));
            }
            return sb.ToString(); 
        }

        public static IEnumerable<string> MakeAddMeaningsArray(params string[] addMeanings)
        {
            return addMeanings;
        }
        public static IEnumerable<string> MakeAddMeaningsArray(IEnumerable<string> addMeaningsArr, params string[] addMeanings)
        {
            string[] newAMArr = new string[addMeaningsArr.ToArray().Length + addMeanings.Length];
            for (int i = 0; i < addMeaningsArr.ToArray().Length || i < addMeanings.Length; i++)
            {
                if (i < addMeaningsArr.ToArray().Length)
                    newAMArr[i] = addMeaningsArr.ToArray()[i];
                if (i < addMeanings.Length)
                    newAMArr[i + addMeaningsArr.ToArray().Length] = addMeanings[i];
            }
            return addMeanings;
        }
    }
}
