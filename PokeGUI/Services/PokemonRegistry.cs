using Newtonsoft.Json.Linq;
using PokeGUI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokeGUI.Services
{
    public class PokemonRegistry : IPokemonRegistry
    {
        public async Task<IEnumerable<Pokemon>> GetAllPokemonAsync()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://pokeapi.co/api/v2/pokemon?offset=0&limit=10");
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonPokeList = JObject.Parse(responseString)["results"];

            var pokemonCollection = new List<Pokemon>();
            foreach (var jsonPokemon in jsonPokeList)
            {
                pokemonCollection.Add(await CreatePokemonObject(jsonPokemon["name"].ToString(), jsonPokemon["url"].ToString()));
            }
            return pokemonCollection;
        }
        public async Task<Pokemon> CreatePokemonObject(string name, string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonPokeTypes = JObject.Parse(responseString)["types"];
            var pokeId = JObject.Parse(responseString)["id"].ToString();
            var types = new List<PokeType>();
            foreach (var pokeType in jsonPokeTypes)
            {
                types.Add(new PokeType(pokeType["type"]["name"].ToString()));
            }
            var pokemon = new Pokemon
            {
                Name = name,
                PokeId = Int32.Parse(pokeId),
                Type1 = types[0],
                Type2 = types.Count > 1 ? types[1] : null
            };

            return pokemon;
        }
    }
}
