using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayManager.Models
{
    class Match
    {
        public string Game { get; set; }
        public int SeriesLength { get; set; }
        public string Team1Name { get; set; }
        public string Team2Name { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }

        public Match()
        {
            Game = "";
            SeriesLength = 0;
            Team1Name = "Unknown";
            Team2Name = "Unknown";
            Team1Score = 0;
            Team2Score = 0;
        }
    }
}
