using Newtonsoft.Json;
using PokeGUI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestIntegration
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var pokeExcel = new PokemonExcelService();
            pokeExcel.getStoredFilter();



            //var dummy = new DummyPokemonRegistry();
            //var waitThis = dummy.GetAllPokemonAsync();
            //waitThis.Wait();
            //var pokelist = waitThis.Result;

            ////real call
            ////var pokeReg = new PokemonRegistry();
            ////var test = pokeReg.GetAllPokemonAsync();
            ////test.Wait();
            ////var res = test.Result;
            ////var list = JsonConvert.SerializeObject(res);

            //var pdf = new PokePdfService();
            //pdf.WritePdf(pokelist);
        }
    }
}
