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
            tl.Add(new TourLogs() { Difficulty = Model.Enums.Difficulty.None, TotalTime = new TimeSpan(1, 0, 0), Rating = 4 });
            tl.Add(new TourLogs() { Difficulty = Model.Enums.Difficulty.Easy, TotalTime = new TimeSpan(9, 15, 32), Rating = 10 });
            tl.Add(new TourLogs() { Difficulty = Model.Enums.Difficulty.Medium, TotalTime = new TimeSpan(4, 30, 0), Rating = 7 });

            // Act
            var result = isChildFriendly(tl);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        // Test child friendliness - answer false
        [Test]
        public void ChildFriendlyTestShouldBeFalse() {
            // Arrange
            ObservableCollection<TourLogs> tl = new ObservableCollection<TourLogs>();
            tl.Add(new TourLogs() { Difficulty = Model.Enums.Difficulty.None, TotalTime = new TimeSpan(1, 0, 0), Rating = 4 });
            tl.Add(new TourLogs() { Difficulty = Model.Enums.Difficulty.Easy, TotalTime = new TimeSpan(9, 15, 32), Rating = 1 });
            tl.Add(new TourLogs() { Difficulty = Model.Enums.Difficulty.Medium, TotalTime = new TimeSpan(4, 30, 0), Rating = 2 });

            // Act
            var result = isChildFriendly(tl);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        // Test child friendliness - nothing in the ObservableCollection
        [Test]
        public void ChildFriendlyTestEmptyObservableCollectionTest() {
            // Arrange
            ObservableCollection<TourLogs> tl = new ObservableCollection<TourLogs>();

            // Act
            var result = isChildFriendly(tl);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        // Test child friendliness - absurd values in the ObservableCollection
        [Test]
        public void ChildFriendlyTestAbsurdValuesInObservableCollectionTest() {
            // Arrange
            ObservableCollection<TourLogs> tl = new ObservableCollection<TourLogs>();
            tl.Add(new TourLogs() { Difficulty = Model.Enums.Difficulty.None, TotalTime = new TimeSpan(1, 0, 0), Rating = 4 });
            tl.Add(new TourLogs() { Difficulty = Model.Enums.Difficulty.Easy, TotalTime = new TimeSpan(9, 15, 32), Rating = 11 });
            tl.Add(new TourLogs() { Difficulty = Model.Enums.Difficulty.Medium, TotalTime = new TimeSpan(4, 30, 0), Rating = -1 });

            // Act
            var result = isChildFriendly(tl);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        public bool isChildFriendly(ObservableCollection<TourLogs> tls) {
            int items = tls.Count;
            int rating = 0;
            int difficulty = 0;
            int totaltime = 0;

            if (items == 0) {
                return false;
            }

            foreach (var log in tls) {
                rating += log.Rating;

                difficulty += (int)log.Difficulty;

                int days = log.TotalTime.Days;
                int hours = log.TotalTime.Hours;
                int minutes = log.TotalTime.Minutes;
                int seconds = log.TotalTime.Seconds;
                totaltime += (days * 24 * 60 * 60) + (hours * 60 * 60) + (minutes * 60) + seconds;
            }

            double avgRating = rating / items;
            double avgDifficulty = difficulty / items;
            double avgTotalTime = totaltime / items;

            // if on average rating is better or equal to 5, difficulty is lower than medium (2)
            // and the total time is less than 12 hours - the tour is child friendly
            return avgRating >= 5 && avgDifficulty < 2 && avgTotalTime < 43201;
        }
        
    }
}
