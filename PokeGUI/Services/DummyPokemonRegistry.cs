using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PokeGUI.Models;

namespace PokeGUI.Services
{
    public class DummyPokemonRegistry : IPokemonRegistry
    {
        public Task<Pokemon> CreatePokemonObject(string name, string url)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pokemon>> GetAllPokemonAsync()
        {
            var task = Task<IEnumerable<Pokemon>>.Factory.StartNew(() =>
            {
                var jsonFile = File.ReadAllText("../../../../pokeList.json");
                var pokelist = JsonConvert.DeserializeObject<List<Pokemon>>(jsonFile);
                foreach (var pokemon in pokelist)
                {
                    pokemon.Image = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{pokemon.PokeId}.png";
                }
                return pokelist;
            });


            return task;
        }
    }
}
