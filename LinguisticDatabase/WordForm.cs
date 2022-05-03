using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class WordForm
    {
        public WordForm()
        {
            MorphologicalTraitSets = new HashSet<MorphologicalTraitSet>();
        }

        public long IdWordForm { get; set; }
        public long IdWord { get; set; }
        public long IdLexeme { get; set; }

        public virtual Lexeme IdLexemeNavigation { get; set; }
        public virtual Word IdWordNavigation { get; set; }
        public virtual ICollection<MorphologicalTraitSet> MorphologicalTraitSets { get; set; }
    }
}
