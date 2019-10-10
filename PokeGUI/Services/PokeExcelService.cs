using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OfficeOpenXml;
using System.Web;
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

                ExcelWorksheet worksheet = newPackage.Workbook.Worksheets.Add("PokeList");

                worksheet.Cells[2, 1].Value = "Pokemon List";
                worksheet.Cells[3, 1].Value = "ID";
                worksheet.Cells[3, 2].Value = "Name";
                worksheet.Cells[3, 3].Value = "Type1";
                worksheet.Cells[3, 4].Value = "Type2";
                worksheet.Cells[3, 5].Value = "Image Link";

                var row = 4;
                foreach (var pokemon in pokemonCollection)
                {
                    worksheet.Cells[row, 1].Value = pokemon.PokeId;
                    worksheet.Cells[row, 2].Value = pokemon.Name;
                    worksheet.Cells[row, 3].Value = pokemon.Type1;
                    worksheet.Cells[row, 4].Value = pokemon.Type2;
                    //using (System.Drawing.Image image = System.Drawing.Image.FromFile(HttpU))
                    //{
                    //    var excelImage = worksheet.Drawings.AddPicture("My Logo", pokemon.Image);

                    //    //add the image to row 20, column E
                    //    excelImage.SetPosition(20, 0, 5, 0);
                    //}
                    worksheet.Cells[row, 5].Value = pokemon.Image;
                }
                FileInfo file = new FileInfo("pokeList.xlsx");
                newPackage.SaveAs(file);
            }
            return false;
        }
    }
}
