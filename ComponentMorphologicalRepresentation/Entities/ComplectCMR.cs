using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL_text_representation.ComponentMorphologicalRepresentation.Entities
{
    public class ComplectCMR
    {
        private readonly string word;
        private readonly CMR[] cmrs;

        public ComplectCMR(string word, IEnumerable<CMR> cmrs)
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
        public IEnumerable<CMR> GetCMRs(CMRClassFilters filter = CMRClassFilters.All)
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

        public IEnumerable<CMR> GetCMRsByTrates(CMRClassFilters filter, Operations operation, params string[] trates)
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
