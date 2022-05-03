using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.ComponentMorphologicalRepresentation.Entities
{
    public class VariableCMR
    {
        private readonly string word;
        private readonly WordTerm[] cmrs;

        public VariableCMR(string word, IEnumerable<WordTerm> cmrs)
        {
            this.word = word;
            this.cmrs = cmrs.ToArray();
        }

        public enum Operations
        {
            And = 0,
            Or = 1
        }

        public enum CMRClassFilters
        {
            All = 0,
            WithClass = 1,
            WithoutClass = 2
        }

        public string Unit { get => word; }
        public IEnumerable<WordTerm> GetCMRs(CMRClassFilters filter = CMRClassFilters.All)
        {
            switch (filter)
            {
                case CMRClassFilters.WithClass:
                    return cmrs.Where(cmr => cmr.HasClass);
                case CMRClassFilters.WithoutClass:
                    return cmrs.Where(cmr => !cmr.HasClass);
                case CMRClassFilters.All:
                default:
                    return cmrs;
            }
        }

        public IEnumerable<WordTerm> GetCMRsByTrates(CMRClassFilters filter, Operations operation, params string[] trates)
        {
            return GetCMRs(filter).Where(cmr =>
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
