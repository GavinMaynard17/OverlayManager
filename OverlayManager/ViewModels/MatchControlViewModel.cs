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
        private const int MatchPort = 2209;
        private const int CasterPort = 2210;
        private TcpClient _matchClient;
        private TcpClient _casterClient;

        public ICommand EndGameCommand { get; }
        public ICommand UpdateScoreCommand { get; }
        public ICommand SwitchSidesCommand { get; }

        public MatchControlViewModel(Match match,
            Services.NavigationService gameSelectionNavigationService)
        {
            _match = match;
            _matchClient = new TcpClient();
            _casterClient = new TcpClient();
            ConnectToServers();
            EndGameCommand = new EndGameCommand(_match,
                _matchClient,
                _casterClient,
                gameSelectionNavigationService);
            UpdateScoreCommand = new UpdateScoreCommand(_match,
                _matchClient,
                this);
            SwitchSidesCommand = new SwitchSidesCommand(_match,
                _matchClient,
                this);
        }

        private void ConnectToServers()
        {
            _matchClient.Connect(ServerIpAddress, MatchPort);
            string serializedMatch = JsonConvert.SerializeObject(_match);
            byte[] data = Encoding.UTF8.GetBytes(serializedMatch);
            NetworkStream stream = _matchClient.GetStream();
            stream.Write(data, 0, data.Length);

            _casterClient.Connect(ServerIpAddress, CasterPort);
            string serializedCasters = JsonConvert.SerializeObject(_match.Casters);
            data = Encoding.UTF8.GetBytes(serializedCasters);
            stream = _casterClient.GetStream();
            stream.Write(data, 0, data.Length);
        }

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