using OverlayManager.Models;
using OverlayManager.Services;
using Reservoom.Commands;
using System;
using System.Collections.Generic;
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
        private TcpClient _client;
        private readonly NavigationService _selectGameNavigationService;
        public EndGameCommand(Match match,
            TcpClient client,
            NavigationService selectGameNavigationService)
        {
            _match = match;
            _client = client;
            _selectGameNavigationService = selectGameNavigationService;
        }

        public override void Execute(object? parameter)
        {
            //close the thing
            string message = "close";
            byte[] data = Encoding.UTF8.GetBytes(message);
            NetworkStream stream = _client.GetStream();
            stream.Write(data, 0, data.Length);

            _match.clearDetails();
            _selectGameNavigationService.Navigate();
        }
    }
}
