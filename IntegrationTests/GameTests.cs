using GameplayContext;
using NUnit.Framework;
using ViewModels;
using System.Threading.Tasks;
using System.Threading;
using System;

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
        public async Task WhenIStartPlayingISeeArtworkFalling()
        {
            // GIVEN: I have started playing
            await InitGameAsync();

            // WHEN: Time ticks by
            for (int i = 0; i < Game.NumberStepsToDrop; i++)
            {
                SetupHelper.GameTimer.DoTick();

                // THEN: I see artwork falling
                Assert.AreEqual(i + 1, SetupHelper.Locator.InGameVm.FallingTileYPos);
            }
        }

        [Test]
        public async Task NewArtworkFallsWhenCurrentReachesTheBottom()
        {
            // GIVEN: Artwork is falling
            await InitGameAsync();
            var numberNewTiles = 0;
            SetupHelper.Locator.InGameVm.PropertyChanged += (sender, args) => 
            {
                if (args.PropertyName == nameof(InGameViewModel.FallingTileImage))
                {
                    numberNewTiles++;
                }
            };

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
            Assert.AreEqual(1, SetupHelper.Locator.InGameVm.Column5.Count);
        }

        [Test]
        public async Task GameEndsWhenThereIsNoRoomForNewArtwork()
        {
            // GIVEN: I am playing the game
            await InitGameAsync();

            // WHEN: There is no room for new artwork
            for (int i = 0; i <= Game.NumberStepsToDrop; i++)
            for (int j = 0; j <= Game.NumberStepsToDrop; j++)
            {
                SetupHelper.GameTimer.DoTick();
            }

            // THEN: I am taken to the game over screen
            Assert.AreEqual(nameof(GameOverViewModel), SetupHelper.NavigationService.CurrentPageKey);
        }

        [Test]
        public async Task IfIMoveRightThenTheNextColumnIsPopulated()
        {
            // GIVEN: I am playing the game
            await InitGameAsync();

            // WHEN: I move right
            SetupHelper.Locator.InGameVm.MoveRightCommand.Execute(null);

            // AND: The Artwork falls
            for (int i = 0; i <= Game.NumberStepsToDrop; i++)
            {
                SetupHelper.GameTimer.DoTick();
            }

            // THEN: the next column is populated
            Assert.AreEqual(1, SetupHelper.Locator.InGameVm.Column6.Count);
        }

        [Test]
        public async Task IfIMoveLeftThenThePreviousColumnIsPopulated()
        {
            // GIVEN: I am playing the game
            await InitGameAsync();

            // WHEN: I move left
            SetupHelper.Locator.InGameVm.MoveLeftCommand.Execute(null);

            // AND: The Artwork falls
            for (int i = 0; i <= Game.NumberStepsToDrop; i++)
            {
                SetupHelper.GameTimer.DoTick();
            }

            // THEN: the next column is populated
            Assert.AreEqual(1, SetupHelper.Locator.InGameVm.Column4.Count);
        }

        [Test]
        public async Task IfIPressDropThenTheFallingArtworkDrops()
        {
            // GIVEN: I am playing the game
            await InitGameAsync();

            // WHEN: I move left
            SetupHelper.Locator.InGameVm.DropCommand.Execute(null);

            // Then: The Artwork falls immediately
            SetupHelper.GameTimer.DoTick();
            Assert.AreEqual(1, SetupHelper.Locator.InGameVm.Column5.Count);
        }

        private static Task InitGameAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            Task.Factory.StartNew(() => 
            {
                SetupHelper.Locator.ArtworkPreviewVm.SearchCommand.Execute ("u2");
                SetupHelper.Locator.ArtworkPreviewVm.GoToGameCommand.CanExecuteChanged += (s, e) =>
                {
                    SetupHelper.Locator.InGameVm.Initialise ();
                    tcs.SetResult(true);
                };
            });

            return tcs.Task;
        }
    }
}