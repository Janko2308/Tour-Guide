using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.DAL;
using Tour_Planner.Model;

namespace Tour_Planner.Test {
    public class DatabaseMockTests {
        [SetUp]
        public void Setup() {
        }

        // Test for adding a tour to the database
        [Test]
        public void AddTourTest() {
            // Arrange
            var dal = new DataManagerEntityFrameworkImpl();
            FieldInfo context = typeof(DataManagerEntityFrameworkImpl).GetField
                ("context", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo onConfiguring = typeof(TourDbContext).GetMethod
                ("onConfiguring", BindingFlags.NonPublic | BindingFlags.Instance);
            var mockContext = new Mock<TourDbContext>();
            context.SetValue(dal, mockContext.Object);
            //onConfiguring.Invoke(mockContext, new object[] { new DbContextOptionsBuilder() });
            //onConfiguring.CallingConvention
            onConfiguring.Invoke(mockContext.Object, new object[] { new DbContextOptionsBuilder() });
            var t = new TourItem();
            List<TourItem> tours = new List<TourItem>();
            mockContext.Setup(x => x.TourItems.Add(It.IsAny<TourItem>())).Callback<TourItem>(tours.Add);

            // Act
            dal.AddTour(t);

            // Assert
            Assert.That(tours.Count, Is.EqualTo(1));
            Assert.That(tours.Contains(t));
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        // Test for editing a tour in the database TODO
        [Test]
        public void EditTourTest() {
            // Arrange
            var dal = new DataManagerEntityFrameworkImpl();
            FieldInfo context = typeof(DataManagerEntityFrameworkImpl).GetField
                ("context", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo onConfiguring = typeof(TourDbContext).GetMethod
                ("onConfiguring", BindingFlags.NonPublic | BindingFlags.Instance);
            var mockContext = new Mock<TourDbContext>();
            context.SetValue(dal, mockContext);
            //onConfiguring.Invoke(mockContext, new object[] { new DbContextOptionsBuilder() });
            //onConfiguring.CallingConvention
            var t = new TourItem {
                Name = "TestTour",
                Description = "TestDescription"
            };
            List<TourItem> tours = new List<TourItem> { t };
            mockContext.Setup(x => x.TourItems.Find(It.IsAny<string>())).Returns<int>(id => tours.Find(x => x.Id == id));

            // Act
            dal.EditTour(t);

            // Assert
            Assert.That(tours.Count, Is.EqualTo(1));
            Assert.That(tours.Contains(t));
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        // Test for deleting a tour from the database TODO
        [Test]
        public void DeleteTourTest() {
            // Arrange
            var dal = new DataManagerEntityFrameworkImpl();
            FieldInfo context = typeof(DataManagerEntityFrameworkImpl).GetField
                ("context", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo onConfiguring = typeof(TourDbContext).GetMethod
                ("onConfiguring", BindingFlags.NonPublic | BindingFlags.Instance);
            var mockContext = new Mock<TourDbContext>();
            context.SetValue(dal, mockContext);
            //onConfiguring.Invoke(mockContext, new object[] { new DbContextOptionsBuilder() });
            //onConfiguring.CallingConvention
            var t = new TourItem {
                Name = "TestTour",
                Description = "TestDescription"
            };
            List<TourItem> tours = new List<TourItem> { t };
            mockContext.Setup(x => x.TourItems.Remove(It.IsAny<TourItem>())).Callback<TourItem>(t => tours.Remove(t));

            // Act
            dal.DeleteTour(t);

            // Assert
            Assert.That(tours.Count, Is.EqualTo(0));
            Assert.That(tours.Contains(t), Is.False);
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        // Test for adding a tourlog TODO
        [Test]
        public void AddTourLogTest() {
            // Arrange
            var dal = new DataManagerEntityFrameworkImpl();
            FieldInfo context = typeof(DataManagerEntityFrameworkImpl).GetField
                ("context", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo onConfiguring = typeof(TourDbContext).GetMethod
                ("onConfiguring", BindingFlags.NonPublic | BindingFlags.Instance);
            var mockContext = new Mock<TourDbContext>();
            context.SetValue(dal, mockContext);
            //onConfiguring.Invoke(mockContext, new object[] { new DbContextOptionsBuilder() });
            //onConfiguring.CallingConvention
            var t = new TourLogs();
            List<TourLogs> tourLogs = new List<TourLogs>();
            mockContext.Setup(x => x.TourLogItems.Add(It.IsAny<TourLogs>())).Callback<TourLogs>(tourLogs.Add);

            // Act
            dal.AddTourLog(t);

            // Assert
            Assert.That(tourLogs.Count, Is.EqualTo(1));
            Assert.That(tourLogs.Contains(t));
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        // Test for editing a tourlog TODO
        [Test]
        public void EditTourLogTest() {
            // Arrange
            var dal = new DataManagerEntityFrameworkImpl();
            FieldInfo context = typeof(DataManagerEntityFrameworkImpl).GetField
                ("context", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo onConfiguring = typeof(TourDbContext).GetMethod
                ("onConfiguring", BindingFlags.NonPublic | BindingFlags.Instance);
            var mockContext = new Mock<TourDbContext>();
            context.SetValue(dal, mockContext);
            //onConfiguring.Invoke(mockContext, new object[] { new DbContextOptionsBuilder() });
            //onConfiguring.CallingConvention
            var t = new TourLogs {
                Comment = "TestTour",
                TotalTime = new TimeSpan(1, 1, 1)
            };
            List<TourLogs> tourLogs = new List<TourLogs> { t };
            mockContext.Setup(x => x.TourLogItems.Find(It.IsAny<string>())).Returns<int>(id => tourLogs.Find(x => x.Id == id));

            // Act
            dal.EditTourLog(t);

            // Assert
            Assert.That(tourLogs.Count, Is.EqualTo(1));
            Assert.That(tourLogs.Contains(t));
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        // Test for deleting a tourlog TODO
        [Test]
        public void DeleteTourLogTest() {
            // Arrange
            var dal = new DataManagerEntityFrameworkImpl();
            FieldInfo context = typeof(DataManagerEntityFrameworkImpl).GetField
                ("context", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo onConfiguring = typeof(TourDbContext).GetMethod
                ("onConfiguring", BindingFlags.NonPublic | BindingFlags.Instance);
            var mockContext = new Mock<TourDbContext>();
            context.SetValue(dal, mockContext);
            //onConfiguring.Invoke(mockContext, new object[] { new DbContextOptionsBuilder() });
            //onConfiguring.CallingConvention
            var t = new TourLogs {
                Comment = "TestTour",
                TotalTime = new TimeSpan(1, 1, 1)
            };
            List<TourLogs> tourLogs = new List<TourLogs> { t };
            mockContext.Setup(x => x.TourLogItems.Remove(It.IsAny<TourLogs>())).Callback<TourLogs>(t => tourLogs.Remove(t));

            // Act
            dal.DeleteTourLog(t);

            // Assert
            Assert.That(tourLogs.Count, Is.EqualTo(0));
            Assert.That(tourLogs.Contains(t), Is.False);
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        // test to fetch all Tours from db
        [Test]
        public void GetAllToursTest() {
            // Arrange
            var dal = new DataManagerEntityFrameworkImpl();
            FieldInfo context = typeof(DataManagerEntityFrameworkImpl).GetField
                ("context", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo onConfiguring = typeof(TourDbContext).GetMethod
                ("onConfiguring", BindingFlags.NonPublic | BindingFlags.Instance);
            var mockContext = new Mock<TourDbContext>();
            context.SetValue(dal, mockContext);
            //onConfiguring.Invoke(mockContext, new object[] { new DbContextOptionsBuilder() });
            //onConfiguring.CallingConvention
            var t = new TourItem {
                Name = "TestTour",
                Description = "TestDescription"
            };
            List<TourItem> tours = new List<TourItem> { t };
            //mockContext.Setup(x => x.TourItems).Returns(tours.AsQueryable());

            // Act
            var result = dal.GetTours();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.Contains(t));
        }
    }
}
