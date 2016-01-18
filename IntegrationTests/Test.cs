﻿using NUnit.Framework;
using GalaSoft.MvvmLight.Views;

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
            // TODO: Simulate passing of time

            // THEN: I see artwork falling
            Assert.AreEqual(100, SetupHelper.Locator.InGameVm.YPosition);

        }
    }
}

