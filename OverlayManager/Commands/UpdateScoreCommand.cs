using OverlayManager.Models;
using OverlayManager.ViewModels;
using Reservoom.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace OverlayManager.Commands
{
    public class UpdateScoreCommand : CommandBase
    {
        private readonly Match _match;
        private TcpClient _client;
        private readonly MatchControlViewModel _matchControlViewModel;

        public UpdateScoreCommand(Match match,
            TcpClient client,
            MatchControlViewModel matchControlViewModel)
        {
            _match = match;
            _client = client;
            _matchControlViewModel = matchControlViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is string res)
            {
                //_match.updateScore(res);

                switch (res)//put this into model later
                {
                    case "t1m"://Team 1 Minus
                        if (_match.gameNum > 1 && !_match.hasWinner) _match.gameNum--;

                        if (_match.Team1.Score == _match.winScore) _match.hasWinner = false;

                        if(_match.Team1.Score > 0 && !_match.hasWinner)
                        {
                            _match.Team1.Score--;
                        }
                        else {
                            if (_match.hasWinner)
                                MessageBox.Show("Cannot change score of losing team after series score has been reached.", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                            else
                                MessageBox.Show("Cannot have a negative score.", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        _matchControlViewModel.Team1Score = _match.Team1.Score.ToString();
                        break;

                    case "t1p"://Team 1 Plus
                        if (_match.Team1.Score < _match.winScore && !_match.hasWinner)
                        {
                            _match.Team1.Score++;
                            _matchControlViewModel.Team1Score = _match.Team1.Score.ToString();
                            if(_match.Team1.Score == _match.winScore)
                            {
                                _match.hasWinner = true;
                            }
                            if (!_match.hasWinner) _match.gameNum++;
                        }
                        else
                        {
                            MessageBox.Show("The winning score has been reached..", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;

                    case "t2m"://Team 2 Minus
                        if (_match.gameNum > 1 && !_match.hasWinner) _match.gameNum--;

                        if (_match.Team2.Score == _match.winScore) _match.hasWinner = false;

                        if (_match.Team2.Score > 0 && !_match.hasWinner)
                        {
                            _match.Team2.Score--;
                        }
                        else
                        {
                            if (_match.hasWinner)
                                MessageBox.Show("Cannot change score of losing team after series score has been reached.", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                            else
                                MessageBox.Show("Cannot have a negative score.", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        _matchControlViewModel.Team2Score = _match.Team2.Score.ToString();
                        break;

                    case "t2p"://Team 2 Plus
                        if (_match.Team2.Score < _match.SeriesLength / 2 + 1 && !_match.hasWinner)
                        {
                            _match.Team2.Score++;
                            _matchControlViewModel.Team2Score = _match.Team2.Score.ToString();
                            if (_match.Team2.Score == _match.SeriesLength / 2 + 1)
                            {
                                _match.hasWinner = true;
                            }

                            if(!_match.hasWinner)_match.gameNum++;
                        }
                        else
                        {
                            MessageBox.Show("The winning score has been reached..", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                }

                //send match to overlay
                string serializedMatch = JsonConvert.SerializeObject(_match);
                byte[] data = Encoding.UTF8.GetBytes(serializedMatch);
                NetworkStream stream = _client.GetStream();
                stream.Write(data, 0, data.Length);
            }
        }
    }
}

