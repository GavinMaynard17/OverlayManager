using OverlayManager.Models;
using OverlayManager.Stores;
using OverlayManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OverlayManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Match _match;
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _match = new Match();
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateGameSelectionViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private GameSelectionViewModel CreateGameSelectionViewModel()
        {
            return new GameSelectionViewModel(_match,
                new Services.NavigationService(_navigationStore, CreateMatchDetailViewModel),
                new Services.NavigationService(_navigationStore, CreateCasterListViewModel));
        }

        private ViewModelBase CreateCasterListViewModel()
        {
            return new CasterListViewModel(_match,
                new Services.NavigationService(_navigationStore, CreateGameSelectionViewModel));
        }

        private MatchDetailViewModel CreateMatchDetailViewModel()
        {
            return new MatchDetailViewModel(_match,
                new Services.NavigationService(_navigationStore, CreateMatchControlViewModel), 
                new Services.NavigationService(_navigationStore, CreateGameSelectionViewModel));
        }

        private MatchControlViewModel CreateMatchControlViewModel()
        {
            return new MatchControlViewModel(_match, 
                new Services.NavigationService(_navigationStore, CreateGameSelectionViewModel));
        }
    }
}
