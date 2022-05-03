using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class MorphologicalTrait
    {
        public MorphologicalTrait()
        {
            MorphologicalTraitSets = new HashSet<MorphologicalTraitSet>();
            PrepositionFrames = new HashSet<PrepositionFrame>();
            TermIdTraitPartOfSpeechNavigations = new HashSet<Term>();
            TermIdTraitSubclassNavigations = new HashSet<Term>();
            VerbPrepositionFrameIdTraitCaseNavigations = new HashSet<VerbPrepositionFrame>();
            VerbPrepositionFrameIdTraitVerbFormNavigations = new HashSet<VerbPrepositionFrame>();
            VerbPrepositionFrameIdTraitVerbReflectNavigations = new HashSet<VerbPrepositionFrame>();
            VerbPrepositionFrameIdTraitVerbVoiceNavigations = new HashSet<VerbPrepositionFrame>();
        }

        public long IdTrait { get; set; }
        public long IdTraitType { get; set; }
        public string Trait { get; set; }

        public virtual MorphologicalTraitType IdTraitTypeNavigation { get; set; }
        public virtual ICollection<MorphologicalTraitSet> MorphologicalTraitSets { get; set; }
        public virtual ICollection<PrepositionFrame> PrepositionFrames { get; set; }
        public virtual ICollection<Term> TermIdTraitPartOfSpeechNavigations { get; set; }
        public virtual ICollection<Term> TermIdTraitSubclassNavigations { get; set; }
        public virtual ICollection<VerbPrepositionFrame> VerbPrepositionFrameIdTraitCaseNavigations { get; set; }
        public virtual ICollection<VerbPrepositionFrame> VerbPrepositionFrameIdTraitVerbFormNavigations { get; set; }
        public virtual ICollection<VerbPrepositionFrame> VerbPrepositionFrameIdTraitVerbReflectNavigations { get; set; }
        public virtual ICollection<VerbPrepositionFrame> VerbPrepositionFrameIdTraitVerbVoiceNavigations { get; set; }
    }
}
