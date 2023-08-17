using Newtonsoft.Json;
using OverlayManager.Commands;
using OverlayManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace OverlayManager.ViewModels
{
    public class MatchControlViewModel : ViewModelBase
    {
        private readonly Match _match;
        private const string ServerIpAddress = "127.0.0.1";
        private const int ServerPort = 2209;
        private TcpClient _client;

        public ICommand EndGameCommand { get; }
        public ICommand UpdateScoreCommand { get; }
        public ICommand SwitchSidesCommand { get; }

        public MatchControlViewModel(Match match,
            Services.NavigationService gameSelectionNavigationService)
        {
            _match = match;
            _client = new TcpClient();
            ConnectToServer();
            EndGameCommand = new EndGameCommand(_match,
                _client,
                gameSelectionNavigationService);
            UpdateScoreCommand = new UpdateScoreCommand(_match,
                _client,
                this);
            SwitchSidesCommand = new SwitchSidesCommand(_match,
                _client,
                this);
        }

        private void ConnectToServer()
        {
            _client.Connect(ServerIpAddress, ServerPort);
            string serializedMatch = JsonConvert.SerializeObject(_match);
            byte[] data = Encoding.UTF8.GetBytes(serializedMatch);
            NetworkStream stream = _client.GetStream();
            stream.Write(data, 0, data.Length);
        }

        private string team1Name;
        public string Team1Name
        {
            get
            {
                return _match.Team1.Name;
            }
            set
            {
                OnPropertyChanged(nameof(Team1Name));
            }
        }


        public string Team2Name
        {
            get
            {
                return _match.Team2.Name;
            }
            set
            {
                OnPropertyChanged(nameof(Team2Name));
            }
        }

        public string SeriesLength
        {
            get
            {
                return "Best of: " + _match.SeriesLength;
            }
        }

        public string Team1Score
        {
            get
            {
                return  _match.Team1.Score.ToString();
            }
            set
            {
                
                _match.Team1.Score = int.Parse(value);
                OnPropertyChanged(nameof(Team1Score));
                
            }
        }

        public string Team2Score
        {
            get
            {
                return _match.Team2.Score.ToString();
            }
            set
            {
                
                _match.Team2.Score = int.Parse(value);
                OnPropertyChanged(nameof(Team2Score));
                
            }
        }


    }
}
//update series score here and on overlay
//close overlay and navigate back to game selection