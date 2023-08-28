
using OverlayManager.RocketLeagueOverlay.Views;
using OverlayManager.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using OverlayManager.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing.Text;

namespace OverlayManager.RocketLeagueOverlay.ViewModels
{
    public class RocketLeagueViewModel : ViewModelBase
    {
        private const int Port = 2209;
        private TcpListener _server;
        Match _match;

        public RocketLeagueViewModel()
        {
            _match = new Match();
            StartServer();
            
        }

        private void StartServer()
        {
            _server = new TcpListener(IPAddress.Any, Port);
            _server.Start();

            ThreadPool.QueueUserWorkItem(_ =>
            {
                
                TcpClient client = _server.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(HandleClient, client);
                
            });
        }

        private void HandleClient(object clientObj)
        {
            using (TcpClient client = (TcpClient)clientObj)
            {
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    if (message == "close")
                    {
                        break;

                    }
                    
                    Match match = JsonConvert.DeserializeObject<Match>(message);
                    _match = match;
                    updateUI();
                    
                }

                stream.Close();
                client.Close();
                _server.Stop();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if(window.Title == "Overlay") window.Close();
                    }
                });
            }
        }

        private void updateUI()
        {
            
            Team1Name = _match.Team1.Name;
            Team2Name = _match.Team2.Name;
            Team1Score = new ObservableCollection<object>(new object[_match.Team1.Score]);
            Team2Score = new ObservableCollection<object>(new object[_match.Team2.Score]);
            GameNum = _match.gameNum.ToString();
            WinScore = new ObservableCollection<object>(new object[_match.winScore]);


            if(_match.Team1.Logo != "") 
                Team1Logo = Application.Current.Dispatcher.Invoke(() => new BitmapImage(new Uri(_match.Team1.Logo)));
            if (_match.Team2.Logo != "")
                Team2Logo = Application.Current.Dispatcher.Invoke(() => new BitmapImage(new Uri(_match.Team2.Logo)));

            if (_match.winScore > 1)
                SeriesVisibility = Visibility.Visible;
            else
                SeriesVisibility = Visibility.Hidden;

            
            switch (_match.winScore)
            {
                case 1:
                    break;
                case 2:
                    Application.Current.Dispatcher.Invoke(() => {
                        Team1ScorePoints = new PointCollection()
                        {
                            new Point(750, 88),
                            new Point(870, 88),
                            new Point(870, 120),
                            new Point(760, 120)
                        };

                        Team2ScorePoints = new PointCollection()
                        {
                            new Point(1050, 88),
                            new Point(1170, 88),
                            new Point(1160, 120),
                            new Point(1050, 120)
                        };
                    });
                    break;
                case 3:
                    Application.Current.Dispatcher.Invoke(() => {
                        Team1ScorePoints = new PointCollection()
                        {
                            new Point(715, 88),
                            new Point(870, 88),
                            new Point(870, 120),
                            new Point(725, 120)
                        };

                        Team2ScorePoints = new PointCollection()
                        {
                            new Point(1050, 88),
                            new Point(1205, 88),
                            new Point(1195, 120),
                            new Point(1050, 120)
                        };
                    });
                    break;
                case 4:
                    Application.Current.Dispatcher.Invoke(() => {
                        Team1ScorePoints = new PointCollection()
                        {
                            new Point(680, 88),
                            new Point(870, 88),
                            new Point(870, 120),
                            new Point(690, 120)
                        };

                        Team2ScorePoints = new PointCollection()
                        {
                            new Point(1050, 88),
                            new Point(1240, 88),
                            new Point(1230, 120),
                            new Point(1050, 120)
                        };
                    });
                    break;
            }
            
        }

        private string team1Name;
        public string Team1Name
        {
            get
            {
                return team1Name;
            }
            set
            {
                team1Name = value;
                OnPropertyChanged(nameof(Team1Name));
            }
        }

        private BitmapImage _team1Logo;
        public BitmapImage Team1Logo
        {
            get { return _team1Logo; }
            set
            {
                _team1Logo = value;
                OnPropertyChanged(nameof(Team1Logo));
            }
        }

        private string team2Name;
        public string Team2Name
        {
            get
            {
                return team2Name;
            }
            set
            {
                team2Name = value;
                OnPropertyChanged(nameof(Team2Name));
            }
        }

        private BitmapImage _team2Logo;
        public BitmapImage Team2Logo
        {
            get { return _team2Logo; }
            set
            {
                _team2Logo = value;
                OnPropertyChanged(nameof(Team2Logo));
            }
        }

        private ObservableCollection<object> team1Score;
        public ObservableCollection<object> Team1Score
        {
            get
            {
                return team1Score;
            }
            set
            {
                team1Score = value;
                OnPropertyChanged(nameof(Team1Score));
            }
        }

        private ObservableCollection<object> team2Score;
        public ObservableCollection<object> Team2Score
        {
            get
            {
                return team2Score;
            }
            set
            {
                team2Score = value;
                OnPropertyChanged(nameof(Team2Score));
            }
        }

        private string gameNum;
        public string GameNum
        {
            get
            {
                return "Game " + gameNum + " | Best of " + _match.SeriesLength;
            }
            set
            {
                gameNum = value;
                OnPropertyChanged(nameof(GameNum));
            }
        }

        private ObservableCollection<object> winScore;
        public ObservableCollection<object> WinScore
        {
            get
            {
                return winScore;
            }
            set
            {
                winScore = value;
                OnPropertyChanged(nameof(WinScore));
            }
        }

        private Visibility _seriesVisibility;
        public Visibility SeriesVisibility
        {
            get { return _seriesVisibility; }
            set
            {
                if (_seriesVisibility != value)
                {
                    _seriesVisibility = value;
                    OnPropertyChanged("SeriesVisibility");
                }
            }
        }

        private PointCollection team1ScorePoints = new PointCollection();
        public PointCollection Team1ScorePoints
        {
            get
            {
                return team1ScorePoints;
            }
            set
            {
                team1ScorePoints = value;
                OnPropertyChanged(nameof(Team1ScorePoints));
            }
        }

        private PointCollection team2ScorePoints = new PointCollection();
        public PointCollection Team2ScorePoints
        {
            get
            {
                return team2ScorePoints;
            }
            set
            {
                team2ScorePoints = value;
                OnPropertyChanged(nameof(Team2ScorePoints));
            }
        }
    }
}
