using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class TermMainMeaning
    {
        public TermMainMeaning()
        {
            TermAddMeanings = new HashSet<TermAddMeaning>();
        }

        public long IdTermMainMeaning { get; set; }
        public long IdTerm { get; set; }
        public long IdMeaningMain { get; set; }
        public string Comment { get; set; }

        public virtual Meaning IdMeaningMainNavigation { get; set; }
        public virtual Term IdTermNavigation { get; set; }
        public virtual ICollection<TermAddMeaning> TermAddMeanings { get; set; }
    }
}
