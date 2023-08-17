using OverlayManager.Models;
using OverlayManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayManager.Commands
{
    public class RemoveCasterCommand : CommandBase
    {
        Match _match;

        public RemoveCasterCommand(Match match, CasterListViewModel casterListViewModel) 
        {
            _match = match;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is Caster caster) _match.Casters.Remove(caster);
            
        }
    }
}
