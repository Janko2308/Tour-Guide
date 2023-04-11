using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.Model {
    public class TourItem {
        public int Id { get; private set; } // can be of type Guid - problem with it tho is that the value is not deterministic
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string TransportType { get; set; } = string.Empty;
        public int Distance { get; set; } = 0;
        public int EstimatedTime { get; set; } = 0;
        public string TourInfo { get; set; } = string.Empty;
    }
}
