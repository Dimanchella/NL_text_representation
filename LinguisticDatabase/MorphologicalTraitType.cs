using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class MorphologicalTraitType
    {
        public MorphologicalTraitType()
        {
            MorphologicalTraitSets = new HashSet<MorphologicalTraitSet>();
            MorphologicalTraits = new HashSet<MorphologicalTrait>();
        }

        public long IdTraitType { get; set; }
        public string TraitType { get; set; }

        public virtual ICollection<MorphologicalTraitSet> MorphologicalTraitSets { get; set; }
        public virtual ICollection<MorphologicalTrait> MorphologicalTraits { get; set; }
    }
}
