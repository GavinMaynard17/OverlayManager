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
    public class RemoveCasterCommand : CommandBase
    {
        Match _match;
        CasterListViewModel _casterListViewModel;

        public RemoveCasterCommand(Match match, CasterListViewModel casterListViewModel) 
        {
            _match = match;
            _casterListViewModel = casterListViewModel;;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is Caster caster) _match.Casters.Remove(caster);
            _casterListViewModel.CasterList = _match.Casters;
        }
    }
}
