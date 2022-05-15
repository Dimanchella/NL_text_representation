using NL_text_representation.ComponentMorphologicalRepresentation.Entities;

namespace NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities
{
    public class PrepositionFrame : MeaningUnit
    {
        private readonly Term prepositionTerm;
        private readonly string noun1AddMeaning;
        private readonly string noun2AddMeaning;
        private readonly string noun2Case;

        public PrepositionFrame(
            ComponentMorphologicalUnit cmu,
            Term prepositionTerm,
            string noun1AddMeaning,
            string noun2AddMeaning,
            string noun2Case,
            string meaning
            ) : base(cmu, meaning)
        {
            this.prepositionTerm = prepositionTerm;
            this.noun1AddMeaning = noun1AddMeaning;
            this.noun2AddMeaning = noun2AddMeaning;
            this.noun2Case = noun2Case;
        }

        public Term PrepositionTerm { get => prepositionTerm; }
        public string Noun1AddMeaning { get => noun1AddMeaning; }
        public string Noun2AddMeaning { get => noun2AddMeaning; }
        public string Noun2Case { get => noun2Case; }

        public override string ToString()
        {
            return $"{cmu}\n    ({noun1AddMeaning}, "
                + $"{prepositionTerm.AllLexemes}, "
                + $"{noun2AddMeaning}, "
                + $"{noun2Case}) {meaning}";
        }
    }
}
