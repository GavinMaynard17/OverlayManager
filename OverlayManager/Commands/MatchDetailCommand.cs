using OverlayManager.LeagueOfLegendsOverlay.ViewModels;
using OverlayManager.LeagueOfLegendsOverlay.Views;
using OverlayManager.Models;
using OverlayManager.OverwatchOverlay.ViewModels;
using OverlayManager.OverwatchOverlay.Views;
using OverlayManager.RocketLeagueOverlay.ViewModels;
using OverlayManager.RocketLeagueOverlay.Views;
using OverlayManager.Services;
using OverlayManager.ValorantOverlay.ViewModels;
using OverlayManager.ValorantOverlay.Views;
using OverlayManager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Printing;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace OverlayManager.Commands
{
    public class MatchDetailCommand : CommandBase
    {
        private readonly MatchDetailViewModel _matchDetailViewModel;
        private readonly Match _match;
        private readonly NavigationService _matchControlNavigationService;
        

        public MatchDetailCommand(MatchDetailViewModel matchDetailViewModel,
            Match match,
            NavigationService matchControlNavigationService)
        {
            _matchDetailViewModel = matchDetailViewModel;
            _match = match;
            _matchControlNavigationService = matchControlNavigationService;
            _matchDetailViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_matchDetailViewModel.Team1Name) &&
                !string.IsNullOrEmpty(_matchDetailViewModel.Team2Name) &&
                _match.SeriesLength != 0 &&
                base.CanExecute(parameter);
        }

        public override async void Execute(object? parameter)
        {
            //launch an overlay

            switch (_match.Game) 
            {
                case "Rocket League":
                    RocketLeagueViewModel rocketLeagueViewModel = new RocketLeagueViewModel();
                    RocketLeagueView rocketLeagueView = new RocketLeagueView();
                    rocketLeagueView.DataContext = rocketLeagueViewModel;
                    rocketLeagueView.Show();                    
                    break;

                case "Valorant":
                    ValorantViewModel valorantViewModel = new ValorantViewModel();
                    ValorantView valorantView = new ValorantView();
                    valorantView.DataContext = valorantViewModel;

                    valorantView.Show();
                    break;

                case "Overwatch":
                    OverwatchViewModel overwatchViewModel = new OverwatchViewModel();
                    OverwatchView overwatchView = new OverwatchView();
                    overwatchView.DataContext = overwatchViewModel;

                    overwatchView.Show();
                    break;

                case "League Of Legends":
                    LeagueOfLegendsViewModel leagueOfLegendsViewModel = new LeagueOfLegendsViewModel();
                    LeagueOfLegendsView leagueOfLegendsView = new LeagueOfLegendsView();
                    leagueOfLegendsView.DataContext = leagueOfLegendsViewModel;

                    leagueOfLegendsView.Show();
                    break;
            }

            

            _matchControlNavigationService.Navigate();
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MatchDetailViewModel.Team1Name) ||
                e.PropertyName == nameof(MatchDetailViewModel.Team2Name) ||
                e.PropertyName == nameof(MatchDetailViewModel.SeriesLength)) 
            {
                OnCanExecutedChanged();
            }
        }
    }
}
