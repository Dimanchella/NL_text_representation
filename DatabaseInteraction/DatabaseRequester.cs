using LinguisticDatabase;
using NL_text_representation.DatabaseInteraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using ComponentMorphologicalUnit = NL_text_representation.ComponentMorphologicalRepresentation.Entities.ComponentMorphologicalUnit;
using LexicalSemanticUnit = NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities.LexicalSemanticUnit;
using PrepositionFrame = NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities.PrepositionFrame;
using QuestionRoleFrame = NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities.QuestionRoleFrame;
using Term = NL_text_representation.ComponentMorphologicalRepresentation.Entities.Term;
using TermComponent = NL_text_representation.ComponentMorphologicalRepresentation.Entities.TermComponent;
using VerbPrepositionFrame = NL_text_representation.MatrixSemanticSyntacticRepresentation.Entities.VerbPrepositionFrame;

namespace NL_text_representation.DatabaseInteraction
{
    public static class DatabaseRequester
    {
        private static readonly Linguistic_DatabaseContext dbContext = new();

        private static readonly List<DBTerms> dbTerms = GetTermsFromDB();
        private static readonly List<DBTermComponents> dbTermComponents = GetTermComponentsFromDB();
        private static readonly List<DBMainMeaning> dbMainMeanings = GetMainMeaningsFromDB();
        private static readonly List<DBAddMeaning> dbAddMeanings = GetAddMeaningsFromDB();
        private static readonly List<DBPrepositionFrame> dbPrepositionFrames = GetPrepositionFramesFromDB();
        private static readonly List<DBVerbPrepositionFrame> dbVerbPrepositionFrames = GetVerbPrepositionFrameFromDB();
        private static readonly List<DBAddMeaning> dbAddMeaningLimits = GetAddMeaningLimits();
        private static readonly List<DBQuestionRoleFrame> dbQuestionRoleFrames = GetQuestionRoleFramesFromDB();

        private static List<DBTerms> GetTermsFromDB()
        {
            try
            {
                return (from term in dbContext.Terms
                        select new DBTerms
                        {
                            ID = term.IdTerm,
                            PartOfSpeech = term.IdTraitPartOfSpeechNavigation.Trait,
                            SubClass = term.IdTraitSubclassNavigation.Trait
                        }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private static List<DBTermComponents> GetTermComponentsFromDB()
        {
            try
            {
                return (from component in dbContext.TermComponents
                        select new DBTermComponents
                        {
                            TermID = component.IdTerm,
                            Lexeme = component.IdLexemeNavigation.Lexeme1,
                            IsMain = component.IsMainLexeme.Value,
                            Position = component.PositionLexeme
                        }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private static List<DBMainMeaning> GetMainMeaningsFromDB() 
        {
            try
            {
                return (from meaning in dbContext.TermMainMeanings
                        select new DBMainMeaning
                        {
                            ID = meaning.IdTermMainMeaning,
                            TermID = meaning.IdTerm,
                            Meaning = meaning.IdMeaningMainNavigation.Meaning1
                        }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private static List<DBAddMeaning> GetAddMeaningsFromDB()
        {
            try
            {
                return (from meaning in dbContext.TermAddMeanings
                        select new DBAddMeaning
                        {
                            ID = meaning.IdTermAddMeaning,
                            MainMeaningID = meaning.IdTermMeainMeaning,
                            Meaning = meaning.IdMeaningAddNavigation.Meaning1
                        }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private static List<DBPrepositionFrame> GetPrepositionFramesFromDB()
        {
            try
            {
                return (from frame in dbContext.PrepositionFrames
                        select new DBPrepositionFrame
                        {
                            ID = frame.IdFrame,
                            PrepositionTermID = frame.IdTermPreposition,
                            Noun1AddMeaning = frame.IdMeaningAddNoun1Navigation.Meaning1,
                            Noun2AddMeaning = frame.IdMeaningAddNoun2Navigation.Meaning1,
                            Noun2Case = frame.IdTraitCase2Navigation.Trait,
                            Meaning = frame.IdMeaningFrameNavigation.Meaning1
                        }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private static List<DBVerbPrepositionFrame> GetVerbPrepositionFrameFromDB()
        {
            try
            {
                return (from frame in dbContext.VerbPrepositionFrames
                        select new DBVerbPrepositionFrame
                        {
                            ID = frame.IdFrame,
                            VerbMeaning = frame.IdMeaningSituationNavigation.Meaning1,
                            VerbForm = frame.IdTraitVerbFormNavigation.Trait,
                            VerbReflection = frame.IdTraitVerbReflectNavigation.Trait,
                            VerbVoice = frame.IdTraitVerbVoiceNavigation.Trait,
                            PrepositionTermID = frame.IdTermPreposition,
                            NounCase = frame.IdTraitCaseNavigation.Trait,
                            Meaning = frame.IdMeaningFrameNavigation.Meaning1
                        }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private static List<DBAddMeaning> GetAddMeaningLimits()
        {
            try
            {
                return (from meaning in dbContext.MeaningLimits
                        select new DBAddMeaning
                        {
                            ID = meaning.IdLimit,
                            MainMeaningID = meaning.IdFrame,
                            Meaning = meaning.IdMeaningAddNavigation.Meaning1
                        }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private static List<DBQuestionRoleFrame> GetQuestionRoleFramesFromDB()
        {
            try
            {
                return (from frame in dbContext.QuestionRoleFrames
                        select new DBQuestionRoleFrame
                        {
                            ID = frame.IdFrame,
                            PrepositionTermID = frame.IdTermPreposition,
                            PronounTermID = frame.IdTermPronounInterrogativeRelativeAdverb,
                            Meaning = frame.IdMeaningFrameNavigation.Meaning1
                        }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static IEnumerable<Term> GetTermsOnLexeme(string firstLexeme)
        {
            try
            {
                return (from term in dbTerms
                        select new Term(
                            id: term.ID,
                            components: from component in dbTermComponents
                                        where component.TermID == term.ID
                                        orderby component.Position
                                        select new TermComponent(
                                            lexeme: component.Lexeme,
                                            isMain: component.IsMain
                                            ),
                            partOfSpeech: term.PartOfSpeech,
                            subClass: term.SubClass
                            )
                       ).Where(term => term.Components.First().Lexeme.ToLower().Equals(firstLexeme.ToLower()));
            }
            catch (Exception e)
            {
                throw new Exception($"Ошибка запроса: {e.Message}");
            }
        }

        public static IEnumerable<LexicalSemanticUnit> GetLexicalSemanticMeaningsOnCMU(ComponentMorphologicalUnit cmu)
        {
            try
            {
                return from main in dbMainMeanings
                       where main.TermID == cmu.Term.ID
                       select new LexicalSemanticUnit(
                           cmu: cmu,
                           meaning: main.Meaning,
                           addMeanings: from add in dbAddMeanings
                                        where add.MainMeaningID == main.ID
                                        select add.Meaning
                           );
            }
            catch (Exception e)
            {
                throw new Exception($"Ошибка запроса: {e.Message}");
            }
        }
        public static IEnumerable<VerbPrepositionFrame> GetVerbPrepositionFrameOnCMUVerb(ComponentMorphologicalUnit cmuVerb)
        {
            try
            {
                return from frame in dbVerbPrepositionFrames
                       join main in dbMainMeanings on frame.VerbMeaning equals main.Meaning
                       where main.TermID == cmuVerb.Term.ID && VerbMorphologicalMatch(frame, cmuVerb)
                       select new VerbPrepositionFrame(
                           cmu: cmuVerb,
                           verbMeaning: frame.VerbMeaning,
                           verbForm: frame.VerbForm,
                           verbReflection: frame.VerbReflection,
                           verbVoice: frame.VerbVoice,
                           prepositionTerm: (from term in dbTerms
                                             where term.ID == frame.PrepositionTermID
                                             select new Term(
                                                 id: term.ID,
                                                 components: from component in dbTermComponents
                                                             where component.TermID == term.ID
                                                             orderby component.Position
                                                             select new TermComponent(
                                                                 lexeme: component.Lexeme,
                                                                 isMain: component.IsMain
                                                                 ),
                                                 partOfSpeech: term.PartOfSpeech,
                                                 subClass: term.SubClass
                                                 )).First(),
                           nounAddMeanings: from add in dbAddMeaningLimits
                                            where add.MainMeaningID == frame.ID
                                            select add.Meaning,
                           nounCase: frame.NounCase,
                           meaning: frame.Meaning
                           );
            }
            catch (Exception e)
            {
                throw new Exception($"Ошибка запроса: {e.Message}");
            }
        }
        public static IEnumerable<PrepositionFrame> GetPrepositionFramesOnCMUPrep(ComponentMorphologicalUnit cmuPrep)
        {
            try
            {
                return from frame in dbPrepositionFrames
                       where frame.PrepositionTermID == cmuPrep.Term.ID
                       select new PrepositionFrame(
                           cmu: cmuPrep,
                           prepositionTerm: cmuPrep.Term,
                           noun1AddMeaning: frame.Noun1AddMeaning,
                           noun2AddMeaning: frame.Noun2AddMeaning,
                           noun2Case: frame.Noun2Case,
                           meaning: frame.Meaning
                           );

            }
            catch (Exception e)
            {
                throw new Exception($"Ошибка запроса: {e.Message}");
            }
        }
        public static IEnumerable<QuestionRoleFrame> GetQuestionRoleFramesOnCMUPronoun(ComponentMorphologicalUnit cmuPronoun)
        {
            try
            {
                return from frame in dbQuestionRoleFrames
                       where frame.PrepositionTermID == cmuPronoun.Term.ID
                       select new QuestionRoleFrame(
                           cmu: cmuPronoun,
                           prepositionTerm: (from term in dbTerms
                                             where term.ID == frame.PrepositionTermID
                                             select new Term(
                                                 id: term.ID,
                                                 components: from component in dbTermComponents
                                                             where component.TermID == term.ID
                                                             orderby component.Position
                                                             select new TermComponent(
                                                                 lexeme: component.Lexeme,
                                                                 isMain: component.IsMain
                                                                 ),
                                                 partOfSpeech: term.PartOfSpeech,
                                                 subClass: term.SubClass
                                                 )).First(),
                           pronounTerm: cmuPronoun.Term,
                           meaning: frame.Meaning
                           );

            }
            catch (Exception e)
            {
                throw new Exception($"Ошибка запроса: {e.Message}");
            }
        }

        private static bool VerbMorphologicalMatch(DBVerbPrepositionFrame frame, ComponentMorphologicalUnit cmuVerb) 
        {
            bool formMatch = false;
            bool reflectionMatch = false;
            bool voiceMatch = false;
            if (frame.VerbForm.Equals("глагол-личн") && cmuVerb.Form.Traits["чр"].Equals("гл")
                || frame.VerbForm.Equals("глаг-в-неопред-форме") && cmuVerb.Form.Traits["чр"].Equals("инф_гл")
                || frame.VerbForm.Equals("#nil#"))
            {
                formMatch = true;
            }
            if (frame.VerbReflection.Equals(cmuVerb.Form.Traits["возв"])
                || frame.VerbReflection.Equals("#nil#"))
            {
                reflectionMatch = true;
            }
            if (frame.VerbVoice.Equals(cmuVerb.Form.Traits["залог"])
                || frame.VerbVoice.Equals("#nil#"))
            {
                voiceMatch = true;
            }
            return formMatch && reflectionMatch && voiceMatch;
        }
    }
}
