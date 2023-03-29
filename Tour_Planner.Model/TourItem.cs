using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.Model {
    public class TourItem {
        public int Id { get; private set; } // can be of type Guid - problem with it tho is that the value is not deterministic
        public string Text { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public bool Status { get; set; } = false;
    }
}
