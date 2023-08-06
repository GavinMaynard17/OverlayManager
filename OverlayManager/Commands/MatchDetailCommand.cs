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
        }

        public override void Execute(object? parameter)
        {
            _matchControlNavigationService.Navigate();
        }
    }
}
