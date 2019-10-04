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
                return new ObservableCollection<Pokemon>(FilterPokemonByName(FilterPokemonByType()));
            }
        }

        public IEnumerable<Pokemon> FilterPokemonByName(List<Pokemon> pokeList)
        {
            if (string.IsNullOrEmpty(PokemonNameFilter) == false)
            {
                return pokeList.FindAll(p => p.Name.StartsWith(PokemonNameFilter));
            }
            else
            {
                return pokeList;
            }
        }
        public List<Pokemon> FilterPokemonByType()
        {
            return SelectedPokeType != null
                ? PokemonCollection.FindAll(p =>
                        p.Type1 == SelectedPokeType || (p.Type2 != null ? p.Type2 == SelectedPokeType : false))
                : PokemonCollection;
        }

    }
}
