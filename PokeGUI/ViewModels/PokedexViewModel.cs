using PokeGUI.Services;
using PokeGUI.Models;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using guiWapper1;
using System.Windows;

namespace PokeGUI.ViewModels
{
    public class PokedexViewModel : BindableDataErrorInfoBase
    {
        private readonly IPokemonRegistry pokemonRegistry;
        private readonly PokeTypeRegistry pokeTypeRegistry;

        public PokedexViewModel(IPokemonRegistry pokemonRegistry, PokeTypeRegistry pokeTypeRegistry)
        {
            this.pokemonRegistry = pokemonRegistry;
            this.pokeTypeRegistry = pokeTypeRegistry;
            LoadPokemonTask = LoadAsync();
        }
        
        public Task LoadAsync()
        {
            return Task.Run( async () =>
            {
                PokemonCollection = new List<Pokemon>(await pokemonRegistry.GetAllPokemonAsync());
                PokeTypes = pokeTypeRegistry.All();
                SelectedPokeType = pokeTypeRegistry.None;
            });

        }

        private Task loadPokemonTask;
        public Task LoadPokemonTask
        {
            get { return loadPokemonTask; }
            set {
                SetProperty(ref loadPokemonTask, value); 
            }
        }

        private List<PokeType> pokeTypes;
        public List<PokeType> PokeTypes
        {
            get { return pokeTypes; }
            set {
                SetProperty(ref pokeTypes, value); 
            }
        }


        private string pokemonNameFilter;
        public string PokemonNameFilter
        {
            get => pokemonNameFilter;
            set
            {


                SetProperty(ref pokemonNameFilter, value);
                RaisePropertyChanged(nameof(PokemonFilteredCollection));
                if (value.Contains(" "))
                {
                    NameError = "Name cannot have a space";
                }
                else if (PokemonFilteredCollection.Count <= 0)
                {
                    NameError = "There isn't a pokemon with these search values in your list";
                }
                else
                {
                    NameError = null;
                }

            }
        }

        private string nameError;
        public string NameError
        {
            get { return nameError; }
            set
            {
                SetProperty(ref nameError, value);
                ErrorDictionary[nameof(PokemonNameFilter)] = value;
                nameErrorVisibility = value?.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
                //RepoButtonDisable = RepoErrorVisibility.Equals(Visibility.Collapsed) ? false : true;
                //ErrorList = Error;
            }
        }


        private Visibility nameErrorVisibility;
        public Visibility NameErrorVisibility
        {
            get { return nameErrorVisibility; }
            set { SetProperty(ref nameErrorVisibility, value); }
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
                if (PokemonFilteredCollection.Count <= 0)
                {
                    NameError = "There isn't a pokemon with these search values in your list";
                }
                else
                {
                    NameError = null;
                }
            }
        }

        public ObservableCollection<Pokemon> PokemonFilteredCollection
        {
            get {
                var list1 = FilterPokemonByType();
                var list2 = FilterPokemonByName(list1);
                return new ObservableCollection<Pokemon>(list2 ?? new List<Pokemon>()); 
            }
        }

        public IEnumerable<Pokemon> FilterPokemonByName(List<Pokemon> pokeList)
        {
            return string.IsNullOrEmpty(PokemonNameFilter) == false
                ? pokeList.FindAll(p => p.Name.StartsWith(PokemonNameFilter))
                : pokeList;
        }
        public List<Pokemon> FilterPokemonByType()
        {
            if (PokemonCollection != null)
            {
                return SelectedPokeType != null
                   ? PokemonCollection.FindAll(p =>
                           p.Type1.TypeName == SelectedPokeType.TypeName
                           || (p.Type2 != null ? p.Type2.TypeName == SelectedPokeType.TypeName : false)
                           || SelectedPokeType.TypeName == "none")
                   : PokemonCollection;
            } 
            else
            {
                return new List<Pokemon>();
            }
           
        }
    }
}
