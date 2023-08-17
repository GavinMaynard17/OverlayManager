using OverlayManager.Models;
using OverlayManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayManager.Commands
{
    public class ClearDetailsCommand : CommandBase
    {
        private readonly Match _match;
        private readonly NavigationService _selectGameNavigationService;
        public ClearDetailsCommand(Match match, NavigationService selectGameNavigationService) 
        {
            _match = match;
            _selectGameNavigationService = selectGameNavigationService;
        }

        public override void Execute(object? parameter)
        {
            _match.clearDetails();
            _selectGameNavigationService.Navigate();
        }
    }
}
