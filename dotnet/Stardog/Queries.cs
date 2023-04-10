namespace Stardog;

public static class Queries
{
    public static string GET_CITIES = @"
    PREFIX : <http://stardog.com/tutorial/>

    SELECT ?s ?n
    WHERE {
        ?s a :City ;
        :hasName ?n .
    }";
}