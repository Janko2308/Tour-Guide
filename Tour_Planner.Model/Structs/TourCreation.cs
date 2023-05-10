using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.Model.Structs {
    public struct TourCreation {
        public byte[] Picture { get; set; }
        public int Distance { get; set; }
        public int EstimatedTime { get; set; }
    }
}
