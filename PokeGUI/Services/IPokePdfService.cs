using System.Collections.Generic;
using PokeGUI.Models;

namespace PokeGUI.Services
{
    public interface IPokePdfService
    {
        bool WritePdf(IEnumerable<Pokemon> pokemonCollection);
    }
}