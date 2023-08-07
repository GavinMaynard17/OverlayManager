using OverlayManager.Commands;
using OverlayManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OverlayManager.ViewModels
{
    public class MatchDetailViewModel : ViewModelBase
    {
        private Match _match;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SeriesLengthCommand { get; }


        public MatchDetailViewModel(Match match, Services.NavigationService matchControlNavigationService, Services.NavigationService gameSelectionNavigationService)
        {
            _match = match;
            SubmitCommand = new MatchDetailCommand(this, _match, matchControlNavigationService);// use this for launching overlay?
            CancelCommand = new ClearDetailsCommand(_match, gameSelectionNavigationService);
            SeriesLengthCommand = new SeriesLengthCommand(this, _match);
        }

        public string GameSelected
        {
            get
            {
                return "Game Selected: "+_match.Game;
            }
        }

        public string SeriesLength
        {
            get
            {
                if (_match.SeriesLength == 0) return "Select a series length";
                return "Best of: " + _match.SeriesLength;
            }
            set
            {
                if (_match.SeriesLength != int.Parse(value))
                {
                    _match.SeriesLength = int.Parse(value);
                    OnPropertyChanged(nameof(SeriesLength));
                }
            }
        }

        private string _team1Name;
        public string Team1Name
        {
            get
            {
                return _team1Name;
            }
            set
            {
                _team1Name = value;
                _match.Team1.Name = value;
                OnPropertyChanged(nameof(Team1Name));
            }
        }

        private string _team2Name;
        public string Team2Name
        {
            get
            {
                return _team2Name;
            }
            set
            {
                _team2Name = value;
                _match.Team2.Name = value;
                OnPropertyChanged(nameof(Team2Name));
            }
        }

        //public string MT1N
        //{
        //    get
        //    {
        //        return "Team 1 Name: " + _match.Team1Name;
        //    }
        //    set
        //    {
        //        if (_match.Team1Name != value)
        //        {
        //            _match.Team1Name = value;
        //            OnPropertyChanged(nameof(Team1Name));
        //        }
        //    }
        //}

        //public string MT2N
        //{
        //    get
        //    {
        //        return "Team 2 Name: " + _match.Team2Name;
        //    }
        //    set
        //    {
        //        if (_match.Team2Name != value)
        //        {
        //            _match.Team2Name = value;
        //            OnPropertyChanged(nameof(Team2Name));
        //        }
        //    }
        //}
    }
}
//propchange SL, t1n, t2n
//launch overlay based on game value
//navigate to match control or game selection