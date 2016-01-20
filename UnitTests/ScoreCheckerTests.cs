using NUnit.Framework;
using System;
using System.Collections.Generic;
using SharedKernel;
using GameplayContext;

namespace UnitTests
{
    [TestFixture]
    public class ScoreCheckerTests
    {
        [Test]
        public void ThreeInAColumnIsAMatch()
        {
            // Arrange
            var column1 = new List<Tile>() { Tile("2"), Tile("3"), Tile("4"), Tile("5") };
            var column2 = new List<Tile>() { Tile("1"), Tile("2"), Tile("2"), Tile("2"), Tile("5") };
            var column3 = new List<Tile>() { Tile("1") };
            var columns = new List<List<Tile>>() { column1, column2, column3 };

            // Act
            var score = new ScoreChecker().CheckScoreAndUpdate(columns);

            // Assert
            Assert.AreEqual(100, score);
            Assert.AreEqual(4, column1.Count);
            Assert.AreEqual(1, column3.Count);
            Assert.AreEqual(2, column2.Count);
            Assert.AreEqual("1", column2[0].ImageId);
            Assert.AreEqual("5", column2[1].ImageId);
        }

        [Test]
        public void FiveInAColumnIsAMatch()
        {
            // Arrange
            var column1 = new List<Tile>() { Tile("2"), Tile("3"), Tile("4"), Tile("5") };
            var column2 = new List<Tile>() { Tile("2"), Tile("2"), Tile("2"), Tile("2"), Tile("2") };
            var column3 = new List<Tile>() { Tile("1") };
            var columns = new List<List<Tile>>() { column1, column2, column3 };

            // Act
            var score = new ScoreChecker().CheckScoreAndUpdate(columns);

            // Assert
            Assert.AreEqual(400, score);
            Assert.AreEqual(4, column1.Count);
            Assert.AreEqual(1, column3.Count);
            Assert.AreEqual(0, column2.Count);
        }

        public Tile Tile(string imageId)
        {
            return new Tile { ImageId = imageId };
        }
    }
}

