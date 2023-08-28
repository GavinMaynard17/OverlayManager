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
using System.Collections.ObjectModel;
using OverlayManager.Models;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace OverlayManager.OverwatchOverlay.ViewModels
{
    public class OverwatchViewModel : ViewModelBase
    {
        private const int Port = 2209;
        private TcpListener _server;
        Match _match;

        public OverwatchViewModel()
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

            switch (_match.winScore)
            {
                case 1:
                    Application.Current.Dispatcher.Invoke(() => { 
                        SeriesVisibility = Visibility.Hidden;
                        ScoreBox1Margin = new Thickness(0, 0, 0, 0);
                        ScoreBox2Margin = new Thickness(0, 0, 0, 0);
                        TextBox1Margin = new Thickness(0, 0, 1035, 1010);
                        TextBox2Margin = new Thickness(1035, 0, 0, 1010);
                        TextBoxWidth = 250;
                    });
                    break;

                case 2:
                    Application.Current.Dispatcher.Invoke(() => { 
                        SeriesVisibility = Visibility.Visible;
                        ScoreBox1Margin = new Thickness(0, 0, 840, 1027);
                        ScoreBox2Margin = new Thickness(840, 0, 0, 1027);
                        TextBox1Margin = new Thickness(0, 0, 1100, 1010);
                        TextBox2Margin = new Thickness(1100, 0, 0, 1010);
                        TextBoxWidth = 200;
                    });
                    break;
                    
                case 3:
                    Application.Current.Dispatcher.Invoke(() => { 
                        SeriesVisibility = Visibility.Visible;
                        ScoreBox1Margin = new Thickness(0, 0, 840, 1018);
                        ScoreBox2Margin = new Thickness(840, 0, 0, 1018);
                        TextBox1Margin = new Thickness(0, 0, 1100, 1010);
                        TextBox2Margin = new Thickness(1100, 0, 0, 1010);
                        TextBoxWidth = 200;
                    });
                    break;

                default:
                    Application.Current.Dispatcher.Invoke(() => { 
                        SeriesVisibility = Visibility.Hidden;
                        ScoreBox1Margin = new Thickness(0, 0, 0, 0);
                        ScoreBox2Margin = new Thickness(0, 0, 0, 0);
                        TextBox1Margin = new Thickness(0, 0, 1035, 1010);
                        TextBox2Margin = new Thickness(1035, 0, 0, 1010);
                        TextBoxWidth = 250;
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

        private Thickness scoreBox1Margin;
        public Thickness ScoreBox1Margin
        {
            get
            {
                return scoreBox1Margin;
            }
            set
            {
                scoreBox1Margin = value;
                OnPropertyChanged(nameof(ScoreBox1Margin));
            }
        }

        private Thickness scoreBox2Margin;
        public Thickness ScoreBox2Margin
        {
            get
            {
                return scoreBox2Margin;
            }
            set
            {
                scoreBox2Margin = value;
                OnPropertyChanged(nameof(ScoreBox2Margin));
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

        private int textBoxWidth;
        public int TextBoxWidth
        {
            get
            {
                return textBoxWidth;
            }
            set
            {
                textBoxWidth = value;
                OnPropertyChanged(nameof(TextBoxWidth));
            }
        }

        private string gameNum;
        public string GameNum
        {
            get
            {
                return "Game " + gameNum + " of " + _match.SeriesLength + "Score to win: " + _match.winScore;
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

        private int seriesBoxHeight;
        public int SeriesBoxHeight
        {
            get
            {
                return seriesBoxHeight;
            }
            set
            {
                seriesBoxHeight = value;
                OnPropertyChanged(nameof(SeriesBoxHeight));
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
