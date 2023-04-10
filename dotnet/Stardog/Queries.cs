using System;

namespace Stardog;

public static class Queries
{
    public static string NAMESPACE = "http://stardog.com/tutorial/";

    public static string GET_CITIES = @$"
    PREFIX : <{NAMESPACE}>

    SELECT ?s ?n
    WHERE {{
        ?s a :City ;
        :hasName ?n .
    }}";

    public static string GetCity(string city) => @$"
    PREFIX : <{NAMESPACE}>

    SELECT ?s ?n
    WHERE {{
        ?s a :City ;
        :hasName {city} .
    }}";
}