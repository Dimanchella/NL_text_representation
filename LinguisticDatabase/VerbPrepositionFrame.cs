using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class VerbPrepositionFrame
    {
        public VerbPrepositionFrame()
        {
            MeaningLimits = new HashSet<MeaningLimit>();
        }

        public long IdFrame { get; set; }
        public long IdMeaningSituation { get; set; }
        public long IdTraitVerbForm { get; set; }
        public long IdTraitVerbReflect { get; set; }
        public long IdTraitVerbVoice { get; set; }
        public long IdTermPreposition { get; set; }
        public long IdTraitCase { get; set; }
        public long IdMeaningFrame { get; set; }
        public string Comment { get; set; }

        public virtual Meaning IdMeaningFrameNavigation { get; set; }
        public virtual Meaning IdMeaningSituationNavigation { get; set; }
        public virtual Term IdTermPrepositionNavigation { get; set; }
        public virtual MorphologicalTrait IdTraitCaseNavigation { get; set; }
        public virtual MorphologicalTrait IdTraitVerbFormNavigation { get; set; }
        public virtual MorphologicalTrait IdTraitVerbReflectNavigation { get; set; }
        public virtual MorphologicalTrait IdTraitVerbVoiceNavigation { get; set; }
        public virtual ICollection<MeaningLimit> MeaningLimits { get; set; }
    }
}
