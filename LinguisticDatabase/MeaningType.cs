using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class MeaningType
    {
        public MeaningType()
        {
            Meanings = new HashSet<Meaning>();
        }

        public long IdType { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Meaning> Meanings { get; set; }
    }
}
