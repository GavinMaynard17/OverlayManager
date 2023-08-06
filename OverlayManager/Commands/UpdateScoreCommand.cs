using OverlayManager.Models;
using OverlayManager.ViewModels;
using Reservoom.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayManager.Commands
{
    public class UpdateScoreCommand : CommandBase
    {
        private readonly Match _match;
        private readonly MatchControlViewModel _matchControlViewModel;

        public UpdateScoreCommand(Match match, MatchControlViewModel matchControlViewModel)
        {
            _match = match;
            _matchControlViewModel = matchControlViewModel;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is string)
            {
                switch (parameter)
                {
                    case "t1m":
                        _match.Team1.Score--;
                        _matchControlViewModel.Team1Score = _match.Team1.Score.ToString();
                        break;
                    case "t1p":
                        _match.Team1.Score++;
                        _matchControlViewModel.Team1Score = _match.Team1.Score.ToString();
                        break;
                    case "t2m":
                        _match.Team2.Score--;
                        _matchControlViewModel.Team2Score = _match.Team2.Score.ToString(); 
                        break;
                    case "t2p":
                        _match.Team2.Score++;
                        _matchControlViewModel.Team2Score = _match.Team2.Score.ToString(); 
                        break;
                }
            }
        }
    }
}

