using OverlayManager.Models;
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
    public class SeriesLengthCommand : CommandBase
    {
        private readonly MatchDetailViewModel _matchDetailViewModel;
        private readonly Match _match;

        public SeriesLengthCommand(MatchDetailViewModel matchDetailViewModel,Match match)
        {
            _matchDetailViewModel = matchDetailViewModel;
            _match = match;
            _matchDetailViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            string length = parameter.ToString();
            return int.Parse(length)!=_match.SeriesLength && 
                base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if (parameter is string length)
            {
                _matchDetailViewModel.SeriesLength = length;
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MatchDetailViewModel.SeriesLength))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
