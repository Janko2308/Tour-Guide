using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Model.Enums;

namespace Tour_Planner.Model {
    public class TourLogs {
        public TourLogs() { }
        public TourLogs(TourLogs other) { 
            Id = other.Id;
            TourId = other.TourId;
            DateTime = other.DateTime;
            Comment = other.Comment;
            Difficulty = other.Difficulty;
            TotalTime = other.TotalTime;
            Rating = other.Rating;
        }
        public int Id { get; private set; }
        public int TourId { get; set; } = -1;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string Comment { get; set; } = string.Empty;
        public Difficulty Difficulty { get; set; } = Difficulty.None;
        public TimeSpan TotalTime { get; set; } = TimeSpan.Zero;
        public int Rating { get; set; } = 0;
    }
}
