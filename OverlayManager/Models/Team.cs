using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayManager.Models
{
    public class Team
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public Team()
        {
            Name = "";
            Score = 0;
        }

        public void clearDetails()
        {
            Name = "";
            Score = 0;
        }
    }
}
