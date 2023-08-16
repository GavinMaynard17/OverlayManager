using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayManager.Models
{
    public class Match
    {
        public string Game { get; set; }
        public int SeriesLength { get; set; }
        public int gameNum { get; set; }
        public int winScore { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public bool hasWinner { get; set; }

        public Match()
        {
            Game = "";
            SeriesLength = 0;
            gameNum = 1;
            winScore = SeriesLength / 2 + 1;
            Team1 = new Team();
            Team2 = new Team();
            hasWinner = false;
        }

        public void clearDetails()
        {
            Game = "Unknown";
            SeriesLength = 0;
            gameNum = 1;
            Team1.clearDetails();
            Team2.clearDetails();
            hasWinner = false;
        }
    }
}
