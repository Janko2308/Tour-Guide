using Tour_Planner.Model;
using Tour_Planner.Model.Structs;

namespace Tour_Planner.Test {
    public class ModelTests {
        [SetUp]
        public void Setup() {
        }

        // Assess whether the TourItem class is working as intended
        [Test]
        public void TourItemCreationTest() {
            // Arrange
            TourItem tourItem = new TourItem();

            // Act
            tourItem.Name = "TestTour";
            tourItem.Description = "TestDescription";
            tourItem.From = "TestFrom";
            tourItem.To = "TestTo";
            tourItem.TransportType = Model.Enums.Transport.fastest;
            tourItem.Distance = 1.0;
            tourItem.EstimatedTime = new System.TimeSpan(1, 1, 1);
            tourItem.TourInfo = new byte[1];

            // Assert
            Assert.That("TestTour", Is.EqualTo(tourItem.Name));
            Assert.That("TestDescription", Is.EqualTo(tourItem.Description));
            Assert.That("TestFrom", Is.EqualTo(tourItem.From));
            Assert.That("TestTo", Is.EqualTo(tourItem.To));
            Assert.That(Model.Enums.Transport.fastest, Is.EqualTo(tourItem.TransportType));
            Assert.That(1.0, Is.EqualTo(tourItem.Distance));
            Assert.That(new System.TimeSpan(1, 1, 1), Is.EqualTo(tourItem.EstimatedTime));
            Assert.That(new byte[1], Is.EqualTo(tourItem.TourInfo));
        }

        // Assess whether the TourLogs class is working as intended
        [Test]
        public void TourLogCreationTest() {
            // Arrange
            TourLogs tourLog = new TourLogs();

            // Act
            tourLog.TourId = 1;
            tourLog.DateTime = new System.DateTime(2021, 1, 1, 12, 41, 11);
            tourLog.Comment = "TestComment";
            tourLog.Difficulty = Model.Enums.Difficulty.Easy;
            tourLog.TotalTime = new System.TimeSpan(1, 1, 1, 1);
            tourLog.Rating = 1;

            // Assert
            Assert.That(1, Is.EqualTo(tourLog.TourId));
            Assert.That(new System.DateTime(2021, 1, 1, 12, 41, 11), Is.EqualTo(tourLog.DateTime));
            Assert.That("TestComment", Is.EqualTo(tourLog.Comment));
            Assert.That(Model.Enums.Difficulty.Easy, Is.EqualTo(tourLog.Difficulty));
            Assert.That(new System.TimeSpan(1, 1, 1, 1), Is.EqualTo(tourLog.TotalTime));
            Assert.That(1, Is.EqualTo(tourLog.Rating));
        }

        // Assess whether it is possible to set and get Data from struct TourCreation
        [Test]
        public void TourCreationStructTest() {
            // Arrange
            TourCreation tourCreation = new TourCreation();

            // Act
            tourCreation.Distance = 1.0;
            tourCreation.EstimatedTime = new System.TimeSpan(1, 1, 1);
            tourCreation.Picture = new byte[1];

            // Assert
            Assert.That(1.0, Is.EqualTo(tourCreation.Distance));
            Assert.That(new System.TimeSpan(1, 1, 1), Is.EqualTo(tourCreation.EstimatedTime));
            Assert.That(new byte[1], Is.EqualTo(tourCreation.Picture));
        }
    }
}