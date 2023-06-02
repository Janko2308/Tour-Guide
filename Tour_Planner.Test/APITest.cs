using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.BL;
using Tour_Planner.DAL;
using Tour_Planner.Model.Structs;

namespace Tour_Planner.Test {
    public class APITest {
        [SetUp]
        public void Setup() {
        }

        // Assess whether MapQuestAPI works as it should
        [Test]
        public void MapQuestAPITest() {
            // Arrange
            TourCreation tc = new TourCreation();
            MapCreator mc = new MapCreator();

            // Act
            tc = mc.CreateMap("Berlin", "Hamburg", Model.Enums.Transport.shortest).Result;

            // Assert
            // check if tc.distance is larger than 0, tc.estimatedtime is correctly parsed to a timespan and tc.mapimage is not null
            Assert.IsTrue(tc.Distance > 0 && tc.EstimatedTime is TimeSpan && tc.Picture != null);
        }

        // Assess if API fails upon wrong input
        [Test]
        public void MapQuestAPIFailTest() {
            // Arrange
            TourCreation tc = new TourCreation();
            MapCreator mc = new MapCreator();
            
            // Act
            tc = mc.CreateMap("adgssssssss", "712 7 78dbcxm", Model.Enums.Transport.shortest).Result;

            // Assert
            // checks if tc is still empty
            Assert.IsTrue(tc.Distance == null && tc.EstimatedTime == null && tc.Picture == null);
        }
    }
}
