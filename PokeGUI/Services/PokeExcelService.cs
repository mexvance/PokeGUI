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
