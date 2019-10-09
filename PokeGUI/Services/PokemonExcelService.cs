using OfficeOpenXml;
using PokeGUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PokeGUI.Services
{
    public class PokemonExcelService : IPokemonExcelService
    {
        public FileInfo filterFileInfo { get; set; }
        public string pokeNameFilter { get; set; }
        public PokeType pokeTypeFilter { get; set; }
        public (string, PokeType) getStoredFilter()
        {
            selectFilterFile();
            readValuesFromFilterFile();

            return (pokeNameFilter, pokeTypeFilter);
        }

        private void readValuesFromFilterFile()
        {
            pokeNameFilter = string.Empty;
            pokeTypeFilter = new PokeType("none");

            using (var filterPackage = new ExcelPackage(filterFileInfo))
            {
                var worksheet = filterPackage.Workbook.Worksheets[0];
                pokeNameFilter = worksheet.Cells["B1"].Value.ToString();
                pokeTypeFilter = new PokeType(worksheet.Cells["B2"].Value.ToString());
            }
        }

        private void selectFilterFile()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel |*.xl*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filterFileInfo = new FileInfo(openFileDialog.FileName);
            }
        }
    }
}
