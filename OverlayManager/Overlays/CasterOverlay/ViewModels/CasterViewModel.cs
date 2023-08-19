using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OverlayManager.Models;
using OverlayManager.ViewModels;
using System.Windows;

namespace OverlayManager.Overlays.CasterOverlay.ViewModels
{
    public class CasterViewModel : ViewModelBase
    {
        private const int Port = 2210;
        private ObservableCollection<Caster> _casterList;
        private TcpListener _server;

        public CasterViewModel() 
        {
            _casterList = new ObservableCollection<Caster>();
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

                    ObservableCollection<Caster> casterList = JsonConvert.DeserializeObject<ObservableCollection<Caster>>(message);
                    CasterList = casterList;
                }

                stream.Close();
                client.Close();
                _server.Stop();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.Title == "Caster Overlay") window.Close();
                    }
                });
            }
        }

        

        private ObservableCollection<Caster> casterList;
        public ObservableCollection<Caster> CasterList
        {
            get
            {
                return casterList;
            }
            set
            {
                casterList = value;
                OnPropertyChanged(nameof(CasterList));
            }
        }
    }
}
