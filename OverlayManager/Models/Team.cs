using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace OverlayManager.Models
{
    public class Team
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public string Logo { get; set; }

        public Team()
        {
            Name = "";
            Score = 0;
            Logo = "";
        }

        public void clearDetails()
        {
            Name = "";
            Score = 0;
            Logo = "";
        }
    }
}
