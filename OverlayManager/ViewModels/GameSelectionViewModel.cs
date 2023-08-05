using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayManager.ViewModels
{
    public class GameSelectionViewModel : ViewModelBase
    {
		private string game;
		public string Game
		{
			get
			{
				return game;
			}
			set
			{
				game = value;
				OnPropertyChanged(nameof(Game));
			}
		}
	}
}
//changes game selection
//navigate to match details