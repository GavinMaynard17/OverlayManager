using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace OverlayManager.RocketLeagueOverlay.Models
{
    class CurrentMsg
    {
        private string _event;
        JsonObject _data;
        public CurrentMsg()
        {
            _event = "";
            _data = new JsonObject();
        }

        public string getEvent()
        {
            return _event;
        }
    }
}
