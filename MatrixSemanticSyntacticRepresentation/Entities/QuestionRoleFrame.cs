using NL_text_representation.ComponentMorphologicalRepresentation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities
{
    public class QuestionRoleFrame : MeaningUnit
    {
        private readonly Term prepositionTerm;
        private readonly Term pronounTerm;

        public QuestionRoleFrame(
            ComponentMorphologicalUnit cmu,
            Term prepositionTerm,
            Term pronounTerm,
            string meaning
            ) : base(cmu, meaning)
        {
            this.prepositionTerm = prepositionTerm;
            this.pronounTerm = pronounTerm;
        }

        public Term PrepositionTerm { get => prepositionTerm; }
        public Term PronounTerm { get => pronounTerm; }

        public override string ToString()
        {
            return $"{cmu}\n"
                + $"({prepositionTerm.AllLexemes}, "
                + $"{pronounTerm}) {meaning}";
        }
    }
}
