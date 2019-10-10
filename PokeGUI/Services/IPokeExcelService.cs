using PokeGUI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeGUI.Services
{
    public interface IPokeExcelService
    {
        bool WriteExcelSheet(IEnumerable<Pokemon> pokemonCollection);

        (string, PokeType) getFilterType();
    }
}
