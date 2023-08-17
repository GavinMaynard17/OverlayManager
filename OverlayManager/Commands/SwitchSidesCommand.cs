using Newtonsoft.Json;
using OverlayManager.Models;
using OverlayManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OverlayManager.Commands
{
    public class SwitchSidesCommand : CommandBase
    {
        MatchControlViewModel _matchControlViewModel;
        Match _match;
        TcpClient _client;
        Team _tempTeam1;
        Team _tempTeam2;

        public SwitchSidesCommand(Match match, TcpClient client, MatchControlViewModel matchControlViewModel) 
        {
            _match = match;
            _client = client;
            _matchControlViewModel = matchControlViewModel;
        }

        public override void Execute(object? parameter)
        {
            _tempTeam1 = _match.Team1;
            _tempTeam2 = _match.Team2;
            _match.Team1 = _tempTeam2;
            _match.Team2 = _tempTeam1;

            _matchControlViewModel.Team1Name = _match.Team1.Name;
            _matchControlViewModel.Team1Score = _match.Team1.Score.ToString();
            _matchControlViewModel.Team2Name = _match.Team2.Name;
            _matchControlViewModel.Team2Score = _match.Team2.Score.ToString();

            string serializedMatch = JsonConvert.SerializeObject(_match);
            byte[] data = Encoding.UTF8.GetBytes(serializedMatch);
            NetworkStream stream = _client.GetStream();
            stream.Write(data, 0, data.Length);
        }
    }
}
