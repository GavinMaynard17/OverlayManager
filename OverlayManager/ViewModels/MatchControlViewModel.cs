using OverlayManager.Commands;
using OverlayManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OverlayManager.ViewModels
{
    public class MatchControlViewModel : ViewModelBase
    {
        private Match _match;

        public ICommand EndGameCommand { get; }
        public ICommand UpdateScoreCommand { get;  }

        public MatchControlViewModel(Match match, Services.NavigationService gameSelectionNavigationService)
        {
            _match = match;
            EndGameCommand = new ClearDetailsCommand(_match, gameSelectionNavigationService);
            UpdateScoreCommand = new UpdateScoreCommand(_match, this);
        }

        public string Team1Name
        {
            get
            {
                return _match.Team1.Name;
            }
        }

        public string Team2Name
        {
            get
            {
                return _match.Team2.Name;
            }
        }

        public string Team1Score
        {
            get
            {
                return  _match.Team1.Score.ToString();
            }
            set
            {
                if (_match.Team1.Score != int.Parse(value))
                {
                    _match.Team1.Score = int.Parse(value);
                    OnPropertyChanged(nameof(Team1Score));
                }
            }
        }

        public string Team2Score
        {
            get
            {
                return _match.Team2.Score.ToString();
            }
            set
            {
                if (_match.Team2.Score != int.Parse(value))
                {
                    _match.Team2.Score = int.Parse(value);
                    OnPropertyChanged(nameof(Team2Score));
                }
            }
        }


    }
}
//update series score here and on overlay
//close overlay and navigate back to game selection