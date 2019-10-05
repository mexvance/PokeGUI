using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Controls;
using OpenHtmlToPdf;
using PokeGUI.Models;

namespace PokeGUI.Services
{
    public class PokePdfService : IPokePdfService
    {
        public bool WritePdf(IEnumerable<Pokemon> pokemonCollection)
        {
            var htmlBuilder = new StringBuilder();
            htmlBuilder.Append(topOfPdf);

            foreach (var pokemon in pokemonCollection)
            {
                htmlBuilder.Append($@"
                    <tr>
                        <td class='no'>{pokemon.PokeId}</td>
                        <td class='desc'>{pokemon.Name}</td>
                        <td class='unit'>{pokemon.Type1}</td>
                        <td class='qty'>{pokemon.Type2}</td>
                        <td class='white'><img src='https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{pokemon.PokeId}.png' /></td>
                    </tr>");
            }

            htmlBuilder.Append(bottomOfPdf);
            var html = htmlBuilder.ToString();


            var pdf = Pdf
                .From(html)
                .OfSize(PaperSize.A4)
                .WithTitle("Your Pokemon")
                .WithoutOutline()
                .WithMargins(1.25.Centimeters())
                .Portrait()
                .Content();

            var fileName = @"..\..\..\..\Pokemon.pdf";

            File.WriteAllBytes(fileName, pdf);

            Process process = new Process();
            process.StartInfo.FileName = fileName;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            return true;
        }

        private string topOfPdf = @"
<!DOCTYPE html>
  <html>
  <head>
     <style>
        .clearfix:after {
            content: '';
            display: table;
            clear: both;
        }

        a {
            color: #0087C3;
            text-decoration: none;
        }

        body {
            position: relative;
            width: 21cm;
            height: 29.7cm;
            margin: 0 auto;
            color: #555555;
            background: #FFFFFF;
            font-size: 14px;
        }
        header {
            padding: 10px 0;
            margin-bottom: 20px;
            border-bottom: 1px solid #AAAAAA;
        }
        #client {
            padding-left: 6px;
            border-left: 6px solid #0087C3;
            float: left;
        }
        #invoice h1 {
            color: #0087C3;
            font-size: 2.4em;
            line-height: 1em;
            font-weight: normal;
            margin: 0 0 10px 0;
        }
        #invoice .date {
            font-size: 1.1em;
            color: #777777;
        }
        table {
            font-size: 1.5em;
            width: 100%;
            border-collapse: collapse;
            border-spacing: 0;
            margin-bottom: 20px;
        }
        table th,
        table td {
            padding: 5px;
            background: #EEEEEE;
            text-align: center;
            border-bottom: 1px solid #FFFFFF;
        }
    </style>
  </head>
  <body>
    <header class='clearfix'>
    </header>
    <main>
      <div id='details' class='clearfix'>
        <div id='client' >
           <img style='height: 7em;' src='https://proxy.duckduckgo.com/iu/?u=https%3A%2F%2Ftse4.mm.bing.net%2Fth%3Fid%3DOIP.JOLMiUAW3JyI6ZhX0vXWBgHaHa%26pid%3DApi&f=1' />
          </div>
        <div id='invoice'>
          <h1>PokeList!</h1>
          <div class='date'>Due Date: 10/5/2019</div>
        </div>
      </div>
      <table cellspacing='0' cellpadding='0'>
        <thead>
          <tr>
            <th class='no'>#</th>
            <th class='desc'>Name</th>
            <th class='unit'>Type1</th>
            <th class='qty'>Type2</th>
            <th class='white'>IMG</th>
          </tr>
        </thead>
    <tbody>";
        private string bottomOfPdf = @"</tbody>
</table>
</body>
</html>";

    }
}