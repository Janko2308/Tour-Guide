using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner.Test {
    public class EnumTests {
        [SetUp]
        public void Setup() {
        }

        // Assess parsing into enum when string chosen "pedestrian"
        [Test]
        public void ParseTransportTest() {
            // Arrange
            string transport = "pedestrian";

            // Act
            Model.Enums.Transport transportEnum = (Model.Enums.Transport)Enum.Parse(typeof(Model.Enums.Transport), transport);

            // Assert
            Assert.That(Model.Enums.Transport.pedestrian, Is.EqualTo(transportEnum));
        }

        // Assess parsing into enum when string chosen "Easy"
        [Test]
        public void ParseDifficultyTest() {
            // Arrange
            string difficulty = "Easy";

            // Act
            Model.Enums.Difficulty difficultyEnum = (Model.Enums.Difficulty)Enum.Parse(typeof(Model.Enums.Difficulty), difficulty);

            // Assert
            Assert.That(Model.Enums.Difficulty.Easy, Is.EqualTo(difficultyEnum));
        }

        // Assess acquiring the value of transport enum
        [Test]
        public void GetTransportValueTest() {
            // Arrange
            Model.Enums.Transport transport = Model.Enums.Transport.pedestrian;
            string transportShould = "pedestrian";

            // Act
            string transportValue = transport.ToString();

            // Assert
            Assert.That(transportShould, Is.EqualTo(transportValue));
        }

        // Assess acquiring the value of difficulty enum - integer
        [Test]
        public void GetDifficultyValueTest() {
            // Arrange
            Model.Enums.Difficulty difficulty = Model.Enums.Difficulty.Easy;
            int difficultyShould = 1;

            // Act
            int difficultyValue = (int)difficulty;

            // Assert
            Assert.That(difficultyShould, Is.EqualTo(difficultyValue));
        }

        // Try setting transport as car, and see if transport enum parse fails, as there is no definition for car
        [Test]
        public void ParseTransportFailTest() {
            // Arrange
            string invalidTransport = "car";

            // Act
            var parsedTransport = Enum.TryParse<Model.Enums.Transport>(invalidTransport, out var result);

            // Assert
            Assert.IsFalse(parsedTransport);
            Assert.AreEqual(default(Model.Enums.Transport), result);
        }

        // Try setting difficulty as hell, and see if difficulty enum parse fails, as there is no definition for hell
        [Test]
        public void ParseDifficultyFailTest() {
            // Arrange
            string invalidDifficulty = "hell";

            // Act
            var parsedDifficulty = Enum.TryParse<Model.Enums.Difficulty>(invalidDifficulty, out var result);

            // Assert
            Assert.IsFalse(parsedDifficulty);
            Assert.AreEqual(default(Model.Enums.Difficulty), result);
        }
    }
}
