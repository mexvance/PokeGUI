using PokeGUI.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace PokeGUI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        public MainWindowViewModel(IRegionManager regionManager) : base()
        {
            
            this.regionManager = regionManager;
            GoToPokedexVisibility = Visibility.Visible;
        }
        private DelegateCommand goToPokedex;

        public DelegateCommand GoToPokedex => goToPokedex ?? (goToPokedex = new DelegateCommand(
                ()=>
                {
                    regionManager.RequestNavigate("ContentRegion", "PokedexView");
                    GoToPokedexVisibility = Visibility.Collapsed;
                }
            ));

        private Visibility goToPokedexVisibility;

        public Visibility GoToPokedexVisibility
        {
            get { return goToPokedexVisibility; }
            set { SetProperty(ref goToPokedexVisibility, value); }
        }


    }
}
