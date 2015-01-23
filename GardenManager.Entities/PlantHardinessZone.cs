using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenManager.Entities
{
    public class PlantHardinessZone
    {
        public int Id { get; set; }
        public string Zone { get; set; }
        public string TempRange { get; set; } // The range of extreme lows for the Zone. Example - "-45, -40"
    }
}
