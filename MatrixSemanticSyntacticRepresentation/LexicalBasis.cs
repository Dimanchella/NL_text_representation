using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NL_text_representation.ComponentMorphologicalRepresentation.Entities;
using NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities;
using NL_text_representation.DatabaseInteraction;

namespace NL_text_representation.MatrixSemanticSyntacticRepresentation
{
    public class LexicalBasis
    {
        private List<LSUComplect> lsp = new();
        private List<PFComplect> pfp = new();
        private List<VPFComplect> vpfp = new();

        public IEnumerable<LSUComplect> LSP { get => lsp; }
        public IEnumerable<PFComplect> PFP { get => pfp; }
        public IEnumerable<VPFComplect> VPFP { get => vpfp; }

        public void ProjectLinguisticBasis(List<CMUComplect> cmr)
        {
            foreach(var cmuc in cmr)
            {
                List<LexicalSemanticUnit> lsu = null;
                List<PrepositionFrame> pf = null;
                List<VerbPrepositionFrame> vpf = null;
                foreach (ComponentMorphologicalUnit cmu in cmuc.CMUs)
                {
                    switch(cmu.Term.PartOfSpeech)
                    {
                        case "предлог":
                            pf = DatabaseRequester.GetPrepositionFramesOnCMUPrep(cmu).ToList();
                            break;
                        case "глагол":
                        case "прич":
                        case "деепр":
                            vpf = DatabaseRequester.GetVerbPrepositionFrameOnCMUVerb(cmu).ToList();
                            break;
                        default:
                            lsu = DatabaseRequester.GetLexicalSemanticMeaningsOnCMU(cmu).ToList();
                            break;
                    }
                }

                if (pf != null)
                    pfp.Add(new(cmuc.Unit, pf));
                if (vpf != null)
                    vpfp.Add(new(cmuc.Unit, vpf));
                if (lsu != null)
                    lsp.Add(new(cmuc.Unit, lsu));
            }
        }
    }
}
