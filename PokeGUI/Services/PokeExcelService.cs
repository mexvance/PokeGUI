using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OfficeOpenXml;
using System.Web;
using PokeGUI.Models;
using System.Windows.Forms;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Threading.Tasks;

namespace PokeGUI.Services
{
    class PokeExcelService : IPokeExcelService
    {
        public FileInfo fileName { get; set; }
        public async Task<List<Pokemon>> getPokemonCollection()
        {
            var service = new PokemonRegistry();
            var pokeNameFilter = string.Empty;
            var pokeImageUrl = string.Empty;
            var pokeList = new List<Pokemon>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel |*.xlsx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = new FileInfo(openFileDialog.FileName);
            }
            FileInfo file = new FileInfo(fileName.FullName);

            using (var filteredPackage = new ExcelPackage(file))
            {
                var worksheet = filteredPackage.Workbook.Worksheets[0];
                for (int row = 4; row <= worksheet.Dimension.End.Row ; row++) {
                    pokeNameFilter = worksheet.Cells[row, 2].Value.ToString();
                    pokeImageUrl = "https://pokeapi.co/api/v2/pokemon/"+pokeNameFilter;
                    var pokemon = await service.CreatePokemonObject(pokeNameFilter, pokeImageUrl);
                    pokeList.Add(pokemon);
                }
            }
            return pokeList;
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

                worksheet.Cells[1, 1].Value = "Pokemon List";
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
                    worksheet.Cells[row, 5].Value = pokemon.Image;
                    row++;
                }

                worksheet.Cells["A1:E2"].Merge = true;
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A1"].Style.Font.Color.SetColor(Color.Blue);
                worksheet.Cells["A1"].Style.Font.Size = 26.0F;
                worksheet.Cells["A1"].Style.ShrinkToFit = true;

                worksheet.Cells[worksheet.Dimension.Address].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                worksheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.LightGray;
                worksheet.Cells["A2:E2"].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                SaveFile(newPackage);
            }
            return false;
        }
        private bool SaveFile(ExcelPackage package)
        {
            Stream myStream;
            SaveFileDialog saveDlg = new SaveFileDialog();

            saveDlg.Filter = "Excel |*.xlsx";
            saveDlg.FilterIndex = 2;
            saveDlg.FileName = "pokeList.xlsx";
            saveDlg.RestoreDirectory = true;

            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveDlg.OpenFile()) != null)
                {

                    var file = new FileInfo(saveDlg.FileName);
                    myStream.Write(package.GetAsByteArray());
                    myStream.Close();
                }
            }
            return false;
        }
    }
}
