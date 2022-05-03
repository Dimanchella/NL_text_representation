using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class MeaningLimit
    {
        public long IdLimit { get; set; }
        public long IdFrame { get; set; }
        public long? IdMeaningAdd { get; set; }

        public virtual VerbPrepositionFrame IdFrameNavigation { get; set; }
        public virtual Meaning IdMeaningAddNavigation { get; set; }
    }
}
