using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.BL;
using Tour_Planner.DAL;
using Tour_Planner.Model;

namespace Tour_Planner.Test {
    public class LogicTests {
        [SetUp]
        public void Setup() {
        }

        // Test child friendliness - answer true
        [Test]
        public void ChildFriendlyTestShouldBeTrue() {
            // Arrange
            ObservableCollection<TourLogs> tl = new ObservableCollection<TourLogs>();
            DataManagerEntityFrameworkImpl dal = new DataManagerEntityFrameworkImpl();
            TourManager tm = new TourManager(dal);
            tl.Add(new TourLogs() { Difficulty = Model.Enums.Difficulty.None, TotalTime = new TimeSpan(1, 0, 0), Rating = 4 });
            tl.Add(new TourLogs() { Difficulty = Model.Enums.Difficulty.Easy, TotalTime = new TimeSpan(9, 15, 32), Rating = 10 });
            tl.Add(new TourLogs() { Difficulty = Model.Enums.Difficulty.Medium, TotalTime = new TimeSpan(4, 30, 0), Rating = 7 });

            // Act
            var result = tm.isChildFriendly(tl);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        // Test child friendliness - answer false
        [Test]
        public void ChildFriendlyTestShouldBeFalse() {
            // Arrange
            ObservableCollection<TourLogs> tl = new ObservableCollection<TourLogs>();
            DataManagerEntityFrameworkImpl dal = new DataManagerEntityFrameworkImpl();
            TourManager tm = new TourManager(dal);
            tl.Add(new TourLogs() { Difficulty = Model.Enums.Difficulty.None, TotalTime = new TimeSpan(1, 0, 0), Rating = 4 });
            tl.Add(new TourLogs() { Difficulty = Model.Enums.Difficulty.Easy, TotalTime = new TimeSpan(9, 15, 32), Rating = 1 });
            tl.Add(new TourLogs() { Difficulty = Model.Enums.Difficulty.Medium, TotalTime = new TimeSpan(4, 30, 0), Rating = 2 });

            // Act
            var result = tm.isChildFriendly(tl);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        
    }
}
