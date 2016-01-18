using NUnit.Framework;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Ioc;

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
            SetupHelper.GameTimer.DoTick();

            // THEN: I see artwork falling
            Assert.AreEqual(1, SetupHelper.Locator.InGameVm.YPosition);

        }
    }
}

