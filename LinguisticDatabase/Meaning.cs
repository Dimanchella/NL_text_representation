using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class Meaning
    {
        public Meaning()
        {
            MeaningLimits = new HashSet<MeaningLimit>();
            PrepositionFrameIdMeaningAddNoun1Navigations = new HashSet<PrepositionFrame>();
            PrepositionFrameIdMeaningAddNoun2Navigations = new HashSet<PrepositionFrame>();
            PrepositionFrameIdMeaningFrameNavigations = new HashSet<PrepositionFrame>();
            QuestionRoleFrames = new HashSet<QuestionRoleFrame>();
            TermAddMeanings = new HashSet<TermAddMeaning>();
            TermMainMeanings = new HashSet<TermMainMeaning>();
            VerbPrepositionFrameIdMeaningFrameNavigations = new HashSet<VerbPrepositionFrame>();
            VerbPrepositionFrameIdMeaningSituationNavigations = new HashSet<VerbPrepositionFrame>();
        }

        public long IdMeaning { get; set; }
        public long IdType { get; set; }
        public string Meaning1 { get; set; }

        public virtual MeaningType IdTypeNavigation { get; set; }
        public virtual ICollection<MeaningLimit> MeaningLimits { get; set; }
        public virtual ICollection<PrepositionFrame> PrepositionFrameIdMeaningAddNoun1Navigations { get; set; }
        public virtual ICollection<PrepositionFrame> PrepositionFrameIdMeaningAddNoun2Navigations { get; set; }
        public virtual ICollection<PrepositionFrame> PrepositionFrameIdMeaningFrameNavigations { get; set; }
        public virtual ICollection<QuestionRoleFrame> QuestionRoleFrames { get; set; }
        public virtual ICollection<TermAddMeaning> TermAddMeanings { get; set; }
        public virtual ICollection<TermMainMeaning> TermMainMeanings { get; set; }
        public virtual ICollection<VerbPrepositionFrame> VerbPrepositionFrameIdMeaningFrameNavigations { get; set; }
        public virtual ICollection<VerbPrepositionFrame> VerbPrepositionFrameIdMeaningSituationNavigations { get; set; }
    }
}
