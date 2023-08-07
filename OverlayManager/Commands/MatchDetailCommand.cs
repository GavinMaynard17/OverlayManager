using OverlayManager.Models;
using OverlayManager.Services;
using OverlayManager.ViewModels;
using Reservoom.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayManager.Commands
{
    public class MatchDetailCommand : CommandBase
    {
        private readonly MatchDetailViewModel _matchDetailViewModel;
        private readonly Match _match;
        private readonly NavigationService _matchControlNavigationService;

        public MatchDetailCommand(MatchDetailViewModel matchDetailViewModel, Match match, NavigationService matchControlNavigationService)
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

        public override void Execute(object? parameter)
        {
            //launch an overlay
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
