using OverlayManager.Commands;
using OverlayManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OverlayManager.ViewModels
{
    public class GameSelectionViewModel : ViewModelBase
    {
		private readonly Match _match;

		public ICommand MatchDetailCommand { get; }
		public ICommand CastersCommand { get; }

        public GameSelectionViewModel(Match match, Services.NavigationService matchDetailNavigationService, Services.NavigationService casterListNavigationService)
		{
			_match = match;
			
			MatchDetailCommand = new SelectGameCommand(this, _match, matchDetailNavigationService);
			CastersCommand = new NavigateCommand(casterListNavigationService);
        }


	}
}
//changes game selection
//navigate to match details