using NL_text_representation.ComponentMorphologicalRepresentation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace nli_to_lod.Validation
{
    public class Validator
    {
        private Dictionary<KeyValuePair<int, String>, int> stateTransition = new Dictionary<KeyValuePair<int, String>, int>();

        public Validator()
        {
            stateTransition.Add(new KeyValuePair<int, string>(0, "прилаг"), 1);
            stateTransition.Add(new KeyValuePair<int, string>(0, "сущ"), 2);

            stateTransition.Add(new KeyValuePair<int, string>(1, "прилаг"), 1);
            stateTransition.Add(new KeyValuePair<int, string>(1, "сущ"), 2);

            stateTransition.Add(new KeyValuePair<int, string>(2, "прилаг"), 4);
            stateTransition.Add(new KeyValuePair<int, string>(2, "сущ"), 5);
            stateTransition.Add(new KeyValuePair<int, string>(2, "предлог"), 3);

            stateTransition.Add(new KeyValuePair<int, string>(3, "прилаг"), 4);
            stateTransition.Add(new KeyValuePair<int, string>(3, "сущ"), 5);

            stateTransition.Add(new KeyValuePair<int, string>(4, "прилаг"), 4);
            stateTransition.Add(new KeyValuePair<int, string>(4, "сущ"), 5);

            stateTransition.Add(new KeyValuePair<int, string>(5, "прилаг"), 6);
            stateTransition.Add(new KeyValuePair<int, string>(5, "сущ"), 6);

        }

        public bool validateRequest(IEnumerable<CMUComplect> cmr)
        {
            int state = 0;
            try
            {
                foreach (CMUComplect comp in cmr)
            {
                state = stateTransition[new KeyValuePair<int, string>(state, comp.CMUs.Select(x => x.Term.PartOfSpeech.ToLower()).First())];
            }
            }
            catch (KeyNotFoundException e)
            {
                return false;
            }

            return state == 5 || state == 6;
        }
        
    }
}
