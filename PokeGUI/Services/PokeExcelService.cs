using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OfficeOpenXml;
using PokeGUI.Models;

namespace PokeGUI.Services
{
    class PokeExcelService : IPokeExcelService
    {
        public (string, PokeType) getFilterType()
        {
            var pokeNameFilter = string.Empty;
            var pokeTypeFilter = new PokeType("none");
            var fileName = "C:/Users/mexva/source/repos/PokeGUI/PokeGUI/Excell/PokeSearchFilter.xlsx";
            FileInfo file = new FileInfo(fileName);
            using (var filteredPackage = new ExcelPackage(file))
            {
                var worksheet = filteredPackage.Workbook.Worksheets[0];
                pokeNameFilter = worksheet.Cells["A2"].Value.ToString();
                pokeTypeFilter = new PokeType(worksheet.Cells["B2"].Value.ToString());
            }
            return (pokeNameFilter, pokeTypeFilter);
        }

        public bool WriteExcelSheet(IEnumerable<Pokemon> pokemonCollection)
        {
            using (var newPackage = new ExcelPackage())
            {
                newPackage.Workbook.Properties.Author = "KaydMike";
                newPackage.Workbook.Properties.Title = "Pokemon By Type";
                newPackage.Workbook.Properties.Subject = "Pokemon";
                newPackage.Workbook.Properties.Created = DateTime.Now;
            }
        }
    }
}
