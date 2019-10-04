using PokeGUI.Models;
using PokeGUI.Services;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PokeGUI.ViewModels
{
    public class PokedexViewModel : BindableBase
    {
        private readonly IPokemonRegistry pokemonRegistry;

        public PokedexViewModel(IPokemonRegistry pokemonRegistry)
        {
            this.pokemonRegistry = pokemonRegistry;
            LoadAsync();
        }
        public async void LoadAsync()
        {
            PokemonCollection = new ObservableCollection<Pokemon>(await pokemonRegistry.GetAllPokemonAsync());

        }

        private ObservableCollection<Pokemon> pokemonCollection;

        public ObservableCollection<Pokemon> PokemonCollection
        {
            get { return pokemonCollection; }
            set { 
                SetProperty(ref pokemonCollection, value);
            }
        }

    }
}
