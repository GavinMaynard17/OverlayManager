using OverlayManager.Models;
using OverlayManager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayManager.Commands
{
    public class AddCasterCommand : CommandBase
    {
        Match _match;
        CasterListViewModel _casterListViewModel;
        

        public AddCasterCommand(Match match, CasterListViewModel casterListViewModel) 
        {
            _match = match;
            _casterListViewModel = casterListViewModel;
            _casterListViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return _casterListViewModel.CasterList.Count < 2 &&
                base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _match.Casters.Add(new Caster(_casterListViewModel.CasterName, _casterListViewModel.CasterUsername));

            _casterListViewModel.CasterList = _match.Casters;
            _casterListViewModel.CasterName = "";
            _casterListViewModel.CasterUsername = "";
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CasterListViewModel.CasterList))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
