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

namespace OverlayManager.LeagueOfLegendsOverlay.ViewModels
{
    public class LeagueOfLegendsViewModel :ViewModelBase
    {
        private const int Port = 2209;
        private TcpListener _server;

        public LeagueOfLegendsViewModel()
        {
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


                    System.Diagnostics.Debug.WriteLine(message);
                }



                // Process the message, update data, and UI accordingly
                // Call appropriate methods on this ViewModel to update UI

                stream.Close();
                client.Close();
                _server.Stop();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Application.Current.Windows[2].Close();
                });
            }
        }
    }
}
