using LinguisticDatabase;
using NL_text_representation.ComponentMorphologicalRepresentation;
using NL_text_representation.ComponentMorphologicalRepresentation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NL_text_representation.SemBuilding
{
    public class SemBuilder
    {
        private HashSet<string> signs = new HashSet<string>();

        public SemBuilder()
        {
            signs.Add("меньше");
            signs.Add("не меньше");
            signs.Add("больше");
            signs.Add("не больше");
        }

        public String getBaseForm(IEnumerable<CMUComplect> cmr, int index)
        {
            var word = cmr.ToList().GetRange(index, 1).First();
            return word.CMUs
                      .Select(cmr => cmr.Form.Lemma)
                      .First();
        }

        public String getSemMaining(String baseForm)
        {
            Linguistic_DatabaseContext context = new Linguistic_DatabaseContext();

            var query = from meaning in context.TermMainMeanings
                        join termComponent in context.TermComponents on meaning.IdTerm equals termComponent.IdTerm
                        where termComponent.IdLexemeNavigation.Lexeme1.ToLower().Equals(baseForm.ToLower())
                        select meaning.IdMeaningMainNavigation.Meaning1;

            return query.First();
        }

        public String getSemMainingId(CMUComplect cmr)
        {
            Linguistic_DatabaseContext context = new Linguistic_DatabaseContext();

            var query = from meaning in context.TermMainMeanings
                        where meaning.IdTerm == cmr.CMUs.First().Term.ID
                        select meaning.IdMeaningMainNavigation.Meaning1;

            string sem = query.First();
            if (sem.Contains("("))
            {
                return sem.Replace("#число#", cmr.Unit);
            }
            else
            {
                return "(Назв, " + sem + ")";
            }
        }

        public String modeficationForm(String inputForm)
        {
            String[] rel = inputForm.Split('(');

            String[] arg2 = rel[1].Split(',');

            return "(" + rel[0] + ",=," + arg2[1].Trim();
        }

        public String modeficationFormConcept(String inputForm)
        {
            String[] rel = inputForm.Split('(');

            if (rel.Length > 1)
            {
                String[] arg2 = rel[1].Split(',');
                if (!signs.Contains(arg2[0].ToLower()))
                {
                    if (arg2.Length == 2)
                    {
                        return ",=," + arg2[1].Remove(arg2[1].Length - 1).Trim();
                    }
                    else
                    {
                        return ",=," + arg2[2].Remove(arg2[2].Length - 1).Trim();
                    }
                }
                else
                {
                    if (arg2.Length == 2)
                    {
                        return "," + arg2[0].ToLower() + "," + arg2[1].Remove(arg2[1].Length - 1).Trim();
                    }
                    else
                    {
                        return "," + arg2[0].ToLower() + "," + arg2[2].Remove(arg2[2].Length - 1).Trim();//todo мб не нужно
                    }
                }

            }
            else
            {
                return ",=," + inputForm;
            }

        }

        public String ConstructSemImage(IEnumerable<CMUComplect> cmr, int j, int m)
        {
            var adjs = cmr.ToList().GetRange(j, m);

            int counter = j;

            String result = "";
            adjs.ForEach(x => { result += modeficationForm(getSemMaining(getBaseForm(cmr, counter))); counter++; });

            return result.Replace(")(", ")*(");

        }

        public String DiscoverConcRelat(IEnumerable<CMUComplect> cmr, int indexNoun1, int indexNoun2, String prep)
        {
            String base1 = getBaseForm(cmr, indexNoun1);
            String base2 = getBaseForm(cmr, indexNoun2);

            var word2 = cmr.ToList().GetRange(indexNoun2, 1).First();

            var frames = getPerpFrames(prep);

            var debug1 = frames.ToList();

            var sorts1 = getSortNoun(base1).ToList();
            var sorts2 = getSortNoun(base2).ToList();
            var grc = word2.CMUs.Select(w => w.Form.Traits["падеж"]).ToList();

            var goodFrame = frames.Where(frame => sorts1.Contains(frame.IdMeaningAddNoun1Navigation.Meaning1)
            && sorts2.Contains(frame.IdMeaningAddNoun2Navigation.Meaning1)
            && grc.Contains(frame.IdTraitCase2Navigation.Trait));

            return goodFrame.Select(x => x.IdMeaningFrameNavigation.Meaning1).First();
        }

        private IQueryable<PrepositionFrame> getPerpFrames(String prep)
        {

            Linguistic_DatabaseContext context = new Linguistic_DatabaseContext();
            IQueryable<PrepositionFrame> query = from frame in context.PrepositionFrames
                                                 join term in context.TermComponents on frame.IdTermPreposition equals term.IdTerm
                                                 where term.IdLexemeNavigation.Lexeme1.ToLower().Equals(prep.ToLower())
                                                 select frame;
            return query;
        }

        private IQueryable<string> getSortNoun(String lexeme)
        {
            Linguistic_DatabaseContext context = new Linguistic_DatabaseContext();
            IQueryable<string> query = from sorts in context.TermAddMeanings
                                       join term in context.TermComponents on sorts.IdTermMeainMeaningNavigation.IdTerm equals term.IdTerm
                                       where term.IdLexemeNavigation.Lexeme1.ToLower().Equals(lexeme.ToLower())
                                       select sorts.IdMeaningAddNavigation.Meaning1;
            return query;
        }

        public String getSemReprRequest(String request)
        {

            String result = "";
            TermsSearcher termsAnalizer = new();

            termsAnalizer.FindCMR(request);
            var cmr = termsAnalizer.CMR;

            var nouns = findNouns(cmr);

            if (nouns.Count() == 1)
            {
                int posNoun1 = nouns[0];

                String base1 = getBaseForm(cmr, posNoun1);
                String semnoun1 = getSemMaining(base1);

                String concept1 = semnoun1;
                if (posNoun1 > 0)
                {
                    concept1 += "*" + ConstructSemImage(cmr, 0, posNoun1);
                }
                result = concept1;
            }

            else if (nouns.Count() == 2 || (nouns.Count() == 2 || nouns[2] - nouns[1] <= 1))
            {
                int posNoun1 = nouns[0];
                int posNoun2 = nouns[1];

                int posPrep = posNoun1 + 1;

                String prep;
                if (checkPastOfSpeech(cmr, posPrep, "предлог"))
                {
                    prep = getBaseForm(cmr, posPrep);
                }
                else
                {
                    prep = "#nil#";
                    posPrep = posNoun1;
                }

                String frame = DiscoverConcRelat(cmr, posNoun1, posNoun2, prep);

                String base1 = getBaseForm(cmr, posNoun1);
                String semnoun1 = getSemMaining(base1);

                String concept1 = semnoun1;
                if (posNoun1 > 0)
                {
                    concept1 += "*" + ConstructSemImage(cmr, 0, posNoun1);
                }

                String base2 = getBaseForm(cmr, posNoun2);
                String semnoun2 = getSemMaining(base2);

                String concept2 = semnoun2;

                //Проверка на имя собственное, если нет , то добавлять "нек"

                if (posNoun2 - 1 > posPrep)
                {
                    concept2 += "*" + ConstructSemImage(cmr, posPrep + 1, posNoun2 - posPrep - 1);
                }

                if (posNoun2 + 1 < cmr.Count())
                {
                    string semAfterNoun2 = getSemMainingId(cmr.ToList().GetRange(posNoun2 + 1, 1).First());


                    concept2 += "*" + semAfterNoun2;
                }

                result = concept1 + "*(" + frame +  modeficationFormConcept(concept2) + ")";
            }
            else
            {
                Console.WriteLine("Error request");
            }
            return result;
        }

        private List<int> findNouns(IEnumerable<CMUComplect> cmr)
        {
            List<int> nouns = new List<int>();
            int i = 0;
            foreach (var word in cmr)
            {
                if (word.CMUs.Select(x => x.Term.PartOfSpeech.ToLower()).First().Equals("сущ"))
                {
                    nouns.Add(i);
                }
                i++;

            }
            return nouns;
        }

        private bool checkPastOfSpeech(IEnumerable<CMUComplect> cmr, int posWord, String partOfSpeech)
        {
            var word = cmr.ToList().GetRange(posWord, 1).First().Unit;

            return cmr.Where(wr => wr.Unit.Equals(word)).First().CMUs.Select(cmr => cmr.Term.PartOfSpeech).First().Equals(partOfSpeech);
        }
    }
}