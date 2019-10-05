using System;
using System.Collections.Generic;
using System.Text;
using IronPdf;


namespace PokeGUI.Services
{
    public class PokePdfService 
    {
        public bool WritePdf()
        {
            var document = new HtmlToPdf();
            var pdf = document.RenderHtmlAsPdf("<h1>hello world</h1>");
            var file = "htmlToPdf.pdf";
            pdf.Print();
            //System.Diagnostics.Process.Start(file);
            return true;
        }
    }
}