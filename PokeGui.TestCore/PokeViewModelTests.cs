using FluentAssertions;
using Moq;
using NUnit.Framework;
using PokeGUI.Models;
using PokeGUI.Services;
using PokeGUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokeTest.TestCore
{
    public class PokeViewModelTests
    {
        [Test]
        public void filterPokemonByName()
        {
            var samplePokemonList = new List<Pokemon>();
            samplePokemonList.Add(new Pokemon {
                Name="aaa"
            });
            samplePokemonList.Add(new Pokemon {
                Name="bbb"
            });
            samplePokemonList.Add(new Pokemon {
                Name="ccc"
            });
            var mockPokeRegistry = new Mock<IPokemonRegistry>();
            mockPokeRegistry.Setup(r => r.GetAllPokemonAsync()).ReturnsAsync(samplePokemonList);

            var pokedexViewModel = new PokedexViewModel(mockPokeRegistry.Object);
            //Thread.Sleep(500); //to async load pokemon list

            pokedexViewModel.PokemonNameFilter = "b";

            pokedexViewModel.PokemonFilteredCollection.Count.Should().Be(1);
            pokedexViewModel.PokemonFilteredCollection.First().Name.Should().Be("bbb");
        }
    }

}
