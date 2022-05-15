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
        private List<QRFComplect> qrfp = new();

        public IEnumerable<LSUComplect> LSP { get => lsp; }
        public IEnumerable<QRFComplect> QRFP { get => qrfp; }
        public IEnumerable<PFComplect> PFP { get => pfp; }
        public IEnumerable<VPFComplect> VPFP { get => vpfp; }

        public void ProjectLinguisticBasis(IEnumerable<CMUComplect> cmr)
        {
            lsp.Clear();
            pfp.Clear();
            vpfp.Clear();
            qrfp.Clear();
            foreach (var cmuc in cmr)
            {
                List<LexicalSemanticUnit> lsus = null;
                List<QuestionRoleFrame> qrfs = null;
                List<PrepositionFrame> pfs = null;
                List<VerbPrepositionFrame> vpfs = null;
                foreach (ComponentMorphologicalUnit cmu in cmuc.CMUs)
                {
                    switch(cmu.Term.PartOfSpeech)
                    {
                        case "предлог":
                            pfs = DatabaseRequester.GetPrepositionFramesOnCMUPrep(cmu).ToList();
                            break;
                        case "местоим":
                            if (cmu.Term.SubClass.Equals("вопр-относ-местоим"))
                            {
                                qrfs = DatabaseRequester.GetQuestionRoleFramesOnCMUPronoun(cmu).ToList();
                            }
                            else
                            {
                                lsus = DatabaseRequester.GetLexicalSemanticMeaningsOnCMU(cmu).ToList();
                            }
                            break;
                        case "глагол":
                        case "прич":
                        case "деепр":
                            vpfs = DatabaseRequester.GetVerbPrepositionFrameOnCMUVerb(cmu).ToList();
                            break;
                        default:
                            lsus = DatabaseRequester.GetLexicalSemanticMeaningsOnCMU(cmu).ToList();
                            break;
                    }
                }

                if (lsus != null)
                    lsp.Add(new(cmuc.Unit, lsus));
                if (qrfs != null)
                    qrfp.Add(new(cmuc.Unit, qrfs));
                if (pfs != null)
                    pfp.Add(new(cmuc.Unit, pfs));
                if (vpfs != null)
                    vpfp.Add(new(cmuc.Unit, vpfs));
            }
        }
    }
}
