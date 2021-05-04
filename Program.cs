using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ConsoleTables;

namespace ApiClient
{
    class Brewery
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("brewery_type")]
        public string BreweryType { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }


    }
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();

            var responseAsStream = await client.GetStreamAsync("https://api.openbrewerydb.org/breweries");
            var breweries = await JsonSerializer.DeserializeAsync<List<Brewery>>(responseAsStream);

            var table = new ConsoleTable("Brewery", "Type", "City", "State", "Phone");

            foreach (var brewery in breweries)
            {
                table.AddRow(brewery.Name, brewery.BreweryType, brewery.City, brewery.State, brewery.Phone);
            }

            table.Write();

        }
    }
}