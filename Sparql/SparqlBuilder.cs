using LinguisticDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NL_text_representation.SPARQL
{
    internal class SparqlBuilder
    {
        private Dictionary<string, string> comparisonSigns = new Dictionary<string, string>();
        private Dictionary<string, string> specialConst = new Dictionary<string, string>();

        public SparqlBuilder()
        {
            comparisonSigns["меньше"] = "<";
            comparisonSigns["больше"] = ">";
            comparisonSigns["не меньше"] = ">=";
            comparisonSigns["не больше"] = "<=";

            comparisonSigns["раньше"] = "<";
            comparisonSigns["позже"] = ">";
            comparisonSigns["не раньше"] = ">=";
            comparisonSigns["не позже"] = "<=";

            specialConst["#макс#"] = "desc";
            specialConst["#мин#"] = "asc";

        }

        public String getSparql(String repr)
        {
            Linguistic_DatabaseContext context = new Linguistic_DatabaseContext();

            String[] parts = repr.Split('*');

            String entity = parts[0];

            var translateEntity = translate(context, entity);

            String typeEntity = createHeader(translateEntity);

            String querySparql = typeEntity;

            int numVar = 3;

            parts = parts.Skip(1).ToArray();

            String sorting = "";

            foreach (String part in parts)
            {
                String[] triple = part.Replace("(", "").Replace(")", "").Split(",");
                var translatePredicate = translate(context, triple[0]);

                List<string> translateValue = new List<string>();

                if (int.TryParse(triple[2], out _))
                {
                    translateValue.Add(triple[2]);

                }
                else if (isSpecialConst(triple[2]))
                {
                    sorting = specialConst[triple[2]] + "(?s" + numVar + ")";
                }
                else
                {
                    translateValue = translate(context, triple[2]);
                }

                if (triple[1].Equals("="))
                {
                    if (isSpecialConst(triple[2]))
                    {
                        querySparql += createEqTripleWithSortVar(translatePredicate, numVar);
                    }
                    else
                    {
                        querySparql += createEqTriple(translatePredicate, numVar, translateValue);
                    }
                }
                else
                {
                    querySparql += createCompareTriple(translatePredicate, numVar, comparisonSigns[triple[1].ToLower()], triple[2]);
                }
                numVar++;
            }

            querySparql += "}";

            if (sorting.Length != 0)
            {
                querySparql += " order by " + sorting + " limit 1";
            }

            return querySparql;
        }

        private bool isSpecialConst(String maybeConst)
        {
            return specialConst.ContainsKey(maybeConst);
        }


        private String createValues(List<String> values)
        {
            return "{" + String.Join(" ", values) + "}";
        }

        private String createHeader(List<String> values)
        {
            return "select distinct ?var1\nwhere {\n    values ?var2 " + createValues(values) + " .\n    ?var1 rdf:type ?var2 .\n";
        }

        //todo добавить разницу значений числовых, даты и типа dbr:Canada
        private String createEqTriple(List<String> values, int numPredicate, List<String> valuePredicate)
        {
            return "    values ?p" + numPredicate.ToString() + " " + createValues(values) + " .\n" +
                "    values ?v" + numPredicate.ToString() + " " + createValues(valuePredicate) + " .\n" +
                "    ?var1 ?p" + numPredicate.ToString() + " ?v" + numPredicate.ToString() + " .\n";
        }

        private String createEqTripleWithSortVar(List<String> predicates, int numPredicate)
        {
            return "    values ?p" + numPredicate.ToString() + " " + createValues(predicates) + " .\n" +
                "    ?var1 ?p" + numPredicate.ToString() + " ?s" + numPredicate.ToString() + " .\n";
        }

        private String createCompareTriple(List<String> predicates, int numPredicate, String comparisonSign, String comparisonValue)//TODO  
        {
            return "    values ?p" + numPredicate.ToString() + " " + createValues(predicates) + " .\n    ?var1 ?p" + numPredicate.ToString() + " ?var" + numPredicate.ToString() + " .\n"
                + "    filter (?var" + numPredicate.ToString() + " " + comparisonSign + " " + comparisonValue + ") . \n";
        }

        private List<String> translate(Linguistic_DatabaseContext context, String word)
        {
            return (from param in context.ParamsKreprToOntologies
                    join paramK in context.ParamsKreprs on param.IdKreprParam equals paramK.IdParam
                    join paramO in context.ParamsOntologies on param.IdOntologyParam equals paramO.IdParam
                    where paramK.Param.ToLower().Equals(word.ToLower())
                    select paramO.Param).ToList();
        }
    }
}
