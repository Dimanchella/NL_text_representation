using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class LinksToKnowledgeBase
    {
        public long IdLink { get; set; }
        public long IdTerm { get; set; }
        public long KbReference { get; set; }

        public virtual Term IdTermNavigation { get; set; }
    }
}
