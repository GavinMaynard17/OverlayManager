using OverlayManager.Models;
using OverlayManager.Services;
using OverlayManager.ViewModels;
using Reservoom.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayManager.Commands
{
    public class SelectGameCommand : CommandBase
    {
        private readonly GameSelectionViewModel _gameSelectionViewModel;
        private readonly Match _match;
        private readonly NavigationService _matchDetailNavigationService;

        public SelectGameCommand(GameSelectionViewModel gameSelectionViewModel, Match match, NavigationService matchDetailNavigationService)
        {
            _gameSelectionViewModel = gameSelectionViewModel;
            _match = match;
            _matchDetailNavigationService = matchDetailNavigationService;
        }


        public override void Execute(object? parameter)
        {
            if (parameter is string game)
            {
                _match.Game = game;
            }

            _matchDetailNavigationService.Navigate();
        }
    }
}
