using System;

namespace Stardog
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var rdfProvider = new RdfProvider();

            rdfProvider.ReadCities();
        }
    }
}
