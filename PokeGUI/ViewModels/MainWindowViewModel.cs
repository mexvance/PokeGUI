using PokeGUI.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeGUI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        public MainWindowViewModel(IRegionManager regionManager) : base()
        {
            
            this.regionManager = regionManager;
            //regionManager.AddToRegion("ContentRegion", "PokedexView");
            //.RegisterViewWithRegion("ContentRegion", typeof(PokedexView));
        }
        private DelegateCommand goToPokedex;

        public DelegateCommand GoToPokedex => goToPokedex ?? (goToPokedex = new DelegateCommand(
                ()=>
                {
                    regionManager.RequestNavigate("ContentRegion", "PokedexView");
                }
            ));


    }
}
