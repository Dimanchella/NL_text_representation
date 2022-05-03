using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class MorphologicalTraitSet
    {
        public long IdSetLine { get; set; }
        public long IdWordForm { get; set; }
        public long IdTrait { get; set; }
        public long IdTraitType { get; set; }

        public virtual MorphologicalTraitType IdTraitNavigation { get; set; }
        public virtual MorphologicalTrait IdTraitTypeNavigation { get; set; }
        public virtual WordForm IdWordFormNavigation { get; set; }
    }
}
