using System;
using VDS.RDF.Query;
using VDS.RDF.Storage;

namespace Stardog;

internal class RdfProvider
{
    const string SERVER_URL = "http://localhost:5820";
    const string STARDOG_USERNAME = "admin";
    const string STARDOG_PASSWORD = "admin";
    const string DATABASE_NAME = "resumes";

    public void ReadCities()
    {
        using (var stardogConn = new StardogConnector(SERVER_URL, DATABASE_NAME, STARDOG_USERNAME, STARDOG_PASSWORD))
        {
            object results = stardogConn.Query(Queries.GET_CITIES);
            if (results is SparqlResultSet)
            {
                SparqlResultSet rset = (SparqlResultSet)results;
                foreach (SparqlResult r in rset)
                {
                    Console.WriteLine("RESULT: {0}", r.ToString());

                    Console.WriteLine("n: {0}", r["s"]);

                    Console.WriteLine("NodeType: {0}", r["s"].NodeType);
                }
            }
            else
            {
                throw new Exception("Result was not a SPARQL result set");
            }
        }
    }
}