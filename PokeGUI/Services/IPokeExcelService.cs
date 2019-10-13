using PokeGUI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PokeGUI.Services
{
    public interface IPokeExcelService
    {
        bool WriteExcelSheet(IEnumerable<Pokemon> pokemonCollection);

       Task<List<Pokemon>> getPokemonCollection();
    }
}
