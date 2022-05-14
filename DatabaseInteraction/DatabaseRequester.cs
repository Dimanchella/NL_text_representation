using System;
using System.Collections.Generic;
using System.Linq;

using LinguisticDatabase;
using Term = NL_text_representation.ComponentMorphologicalRepresentation.Entities.Term;
using TermComponent = NL_text_representation.ComponentMorphologicalRepresentation.Entities.TermComponent;
using NL_text_representation.DatabaseInteraction.Entities;

namespace NL_text_representation.DatabaseInteraction
{
    public static class DatabaseRequester
    {
        private static readonly Linguistic_DatabaseContext dbContext = new();

        private static readonly List<DBTerms> dbTerms = GetTermsFromDB();
        private static readonly List<DBTermComponents> dbTermComponents = GetTermComponentsFromDB();
        private static readonly List<DBMainMeaning> dbMainMeanings = GetMainMeaningsFromDB();
        private static readonly List<DBAddMeaning> dbAddMeanings = GetAddMeaningsFromDB();
        private static readonly List<DBAddMeaning> dBAddMeaningLimits = GetAddMeaningLimits();

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

        public static IEnumerable<Term> GetTermsOnLexeme(string firstLexeme)
        {
            try
            {
                return dbTerms
                    .Select(term => new Term(
                        term.ID,
                        dbTermComponents
                            .Where(component => component.TermID == term.ID)
                            .OrderBy(component => component.Position)
                            .Select(component => new TermComponent(
                                component.Lexeme,
                                component.IsMain
                                )
                            ),
                        term.PartOfSpeech,
                        term.SubClass
                        ))
                    .Where(term => term
                        .Components.First()
                        .Lexeme.ToLower().Equals(firstLexeme.ToLower())
                        );
            }
            catch (Exception e)
            {
                throw new Exception($"Ошибка запроса: {e.Message}");
            }
        }
    }
}
