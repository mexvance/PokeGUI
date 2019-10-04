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
            PokemonCollection = new List<Pokemon>(await pokemonRegistry.GetAllPokemonAsync());

        }


        private string pokemonNameFilter;
        public string PokemonNameFilter
        {
            get => pokemonNameFilter;
            set
            {
                SetProperty(ref pokemonNameFilter, value);
                RaisePropertyChanged(nameof(PokemonFilteredCollection));
            }
        }



        private List<Pokemon> pokemonCollection;
        public List<Pokemon> PokemonCollection
        {
            get { return pokemonCollection; }
            set { 
                SetProperty(ref pokemonCollection, value);
                RaisePropertyChanged(nameof(PokemonFilteredCollection));
            }
        }

        private PokeType selectedPokeType;

        public PokeType SelectedPokeType
        {
            get { return selectedPokeType; }
            set {
                SetProperty(ref selectedPokeType, value);
                RaisePropertyChanged(nameof(PokemonFilteredCollection));
            }
        }



        public ObservableCollection<Pokemon> PokemonFilteredCollection
        {
            get
            {
                return FilteredPokemon();
            }
        }

        public ObservableCollection<Pokemon> FilteredPokemon()
        {
            return new ObservableCollection<Pokemon>( 
                PokemonCollection.FindAll(p => p.Name.StartsWith(PokemonNameFilter))
                );
        }

    }
}
