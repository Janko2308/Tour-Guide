using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Model;

namespace Tour_Planner.Test {
    public class DatabaseMockTests {
        [SetUp]
        public void Setup() {
        }

        // Create a mock repository and try adding a tour item
        [Test]
        public void AddTourItemTest() {
            // Arrange
            // TODO

            // Act
            bool result = database.AddTourItem(tour);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
