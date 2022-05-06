using System;
using System.Collections.Generic;
using VDS.RDF.Query;

namespace NL_text_representation.SPARQL
{
    public class SparqlRunner
    {
        public List<String> runQuery(String commandText)
        {
            SparqlParameterizedString queryString = new SparqlParameterizedString();

            queryString.Namespaces.AddNamespace("dbo", new Uri("http://dbpedia.org/ontology/"));
            queryString.Namespaces.AddNamespace("dbp", new Uri("http://dbpedia.org/property/"));
            queryString.Namespaces.AddNamespace("dbr", new Uri("http://dbpedia.org/resource/"));
            queryString.Namespaces.AddNamespace("rdf", new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#"));
            queryString.Namespaces.AddNamespace("schema", new Uri("http://schema.org/"));

            queryString.CommandText = commandText;

            SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"), "http://dbpedia.org");
            SparqlResultSet queryResults = endpoint.QueryWithResultSet(queryString.ToString());

            List<String> results = new List<String>();

            foreach (SparqlResult row in queryResults)
            {
                results.Add(row.Value("var1").ToString());
            }

            return results;
        }
    }
}
