using Microsoft.Win32;
using OverlayManager.Models;
using OverlayManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace OverlayManager.Commands
{
    public class SelectLogoCommand : CommandBase
    {
        private readonly MatchDetailViewModel _matchDetailViewModel;
        private readonly Match _match;
        public SelectLogoCommand(MatchDetailViewModel matchDetailViewModel, Match match) 
        {
            _matchDetailViewModel = matchDetailViewModel;
            _match = match;
        }

        public override void Execute(object? parameter)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                if(parameter is string team)
                {
                    if (team == "Team1")
                    {
                        _match.Team1.Logo = openFileDialog.FileName;
                        _matchDetailViewModel.Team1Logo = new BitmapImage(new Uri(_match.Team1.Logo));
                    }

                    else
                    {
                        _match.Team2.Logo = openFileDialog.FileName;
                        _matchDetailViewModel.Team2Logo = new BitmapImage(new Uri(_match.Team2.Logo));
                    }
                }
            }
        }
    }
}
