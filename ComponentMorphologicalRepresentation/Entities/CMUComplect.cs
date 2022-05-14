using System;
using System.Collections.Generic;
using System.Linq;

namespace NL_text_representation.ComponentMorphologicalRepresentation.Entities
{
    public class CMUComplect
    {
        private readonly string word;
        private readonly ComponentMorphologicalUnit[] cmus;

        public CMUComplect(string word, IEnumerable<ComponentMorphologicalUnit> cmus)
        {
            this.word = word;
            this.cmus = cmus.ToArray();
        }

        public enum Operations
        {
            And = 0,
            Or = 1
        }

        public string Unit { get => word; }
        public IEnumerable<ComponentMorphologicalUnit> CMUs { get => cmus; }

        public IEnumerable<ComponentMorphologicalUnit> GetCMRsByTrates(Operations operation, params string[] trates)
        {
            return cmus.Where(cmr =>
            {
                bool isConf;
                switch (operation)
                {
                    case Operations.Or:
                        isConf = false;
                        for (int i = 0; i < trates.Length && !isConf; i++)
                        {
                            isConf = cmr.Form.Traits.Grams.Contains(trates[i]);
                        }
                        break;
                    case Operations.And:
                    default:
                        isConf = true;
                        for (int i = 0; i < trates.Length && isConf; i++)
                        {
                            isConf = cmr.Form.Traits.Grams.Contains(trates[i]);
                        }
                        break;
                }
                return isConf;
            });
        }
    }
}
