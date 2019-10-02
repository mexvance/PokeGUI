using PokeGUI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokeGUI.Services
{
    public class PokemonRegistry
    {
        public async Task<List<Pokemon>> GetAllPokemonAsync()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://pokeapi.co/api/v2/pokemon?offset=0&limit=10");
            Console.WriteLine(response.Content.ReadAsStringAsync());
            return null; 
        }
    }
}
