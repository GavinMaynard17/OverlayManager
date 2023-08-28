using OverlayManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using OverlayManager.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace OverlayManager.ValorantOverlay.ViewModels
{
    public class ValorantViewModel : ViewModelBase
    {
        private const int Port = 2209;
        private TcpListener _server;
        Match _match;

        public ValorantViewModel()
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
                        if (window.Title == "Overlay") window.Close();
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

            if (_match.Team1.Logo != "")
                Team1Logo = Application.Current.Dispatcher.Invoke(() => new BitmapImage(new Uri(_match.Team1.Logo)));
            if (_match.Team2.Logo != "")
                Team2Logo = Application.Current.Dispatcher.Invoke(() => new BitmapImage(new Uri(_match.Team2.Logo)));

            if (_match.winScore > 1) 
            {
                Application.Current.Dispatcher.Invoke(() => { 
                    TextBox1Margin = new Thickness(0, 0, 430, 848);
                    TextBox2Margin = new Thickness(430, 0, 0, 848);
                    TextBoxHeight = 50;

                    SeriesVisibility = Visibility.Visible;

                    Team1NamePoints = new PointCollection()
                        {
                            new Point(669, 140),
                            new Point(669, 93),
                            new Point(825, 93),
                            new Point(825, 125),
                            new Point(813, 140)
                        };
                    Team2NamePoints = new PointCollection()
                        {
                            new Point(1093, 125),
                            new Point(1093, 93),
                            new Point(1250, 93),
                            new Point(1250, 140),
                            new Point(1105, 140)
                        };
                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() => { 
                    TextBox1Margin = new Thickness(0, 0, 430, 828);
                    TextBox2Margin = new Thickness(430, 0, 0, 828);
                    TextBoxHeight = 70;

                    SeriesVisibility = Visibility.Hidden;

                    Team1NamePoints = new PointCollection()
                        {
                            new Point(669, 161),
                            new Point(669, 93),
                            new Point(825, 93),
                            new Point(825, 125),
                            new Point(796, 161)
                        };
                    Team2NamePoints = new PointCollection()
                        {
                            new Point(1093, 125),
                            new Point(1093, 93),
                            new Point(1250, 93),
                            new Point(1250, 161),
                            new Point(1120, 161)
                        };
                });
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
                return "Game " + gameNum + " best of " + _match.SeriesLength;
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

        private Thickness textBox1Margin;
        public Thickness TextBox1Margin
        {
            get
            {
                return textBox1Margin;
            }
            set
            {
                textBox1Margin = value;
                OnPropertyChanged(nameof(TextBox1Margin));
            }
        }

        private Thickness textBox2Margin;
        public Thickness TextBox2Margin
        {
            get
            {
                return textBox2Margin;
            }
            set
            {
                textBox2Margin = value;
                OnPropertyChanged(nameof(TextBox2Margin));
            }
        }

        private PointCollection team1NamePoints = new PointCollection();
        public PointCollection Team1NamePoints
        {
            get
            {
                return team1NamePoints;
            }
            set
            {
                team1NamePoints = value;
                OnPropertyChanged(nameof(Team1NamePoints));
            }
        }

        private PointCollection team2NamePoints = new PointCollection();
        public PointCollection Team2NamePoints
        {
            get
            {
                return team2NamePoints;
            }
            set
            {
                team2NamePoints = value;
                OnPropertyChanged(nameof(Team2NamePoints));
            }
        }

        private int textBoxHeight;
        public int TextBoxHeight
        {
            get
            {
                return textBoxHeight;
            }
            set
            {
                textBoxHeight = value;
                OnPropertyChanged(nameof(TextBoxHeight));
            }
        }

        private SolidColorBrush seriesBoxColor;
        public SolidColorBrush SeriesBoxColor
        {
            get
            {
                return seriesBoxColor;
            }
            set
            {
                seriesBoxColor = value;
                OnPropertyChanged(nameof(SeriesBoxColor));
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
    }
}
