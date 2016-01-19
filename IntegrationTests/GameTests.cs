using GameplayContext;
using NUnit.Framework;
using ViewModels;

namespace IntegrationTests
{
    [TestFixture]
    public class Test
    {
        [SetUp]
        public void Setup()
        {
            SetupHelper.InitialiseApp();
        }

        [Test]
        public void WhenIStartPlayingISeeArtworkFalling()
        {
            // GIVEN: I have started playing
            SetupHelper.Locator.InGameVm.Initialise();

            // WHEN: Time ticks by
            for (int i = 0; i < Game.NumberStepsToDrop; i++)
            {
                SetupHelper.GameTimer.DoTick();

                // THEN: I see artwork falling
                Assert.AreEqual(i + 1, SetupHelper.Locator.InGameVm.FallingTileYPos);
            }
        }

        [Test]
        public void NewArtworkFallsWhenCurrentReachesTheBottom()
        {
            // GIVEN: Artwork is falling
            var numberNewTiles = 0;
            SetupHelper.Locator.InGameVm.Game.NewTile += (sender, args) => numberNewTiles++;
            SetupHelper.Locator.InGameVm.Initialise();

            // WHEN: The artwork reaches the lowest level
            for (int i = 0; i < Game.NumberStepsToDrop; i++)
            {
                SetupHelper.GameTimer.DoTick();
            }

            // THEN: New Artwork starts falling
            SetupHelper.GameTimer.DoTick();
            Assert.AreEqual(1, numberNewTiles);
            Assert.AreEqual(0, SetupHelper.Locator.InGameVm.FallingTileYPos);

            // AND: The original artwork remains at the bottom
            Assert.AreEqual(1, SetupHelper.Locator.InGameVm.Game.GetColumn(0).Count);
        }

        [Test]
        public void GameEndsWhenThereIsNoRoomForNewArtwork()
        {
            // GIVEN: I am playing the game
            SetupHelper.Locator.InGameVm.Initialise();

            // WHEN: There is no room for new artwork
            for (int i = 0; i <= Game.NumberStepsToDrop; i++)
            for (int j = 0; j <= Game.NumberStepsToDrop; j++)
            {
                SetupHelper.GameTimer.DoTick();
            }

            // THEN: I am taken to the game over screen
            Assert.AreEqual(nameof(GameOverViewModel), SetupHelper.NavigationService.CurrentPageKey);
        }
    }
}