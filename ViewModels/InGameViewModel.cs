using System;
using GalaSoft.MvvmLight;
using GameplayContext;
using SharedKernel;
using GalaSoft.MvvmLight.Views;
using GameplayContext.Ports;
using System.Collections.ObjectModel;

namespace ViewModels
{
    public class InGameViewModel : ViewModelBase
    {
        private const int GameSpeed = 500;

        private int _yPos = 0;
        private IGame _game;
        private INavigationService _navigationService;
        private IGameDimensions _gameDimensions;
        private IGameTimer _timer;
        private int _stepsToFall;
        private IDispatcher _dispatcher;

        public const int NumberColumns = 9;

        public ObservableCollection<Tile> Column0 { get; set; }

        public InGameViewModel(INavigationService navigation, IGame game, IGameDimensions gameDimensions, IGameTimer timer, IDispatcher dispatcher)
        {
            Column0 = new ObservableCollection<Tile>();
            _navigationService = navigation;
            _game = game;
            _gameDimensions = gameDimensions;
            _timer = timer;
            _dispatcher = dispatcher;
            _stepsToFall = _gameDimensions.GameHeight / GameplayContext.Game.NumberStepsToDrop;
        }

        public IGame Game
        {
            get
            {
                return _game;
            }
        }

        public int FallingTileYPos
        {
            get
            {
                return _game.FallingTile.YPos * _stepsToFall;
            }
        }

        public int TileSize
        {
            get
            {
                return _gameDimensions.GameWidth / NumberColumns;
            }
        }

        public void Initialise()
        {
            _timer.Tick += OnGameTimerFired;
            _timer.Start(GameSpeed);
            _game.StartGame();
            //// _game.NewTile += OnNewTileCreated;
            _game.TileFell += OnTileFell;
            _game.TileStopped += OnTileStopped;
            _game.GameOver += OnGameOver;
        }

        private void OnTileStopped(object sender, TileEventArgs e)
        {
            _dispatcher.RunOnUiThread(() => Column0.Add(e.Tile));
        }

        /*private void OnNewTileCreated(object sender, TileEventArgs e)
        {
            RaisePropertyChanged(nameof(Column0));
        }*/

        private void OnGameTimerFired(object sender, EventArgs e)
        {
            _game.Continue();
        }

        private void OnTileFell(object sender, TileEventArgs e)
        {
            RaisePropertyChanged(nameof(Game));
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            _timer.Stop();
            _navigationService.NavigateTo(nameof(GameOverViewModel));
        }
    }
}

