﻿using System;
using ViewModels;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GameplayContext;
using GameplayContext.Ports;

namespace IntegrationTests
{
    public static class SetupHelper
    {
        private static ViewModelLocator _locator;
        private static GameTimerStub _gameTimer = new GameTimerStub();

        public static GameTimerStub GameTimer
        {
            get
            {
                return _gameTimer;
            }
        }

        public static ViewModelLocator Locator
        {
            get
            {
                return _locator;
            }
        }

        public static void InitialiseApp()
        {
            SimpleIoc.Default.Reset();
            if (_locator != null) {
                ViewModelLocator.Reset();
            }

            var nav = new NavigationServiceStub ();

            SimpleIoc.Default.Register<INavigationService> (() => nav);
            SimpleIoc.Default.Register<IGame, Game> ();
            SimpleIoc.Default.Register<IGameTimer> (() => _gameTimer);
            SimpleIoc.Default.Register<IGameDimensions, GameDimensionsStub> ();
            _locator = new ViewModelLocator ();
        }
    }
}

