using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class PrepositionFrame
    {
        public long IdFrame { get; set; }
        public long IdTermPreposition { get; set; }
        public long IdMeaningAddNoun1 { get; set; }
        public long IdMeaningAddNoun2 { get; set; }
        public long IdTraitCase2 { get; set; }
        public long IdMeaningFrame { get; set; }
        public string Comment { get; set; }

        public virtual Meaning IdMeaningAddNoun1Navigation { get; set; }
        public virtual Meaning IdMeaningAddNoun2Navigation { get; set; }
        public virtual Meaning IdMeaningFrameNavigation { get; set; }
        public virtual Term IdTermPrepositionNavigation { get; set; }
        public virtual MorphologicalTrait IdTraitCase2Navigation { get; set; }
    }
}
