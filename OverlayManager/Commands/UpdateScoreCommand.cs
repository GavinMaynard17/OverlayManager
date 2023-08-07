using OverlayManager.Models;
using OverlayManager.ViewModels;
using Reservoom.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            if (parameter is string data)
            {
                switch (data)
                {
                    case "t1m":
                        if (_match.Team1.Score > 0 && !_match.hasWinner)
                        {
                            if (_match.Team1.Score == _match.SeriesLength / 2 + 1)
                            {
                                _match.hasWinner = false;
                            }
                            _match.Team1.Score--;
                            _matchControlViewModel.Team1Score = _match.Team1.Score.ToString();
                        }
                        else if (_match.Team1.Score == _match.SeriesLength / 2 + 1)
                        {
                            _match.hasWinner = false;
                            _match.Team1.Score--;
                            _matchControlViewModel.Team1Score = _match.Team1.Score.ToString();
                        }
                        else
                        {
                            if(_match.hasWinner)
                                MessageBox.Show("Cannot change score of losing team after series score has been reached.", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                            else
                                MessageBox.Show("Cannot have a negative score.", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;

                    case "t1p":
                        if (_match.Team1.Score < _match.SeriesLength / 2 + 1 && !_match.hasWinner)
                        {
                            _match.Team1.Score++;
                            _matchControlViewModel.Team1Score = _match.Team1.Score.ToString();
                            if(_match.Team1.Score == _match.SeriesLength / 2 + 1)
                            {
                                _match.hasWinner = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("The winning score has been reached..", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;

                    case "t2m":
                        if (_match.Team2.Score > 0 && !_match.hasWinner)
                        {
                            if (_match.Team2.Score == _match.SeriesLength / 2 + 1)
                            {
                                _match.hasWinner = false;
                            }
                            _match.Team2.Score--;
                            _matchControlViewModel.Team2Score = _match.Team2.Score.ToString();
                        }
                        else if (_match.Team2.Score == _match.SeriesLength / 2 + 1)
                        {
                            _match.hasWinner = false;
                            _match.Team2.Score--;
                            _matchControlViewModel.Team2Score = _match.Team2.Score.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Cannot have a negative score.", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;

                    case "t2p":
                        if (_match.Team2.Score < _match.SeriesLength / 2 + 1 && !_match.hasWinner)
                        {
                            _match.Team2.Score++;
                            _matchControlViewModel.Team2Score = _match.Team2.Score.ToString();
                            if (_match.Team2.Score == _match.SeriesLength / 2 + 1)
                            {
                                _match.hasWinner = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("The winning score has been reached..", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                }
            }
        }
    }
}

