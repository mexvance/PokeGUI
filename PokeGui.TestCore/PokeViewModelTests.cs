using FluentAssertions;
using Moq;
using NUnit.Framework;
using PokeGUI.Services;
using PokeGUI.Models;
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
        private List<Pokemon> samplePokemonList;
        private Mock<IPokemonRegistry> mockPokeRegistry;
        private Mock<IPokePdfService> mockPdfService;

        public PokeTypeRegistry pokeTypeConstants { get; private set; }

        [SetUp]
        public void setup()
        {
            pokeTypeConstants = new PokeTypeRegistry();
            mockPdfService = new Mock<IPokePdfService>();
            samplePokemonList = new List<Pokemon>();
            samplePokemonList.Add(new Pokemon
            {
                Name = "aaa",
                Type1 = pokeTypeConstants.Fire
            });
            samplePokemonList.Add(new Pokemon
            {
                Name = "bbb",
                Type1 = pokeTypeConstants.Water
            });
            samplePokemonList.Add(new Pokemon
            {
                Name = "ccc",
                Type1 = pokeTypeConstants.Dragon
            });
            samplePokemonList.Add(new Pokemon
            {
                Name = "ddd",
                Type1 = pokeTypeConstants.Electric
            });
            samplePokemonList.Add(new Pokemon
            {
                Name = "dab",
                Type1 = pokeTypeConstants.Dark,
                Type2 = pokeTypeConstants.Electric

            });
            samplePokemonList.Add(new Pokemon
            {
                Name = "dac",
                Type1 = pokeTypeConstants.Electric,
                Type2 = pokeTypeConstants.Bug
            });
            mockPokeRegistry = new Mock<IPokemonRegistry>();
            mockPokeRegistry.Setup(r => r.GetAllPokemonAsync()).ReturnsAsync(samplePokemonList);
        }

        [Test]
        public void filterPokemonByName()
        {
            var pokedexViewModel = new PokedexViewModel(mockPokeRegistry.Object, pokeTypeConstants, mockPdfService.Object);
            pokedexViewModel.LoadPokemonTask.Wait();


            pokedexViewModel.PokemonNameFilter = "b";

            pokedexViewModel.PokemonFilteredCollection.Count.Should().Be(1);
            pokedexViewModel.PokemonFilteredCollection.First().Name.Should().Be("bbb");
        }

        [Test]
        public void CanFilterPokemonByType()
        {
            var pokedexViewModel = new PokedexViewModel(mockPokeRegistry.Object, pokeTypeConstants, mockPdfService.Object);
            pokedexViewModel.LoadPokemonTask.Wait();

            pokedexViewModel.SelectedPokeType = pokeTypeConstants.Dragon;


            pokedexViewModel.PokemonFilteredCollection.Count.Should().Be(1);
            pokedexViewModel.PokemonFilteredCollection.First().Name.Should().Be("ccc");
        }


        [Test]
        public void CanFilterByTypeAndName()
        {
            var pokedexViewModel = new PokedexViewModel(mockPokeRegistry.Object, pokeTypeConstants, mockPdfService.Object);
            pokedexViewModel.LoadPokemonTask.Wait();
            pokedexViewModel.PokemonNameFilter = "d";
            pokedexViewModel.SelectedPokeType = pokeTypeConstants.Electric;

            pokedexViewModel.PokemonFilteredCollection.Count.Should().Be(3);
            pokedexViewModel.PokemonFilteredCollection.Should().Contain(p => p.Name == "ddd");
            pokedexViewModel.PokemonFilteredCollection.Should().Contain(p => p.Name == "dab");
            pokedexViewModel.PokemonFilteredCollection.Should().Contain(p => p.Name == "dac");
        }
    }

}
