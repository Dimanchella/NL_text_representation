using System;
using System.Collections.Generic;

#nullable disable

namespace LinguisticDatabase
{
    public partial class Word
    {
        public Word()
        {
            WordForms = new HashSet<WordForm>();
        }

        public long IdWord { get; set; }
        public string Word1 { get; set; }

        public virtual ICollection<WordForm> WordForms { get; set; }
    }
}
