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

        public Task<ICollection<Pokemon>> GetAllPokemonAsync()
        {
            var task = Task<ICollection<Pokemon>>.Factory.StartNew(() =>
            {
                var jsonFile = File.ReadAllText("../../../../pokeList.json");
                var pokelist = JsonConvert.DeserializeObject<List<Pokemon>>(jsonFile);
                return pokelist;
            });


            return task;
        }
    }
}
