using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LinguisticDatabase;
using Term = NL_text_representation.ComponentMorphologicalRepresentation.Entities.Term;
using TermComponent = NL_text_representation.ComponentMorphologicalRepresentation.Entities.TermComponent;
using NL_text_representation.DatabaseRequester.Entities;

namespace NL_text_representation.DatabaseRequester
{
    public static class MorphologicalDBRequester
    {
        private static readonly Linguistic_DatabaseContext dbContext = new();
        
        private static readonly List<DBTerms> dbTerms = GetTermsFromDB();
        private static readonly List<DBTermComponents> dbTermComponents = GetTermComponentsFromDB();

        public static IEnumerable<Term> GetTermsOnLexeme(string firstLexeme)
        {
            try
            {
                List<Term> terms = dbTerms
                    .Select(term => new Term(
                        dbTermComponents
                            .Where(component => component.ID == term.ID)
                            .OrderBy(component => component.Position)
                            .Select(component => new TermComponent(
                                component.Lexeme, 
                                component.IsMain
                                )),
                        term.PartOfSpeech,
                        term.SubClass
                        )).ToList();

                return terms.Where(term => term.Components.First().Lexeme.ToLower().Equals(firstLexeme.ToLower()));
            }
            catch (Exception e)
            {
                throw new Exception($"Ошибка запроса: {e.Message}");
            }
        }
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
                            ID = component.IdTerm,
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
    }
}
