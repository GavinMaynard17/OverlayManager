using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayManager.Models
{
    public class Caster
    {
        public string Name { get; set; }
        public string Username { get; set; }

        public Caster(string name, string username) 
        {
            this.Name = name;
            this.Username = username;
        }
    }
}
