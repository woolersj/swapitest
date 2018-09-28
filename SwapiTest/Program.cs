using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SwapiTest
{
    class Program
    {
        static readonly SwapiClient SwapiClient = new SwapiClient();

        static void Main(string[] args)
        {
            Console.WriteLine("The Star Wars API");

            Console.WriteLine("Need a hint? try people/1/ or planets/3/ or starships/9/");

            string input;
            while ((input = Console.ReadLine()) != "Quit")
            {
                try
                {
                    ProcessRequest(input).Wait();
                }
                catch (AggregateException ae)
                {
                    ae.Handle(x =>
                    {
                        if (x is HttpRequestException) // This we know how to handle.
                        {
                            Console.WriteLine(x.Message);
                            Console.WriteLine("See your network administrator or try another path.");
                            return true;
                        }

                        return false; // Let anything else stop the application.
                    });
                }
            }
        }

        private static async Task ProcessRequest(string relativeUri)
        {
            var response = await SwapiClient.GetDataAsync(relativeUri);
            Console.WriteLine(JToken.Parse(response).ToString(Formatting.Indented));
        }

    }
}
