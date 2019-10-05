using System.Collections.Generic;
using System.Threading.Tasks;
using PokeGUI.Models;

namespace PokeGUI.Services
{
    public interface IPokemonRegistry
    {
        Task<Pokemon> CreatePokemonObject(string name, string url);
        Task<IEnumerable<Pokemon>> GetAllPokemonAsync();
    }
}