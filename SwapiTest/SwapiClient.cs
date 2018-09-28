using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SwapiTest
{
    

    public class SwapiClient
    {
        private readonly HttpClient httpClient = new HttpClient();

        private readonly Uri baseUri = new Uri("http://swapi.co/api/");

        public async Task<string> GetDataAsync(string relativeUri)
        {
            return await httpClient.GetStringAsync(new Uri(baseUri, relativeUri));
        }
    }
}
