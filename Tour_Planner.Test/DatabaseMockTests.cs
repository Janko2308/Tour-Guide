using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
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

        [Test]
        public void AddTourTest() {
            // Arrange
            var dal = new DataManagerEntityFrameworkImpl();
            FieldInfo context = typeof(DataManagerEntityFrameworkImpl).GetField
                ("context", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo onConfiguring = typeof(TourDbContext).GetMethod
                ("onConfiguring", BindingFlags.NonPublic | BindingFlags.Instance);
            var mockContext = new Mock<TourDbContext>();
            context.SetValue(dal, mockContext);
            onConfiguring.Invoke(mockContext, new object[] { new DbContextOptionsBuilder() });
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

        /*public void AddTour(TourItem t) {
            // check if the t.name already exists
            if (context.TourItems.Any(x => x.Name == t.Name)) {
                throw new DbUpdateException($"Tour name {t.Name} already used");
            }
            
            context.TourItems.Add(t);
            context.SaveChanges();
        }*/
    }
}
