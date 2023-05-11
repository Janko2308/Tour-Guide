using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Model.Enums;

namespace Tour_Planner.Model {
    public class TourItem {
        public int Id { get; private set; } // can be of type Guid - problem with it tho is that the value is not deterministic
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public Transport TransportType { get; set; } = Transport.fastest;
        public double Distance { get; set; } = 0;
        public TimeSpan EstimatedTime { get; set; } = TimeSpan.Zero;
        public byte[] TourInfo { get; set; } = Array.Empty<byte>();
    }
}
