using OverlayManager.Models;
using OverlayManager.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OverlayManager.Commands
{
    public class EndGameCommand : CommandBase
    {
        private readonly Match _match;
        private TcpClient _matchClient;
        private TcpClient _casterClient;
        private readonly NavigationService _selectGameNavigationService;
        public EndGameCommand(Match match,
            TcpClient matchClient,
            TcpClient casterClient,
            NavigationService selectGameNavigationService)
        {
            _match = match;
            _matchClient = matchClient;
            _casterClient = casterClient;
            _selectGameNavigationService = selectGameNavigationService;
        }

        public override void Execute(object? parameter)
        {
            //close the thing
            string message = "close";
            byte[] data = Encoding.UTF8.GetBytes(message);
            NetworkStream stream = _matchClient.GetStream();
            stream.Write(data, 0, data.Length);

            stream = _casterClient.GetStream();
            stream.Write(data, 0, data.Length);

            _match.clearDetails();
            _selectGameNavigationService.Navigate();
        }
    }
}
