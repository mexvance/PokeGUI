using PokeGUI.Models;

namespace PokeGUI.Services
{
    public interface IPokemonExcelService
    {
        (string, PokeType) getStoredFilter();
    }
}