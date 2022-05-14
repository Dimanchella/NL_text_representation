﻿using NL_text_representation.ComponentMorphologicalRepresentation.Entities;
using System.Collections.Generic;
using System.Linq;

namespace NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities
{
    public class VerbPrepositionFrame
    {
        private readonly ComponentMorphologicalUnit cmu;
        private readonly string verbMeaning;
        private readonly string verbForm;
        private readonly string verbReflection;
        private readonly string verbVoice;
        private readonly Term prepositionTerm;
        private readonly List<string> nounAddMeanings;
        private readonly string nounCase;
        private readonly string meaning;

        public VerbPrepositionFrame(
            ComponentMorphologicalUnit cmu,
            string verbMeaning,
            string verbForm,
            string verbReflection,
            string verbVoice,
            Term prepositionTerm,
            IEnumerable<string> nounAddMeanings,
            string nounCase,
            string meaning
            )
        {
            this.cmu = cmu;
            this.verbMeaning = verbMeaning;
            this.verbForm = verbForm;
            this.verbReflection = verbReflection;
            this.verbVoice = verbVoice;
            this.prepositionTerm = prepositionTerm;
            this.nounAddMeanings = nounAddMeanings.ToList();
            this.nounCase = nounCase;
            this.meaning = meaning;
        }

        public ComponentMorphologicalUnit CMU { get => cmu; }
        public string VerbMeaning { get => verbMeaning; }
        public string VerbForm { get => verbForm; }
        public string VerbReflection { get => verbReflection; }
        public string VerbVoice { get => verbVoice; }

        public Term PrepositionTerm { get => prepositionTerm; }
        public IEnumerable<string> NounAddMeanings { get => nounAddMeanings; }
        public string NounCase { get => nounCase; }
        public string Meaning { get => meaning; }

        public override string ToString()
        {
            return $"({verbMeaning}, "
                + $"[{verbForm} {verbReflection} {verbVoice}],"
                + $"{prepositionTerm.AllLexemes}, "
                + $"{nounAddMeanings}, "
                + $"{nounCase}) {meaning}";
        }
    }
}
