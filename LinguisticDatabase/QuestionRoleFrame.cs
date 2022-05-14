using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class QuestionRoleFrame
    {
        public long IdFrame { get; set; }
        public long IdTermPreposition { get; set; }
        public long IdTermPronounInterrogativeRelativeAdverb { get; set; }
        public long IdMeaningFrame { get; set; }
        public string Comment { get; set; }

        public virtual Meaning IdMeaningFrameNavigation { get; set; }
        public virtual Term IdTermPrepositionNavigation { get; set; }
        public virtual Term IdTermPronounInterrogativeRelativeAdverbNavigation { get; set; }
    }
}
