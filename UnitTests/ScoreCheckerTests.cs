using NUnit.Framework;
using System;
using System.Collections.Generic;
using SharedKernel;
using GameplayContext;
using System.Collections.ObjectModel;

namespace UnitTests
{
    [TestFixture]
    public class ScoreCheckerTests
    {
        [Test]
        public void NoItemsNoScore()
        {
            // Arrange
            var column1 = new ObservableCollection<Tile>();
            var column2 = new ObservableCollection<Tile>();
            var column3 = new ObservableCollection<Tile>();
            var columns = new List<ObservableCollection<Tile>>() { column1, column2, column3 };

            // Act
            var score = new ScoreChecker().CheckScoreAndUpdate(columns);

            // Assert
            Assert.AreEqual(0, score);
            Assert.AreEqual(0, column1.Count);
            Assert.AreEqual(0, column3.Count);
            Assert.AreEqual(0, column2.Count);
        }

        [Test]
        public void ThreeInAColumnIsAMatch()
        {
            // Arrange
            var column1 = new ObservableCollection<Tile>() { Tile("2"), Tile("3"), Tile("4"), Tile("5") };
            var column2 = new ObservableCollection<Tile>() { Tile("1"), Tile("2"), Tile("2"), Tile("2"), Tile("5") };
            var column3 = new ObservableCollection<Tile>() { Tile("1") };
            var columns = new List<ObservableCollection<Tile>>() { column1, column2, column3 };

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
            var column1 = new ObservableCollection<Tile>() { Tile("2"), Tile("3"), Tile("4"), Tile("5") };
            var column2 = new ObservableCollection<Tile>() { Tile("2"), Tile("2"), Tile("2"), Tile("2"), Tile("2") };
            var column3 = new ObservableCollection<Tile>() { Tile("1") };
            var columns = new List<ObservableCollection<Tile>>() { column1, column2, column3 };

            // Act
            var score = new ScoreChecker().CheckScoreAndUpdate(columns);

            // Assert
            Assert.AreEqual(400, score);
            Assert.AreEqual(4, column1.Count);
            Assert.AreEqual(1, column3.Count);
            Assert.AreEqual(0, column2.Count);
        }

        [Test]
        public void MatchOnlyCountsForColumnEvenIfItStartsFromPreviousColumn()
        {
            // Arrange
            var column1 = new ObservableCollection<Tile>();
            var column2 = new ObservableCollection<Tile>();
            var column3 = new ObservableCollection<Tile>();
            var column4 = new ObservableCollection<Tile>()  { Tile("2") };

            var column5 = new ObservableCollection<Tile>() { Tile("2"), Tile("2"), Tile("2"), Tile("2") };
            var column6 = new ObservableCollection<Tile>() { Tile("1") };
            var columns = new List<ObservableCollection<Tile>>() { column1, column2, column3, column4, column5, column6 };

            // Act
            var score = new ScoreChecker().CheckScoreAndUpdate(columns);

            // Assert
            Assert.AreEqual(250, score);
            Assert.AreEqual(0, column1.Count);
            Assert.AreEqual(0, column2.Count);
            Assert.AreEqual(0, column3.Count);
            Assert.AreEqual(1, column4.Count);
            Assert.AreEqual(0, column5.Count);
            Assert.AreEqual(1, column6.Count);
        }

        [Test]
        public void MatchOnlyCountsForColumnEvenIfItRunsIntoNextColumn()
        {
            // Arrange
            var column1 = new ObservableCollection<Tile>();
            var column2 = new ObservableCollection<Tile>();
            var column3 = new ObservableCollection<Tile>();
            var column4 = new ObservableCollection<Tile>();

            var column5 = new ObservableCollection<Tile>() { Tile("1"), Tile("2"), Tile("2"), Tile("2") };
            var column6 = new ObservableCollection<Tile>() { Tile("2") };
            var columns = new List<ObservableCollection<Tile>>() { column1, column2, column3, column4, column5, column6 };

            // Act
            var score = new ScoreChecker().CheckScoreAndUpdate(columns);

            // Assert
            Assert.AreEqual(100, score);
            Assert.AreEqual(0, column1.Count);
            Assert.AreEqual(0, column2.Count);
            Assert.AreEqual(0, column3.Count);
            Assert.AreEqual(0, column4.Count);
            Assert.AreEqual(1, column5.Count);
            Assert.AreEqual(1, column6.Count);
        }

        [Test]
        public void MatchInFinalColumnIsAScore()
        {
            // Arrange
            var column1 = new ObservableCollection<Tile>();
            var column2 = new ObservableCollection<Tile>();
            var column3 = new ObservableCollection<Tile>();
            var column4 = new ObservableCollection<Tile>();

            var column5 = new ObservableCollection<Tile>() { Tile("1"), Tile("2"), Tile("2"), Tile("2") };
            var columns = new List<ObservableCollection<Tile>>() { column1, column2, column3, column4, column5 };

            // Act
            var score = new ScoreChecker().CheckScoreAndUpdate(columns);

            // Assert
            Assert.AreEqual(100, score);
            Assert.AreEqual(0, column1.Count);
            Assert.AreEqual(0, column2.Count);
            Assert.AreEqual(0, column3.Count);
            Assert.AreEqual(0, column4.Count);
            Assert.AreEqual(1, column5.Count);
        }

        [Test]
        public void MatchInFinalColumnIsAScore2()
        {
            // Arrange
            var column1 = new ObservableCollection<Tile>();
            var column2 = new ObservableCollection<Tile>();
            var column3 = new ObservableCollection<Tile>();
            var column4 = new ObservableCollection<Tile>();

            var column5 = new ObservableCollection<Tile>() { Tile("2"), Tile("2"), Tile("2") };
            var columns = new List<ObservableCollection<Tile>>() { column1, column2, column3, column4, column5 };

            // Act
            var score = new ScoreChecker().CheckScoreAndUpdate(columns);

            // Assert
            Assert.AreEqual(100, score);
            Assert.AreEqual(0, column1.Count);
            Assert.AreEqual(0, column2.Count);
            Assert.AreEqual(0, column3.Count);
            Assert.AreEqual(0, column4.Count);
            Assert.AreEqual(0, column5.Count);
        }

        [Test]
        public void MatchButEmptyFinalColumnIsAScore()
        {
            // Arrange
            var column1 = new ObservableCollection<Tile>();
            var column2 = new ObservableCollection<Tile>();
            var column3 = new ObservableCollection<Tile>(){ Tile("1"), Tile("2"), Tile("2"), Tile("2") };
            var column4 = new ObservableCollection<Tile>();
            var column5 = new ObservableCollection<Tile>();
            var columns = new List<ObservableCollection<Tile>>() { column1, column2, column3, column4, column5 };

            // Act
            var score = new ScoreChecker().CheckScoreAndUpdate(columns);

            // Assert
            Assert.AreEqual(100, score);
            Assert.AreEqual(0, column1.Count);
            Assert.AreEqual(0, column2.Count);
            Assert.AreEqual(1, column3.Count);
            Assert.AreEqual(0, column4.Count);
            Assert.AreEqual(0, column5.Count);
        }

        [Test]
        public void MatchFollowedByEmptyColumnFollowedByPopulatedColumnIsAScore()
        {
            // Arrange
            var column1 = new ObservableCollection<Tile>();
            var column2 = new ObservableCollection<Tile>();
            var column3 = new ObservableCollection<Tile>(){  Tile("2"), Tile("2"), Tile("2") };
            var column4 = new ObservableCollection<Tile>();
            var column5 = new ObservableCollection<Tile>() { Tile("1") };
            var columns = new List<ObservableCollection<Tile>>() { column1, column2, column3, column4, column5 };

            // Act
            var score = new ScoreChecker().CheckScoreAndUpdate(columns);

            // Assert
            Assert.AreEqual(100, score);
            Assert.AreEqual(0, column1.Count);
            Assert.AreEqual(0, column2.Count);
            Assert.AreEqual(0, column3.Count);
            Assert.AreEqual(0, column4.Count);
            Assert.AreEqual(1, column5.Count);
        }

        /*
                HORIZONTAL MATCH TESTS
         */


        [Test]
        public void HorizontalMatchIsAScore()
        {
            // Arrange
            var column1 = new ObservableCollection<Tile>() { Tile("1") };
            var column2 = new ObservableCollection<Tile>(){  Tile("1") };
            var column3 = new ObservableCollection<Tile>(){  Tile("1")};
            var column4 = new ObservableCollection<Tile>();
            var column5 = new ObservableCollection<Tile>();
            var columns = new List<ObservableCollection<Tile>>() { column1, column2, column3, column4, column5 };

            // Act
            var score = new ScoreChecker().CheckScoreAndUpdate(columns);

            // Assert
            Assert.AreEqual(100, score);
            Assert.AreEqual(0, column1.Count);
            Assert.AreEqual(0, column2.Count);
            Assert.AreEqual(0, column3.Count);
            Assert.AreEqual(0, column4.Count);
            Assert.AreEqual(0, column5.Count);
        }

        [Test]
        public void HorizontalMatchAtEndIsAScore()
        {
            // Arrange
            var column1 = new ObservableCollection<Tile>();
            var column2 = new ObservableCollection<Tile>();
            var column3 = new ObservableCollection<Tile>(){  Tile("1")};
            var column4 = new ObservableCollection<Tile>(){  Tile("1") };
            var column5 = new ObservableCollection<Tile>() { Tile("1") };
            var columns = new List<ObservableCollection<Tile>>() { column1, column2, column3, column4, column5 };

            // Act
            var score = new ScoreChecker().CheckScoreAndUpdate(columns);

            // Assert
            Assert.AreEqual(100, score);
            Assert.AreEqual(0, column1.Count);
            Assert.AreEqual(0, column2.Count);
            Assert.AreEqual(0, column3.Count);
            Assert.AreEqual(0, column4.Count);
            Assert.AreEqual(0, column5.Count);
        }

        [Test]
        public void HorizontalMatchAtEndWithPopulatedColumsIsAScore()
        {
            // Arrange
            var column1 = new ObservableCollection<Tile>();
            var column2 = new ObservableCollection<Tile>(){  Tile("1"), Tile("3"), Tile("4"), Tile("1")};;
            var column3 = new ObservableCollection<Tile>(){  Tile("1"), Tile("1")};
            var column4 = new ObservableCollection<Tile>(){  Tile("2"), Tile("1") };
            var column5 = new ObservableCollection<Tile>() { Tile("3"), Tile("1") };
            var columns = new List<ObservableCollection<Tile>>() { column1, column2, column3, column4, column5 };

            // Act
            var score = new ScoreChecker().CheckScoreAndUpdate(columns);

            // Assert
            Assert.AreEqual(100, score);
            Assert.AreEqual(0, column1.Count);
            Assert.AreEqual(4, column2.Count);
            Assert.AreEqual(1, column3.Count);
            Assert.AreEqual(1, column4.Count);
            Assert.AreEqual(1, column5.Count);
        }

        [Test]
        public void HorizontalRunOfFourScoresWhenNotAtTopOfColumn()
        {
            // Arrange
            var column1 = new ObservableCollection<Tile>();
            var column2 = new ObservableCollection<Tile>(){  Tile("3"), Tile("1"), Tile("4"), Tile("2")};;
            var column3 = new ObservableCollection<Tile>(){  Tile("5"), Tile("1")};
            var column4 = new ObservableCollection<Tile>(){  Tile("2"), Tile("1"), Tile("5") };
            var column5 = new ObservableCollection<Tile>() { Tile("3"), Tile("1"), Tile("4"), Tile("2") };
            var columns = new List<ObservableCollection<Tile>>() { column1, column2, column3, column4, column5 };

            // Act
            var score = new ScoreChecker().CheckScoreAndUpdate(columns);

            // Assert
            Assert.AreEqual(250, score);
            Assert.AreEqual(0, column1.Count);
            Assert.AreEqual(3, column2.Count);
            Assert.AreEqual(1, column3.Count);
            Assert.AreEqual(2, column4.Count);
            Assert.AreEqual(3, column5.Count);
        }


        public Tile Tile(string imageId)
        {
            return new Tile { ImageId = imageId };
        }
    }
}

