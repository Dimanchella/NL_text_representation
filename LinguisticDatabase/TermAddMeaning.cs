using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class TermAddMeaning
    {
        public long IdTermAddMeaning { get; set; }
        public long IdTermMeainMeaning { get; set; }
        public long IdMeaningAdd { get; set; }
        public string Comment { get; set; }

        public virtual Meaning IdMeaningAddNavigation { get; set; }
        public virtual TermMainMeaning IdTermMeainMeaningNavigation { get; set; }
    }
}
