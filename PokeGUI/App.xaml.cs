using CommonServiceLocator;
using PokeGUI.Views;
using Prism.Ioc;
using Prism.Unity;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PokeGUI.Services;

namespace PokeGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return ServiceLocator.Current.GetInstance<MainWindowView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register(typeof(IPokemonRegistry), typeof(DummyPokemonRegistry));
            containerRegistry.Register(typeof(PokeTypeRegistry), typeof(PokeTypeRegistry));
            containerRegistry.RegisterForNavigation<PokedexView>();
            
        }

        protected override void ConfigureServiceLocator()
        {
            base.ConfigureServiceLocator();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
