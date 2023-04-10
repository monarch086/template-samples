using System;
using System.Threading.Tasks;

namespace Stardog
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("~~~Stardog example~~~");

            var rdfProvider = new RdfProvider();

            // rdfProvider.ReadCities();

            var city = "Ladyzhyn";
            var cityName = "Ладижин";

            await rdfProvider.InsertCity(city, cityName);
            // var result = rdfProvider.GetCity(cityName);

            // Console.WriteLine(">>> RESULT: {0}", result);
        }
    }
}
