using System;
using System.Linq;
using System.Threading.Tasks;
using VDS.RDF;
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

                    Console.WriteLine("n: {0}", r["n"].ToString());

                    //Console.WriteLine("Value: {0}", r.Value("n"));

                    Console.WriteLine("NodeType: {0}", r["n"].NodeType);
                }
            }
            else
            {
                throw new Exception("Result was not a SPARQL result set");
            }
        }
    }

    public async Task InsertCity(string city, string cityName)
    {
        Graph g = new Graph();

        g.NamespaceMap.AddNamespace("rdf", new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#"));
        g.NamespaceMap.AddNamespace("tutorial", new Uri(Queries.NAMESPACE));

        var rdfNs = g.NamespaceMap.GetNamespaceUri("rdf");
        var tutorialNs = g.NamespaceMap.GetNamespaceUri("tutorial");

        INode s = g.CreateUriNode(new Uri(tutorialNs + city));
        INode p = g.CreateUriNode(new Uri(rdfNs + "type"));
        INode o = g.CreateUriNode(new Uri(tutorialNs + "City"));

        await AddTripple(g, s, p, o);

        p = g.CreateUriNode(new Uri(tutorialNs + "hasName"));
        o = g.CreateLiteralNode(cityName);

        await AddTripple(g, s, p, o);
    }

    public string GetCity(string cityName)
    {
        using (var stardogConn = new StardogConnector(SERVER_URL, DATABASE_NAME, STARDOG_USERNAME, STARDOG_PASSWORD))
        {
            object results = stardogConn.Query(Queries.GetCity(cityName));
            if (results is SparqlResultSet)
            {
                SparqlResultSet rset = (SparqlResultSet)results;

                return rset.FirstOrDefault()["s"].ToSafeString();
            }
            else
            {
                throw new Exception("Result was not a SPARQL result set");
            }
        }
    }

    private async Task AddTripple(Graph g, INode s, INode p, INode o)
    {
        using (var stardogConn = new StardogConnector(SERVER_URL, DATABASE_NAME, STARDOG_USERNAME, STARDOG_PASSWORD))
        {
            var triple = new Triple(s, p, o);
            g.Assert(triple);

            if (!stardogConn.IsReadOnly)
            {
                await stardogConn.SaveGraphAsync(g, default);
            }
        }
    }
}