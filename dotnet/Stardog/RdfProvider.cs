using System;
using System.Linq;
using VDS.RDF.Query;
using VDS.RDF.Storage;
using VDS.RDF.Storage.Management;

namespace Stardog;

internal class RdfProvider
{
    const string SERVER_URL = "http://localhost:5820";
    const string STARDOG_USERNAME = "admin";
    const string STARDOG_PASSWORD = "admin";
    const string DATABASE_NAME = "resumes";

    public void Read()
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
                }
            }
            else
            {
                throw new Exception("Result was not a SPARQL result set");
            }
        }
    }
}