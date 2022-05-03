using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class Term
    {
        public Term()
        {
            LinksToKnowledgeBases = new HashSet<LinksToKnowledgeBase>();
            PrepositionFrames = new HashSet<PrepositionFrame>();
            QuestionRoleFrameIdTermPrepositionNavigations = new HashSet<QuestionRoleFrame>();
            QuestionRoleFrameIdTermPronounInterrogativeRelativeAdverbNavigations = new HashSet<QuestionRoleFrame>();
            TermComponents = new HashSet<TermComponent>();
            TermMainMeanings = new HashSet<TermMainMeaning>();
            VerbPrepositionFrames = new HashSet<VerbPrepositionFrame>();
        }

        public long IdTerm { get; set; }
        public long IdTraitPartOfSpeech { get; set; }
        public long? IdTraitSubclass { get; set; }
        public string Comment { get; set; }

        public virtual MorphologicalTrait IdTraitPartOfSpeechNavigation { get; set; }
        public virtual MorphologicalTrait IdTraitSubclassNavigation { get; set; }
        public virtual ICollection<LinksToKnowledgeBase> LinksToKnowledgeBases { get; set; }
        public virtual ICollection<PrepositionFrame> PrepositionFrames { get; set; }
        public virtual ICollection<QuestionRoleFrame> QuestionRoleFrameIdTermPrepositionNavigations { get; set; }
        public virtual ICollection<QuestionRoleFrame> QuestionRoleFrameIdTermPronounInterrogativeRelativeAdverbNavigations { get; set; }
        public virtual ICollection<TermComponent> TermComponents { get; set; }
        public virtual ICollection<TermMainMeaning> TermMainMeanings { get; set; }
        public virtual ICollection<VerbPrepositionFrame> VerbPrepositionFrames { get; set; }
    }
}
