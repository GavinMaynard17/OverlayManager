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
    public class SeriesLengthCommand : CommandBase
    {
        private readonly MatchDetailViewModel _matchDetailViewModel;
        private readonly Match _match;

        public SeriesLengthCommand(MatchDetailViewModel matchDetailViewModel,Match match)
        {
            _matchDetailViewModel = matchDetailViewModel;
            _match = match;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is string length)
            {
                _matchDetailViewModel.SeriesLength = length;
            }
        }
    }
}
