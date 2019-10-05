using FluentAssertions;
using Moq;
using NUnit.Framework;
using PokeGUI.Data;
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
        private List<Pokemon> samplePokemonList;
        private Mock<IPokemonRegistry> mockPokeRegistry;
        [SetUp]
        public void setup()
        {
            samplePokemonList = new List<Pokemon>();
            samplePokemonList.Add(new Pokemon
            {
                Name = "aaa",
                Type1 = PokeTypeConstants.Fire
            });
            samplePokemonList.Add(new Pokemon
            {
                Name = "bbb",
                Type1 = PokeTypeConstants.Water
            });
            samplePokemonList.Add(new Pokemon
            {
                Name = "ccc",
                Type1 = PokeTypeConstants.Dragon
            });
            samplePokemonList.Add(new Pokemon
            {
                Name = "ddd",
                Type1 = PokeTypeConstants.Electric
            });
            samplePokemonList.Add(new Pokemon
            {
                Name = "dab",
                Type1 = PokeTypeConstants.Dark,
                Type2 = PokeTypeConstants.Electric

            });
            samplePokemonList.Add(new Pokemon
            {
                Name = "dac",
                Type1 = PokeTypeConstants.Electric,
                Type2 = PokeTypeConstants.Bug
            });
            mockPokeRegistry = new Mock<IPokemonRegistry>();
            mockPokeRegistry.Setup(r => r.GetAllPokemonAsync()).ReturnsAsync(samplePokemonList);
        }

        [Test]
        public void filterPokemonByName()
        {
            var pokedexViewModel = new PokedexViewModel(mockPokeRegistry.Object);
            //Thread.Sleep(500); //to async load pokemon list

            pokedexViewModel.PokemonNameFilter = "b";

            pokedexViewModel.PokemonFilteredCollection.Count.Should().Be(1);
            pokedexViewModel.PokemonFilteredCollection.First().Name.Should().Be("bbb");
        }

        [Test]
        public void CanFilterPokemonByType()
        {
            var pokedexViewModel = new PokedexViewModel(mockPokeRegistry.Object);

            pokedexViewModel.SelectedPokeType = PokeTypeConstants.Dragon;


            pokedexViewModel.PokemonFilteredCollection.Count.Should().Be(1);
            pokedexViewModel.PokemonFilteredCollection.First().Name.Should().Be("ccc");
        }


        [Test]
        public void CanFilterByTypeAndName()
        {
            var pokedexViewModel = new PokedexViewModel(mockPokeRegistry.Object);
            pokedexViewModel.PokemonNameFilter = "a";
            pokedexViewModel.SelectedPokeType = PokeTypeConstants.Electric;

            pokedexViewModel.PokemonFilteredCollection.Count.Should().Be(3);
            pokedexViewModel.PokemonFilteredCollection.Should().Contain(p => p.Name == "ddd");
            pokedexViewModel.PokemonFilteredCollection.Should().Contain(p => p.Name == "dab");
            pokedexViewModel.PokemonFilteredCollection.Should().Contain(p => p.Name == "dac");
        }
    }

}
