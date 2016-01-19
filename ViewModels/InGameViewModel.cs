using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GameplayContext;
using GameplayContext.Ports;
using SharedKernel;

namespace ViewModels
{
    public class InGameViewModel : ViewModelBase
    {
        private const int GameSpeed = 500;

        private IGame _game;
        private INavigationService _navigationService;
        private IGameDimensions _gameDimensions;
        private IGameTimer _timer;
        private int _itemHeight;
        private IDispatcher _dispatcher;
        private RelayCommand _moveRightCommand;
        private RelayCommand _moveLeftCommand;

        private List<ObservableCollection<Tile>> Columns = new List<ObservableCollection<Tile>>();

        public const int NumberColumns = 9;

        public ObservableCollection<Tile> Column1 { get; set; }
        public ObservableCollection<Tile> Column2 { get; set; }
        public ObservableCollection<Tile> Column3 { get; set; }
        public ObservableCollection<Tile> Column4 { get; set; }
        public ObservableCollection<Tile> Column5 { get; set; }
        public ObservableCollection<Tile> Column6 { get; set; }
        public ObservableCollection<Tile> Column7 { get; set; }
        public ObservableCollection<Tile> Column8 { get; set; }
        public ObservableCollection<Tile> Column9 { get; set; }

        public InGameViewModel(INavigationService navigation, IGame game, IGameDimensions gameDimensions, IGameTimer timer, IDispatcher dispatcher)
        {
            Column1 = new ObservableCollection<Tile>();
            Column2 = new ObservableCollection<Tile>();
            Column3 = new ObservableCollection<Tile>();
            Column4 = new ObservableCollection<Tile>();
            Column5 = new ObservableCollection<Tile>();
            Column6 = new ObservableCollection<Tile>();
            Column7 = new ObservableCollection<Tile>();
            Column8 = new ObservableCollection<Tile>();
            Column9 = new ObservableCollection<Tile>();

            Columns.Add(Column1);
            Columns.Add(Column2);
            Columns.Add(Column3);
            Columns.Add(Column4);
            Columns.Add(Column5);
            Columns.Add(Column6);
            Columns.Add(Column7);
            Columns.Add(Column8);
            Columns.Add(Column9);

            _navigationService = navigation;
            _game = game;
            _gameDimensions = gameDimensions;
            _timer = timer;
            _dispatcher = dispatcher;
            _itemHeight = _gameDimensions.GameHeight / GameplayContext.Game.NumberStepsToDrop;
        }

        public RelayCommand MoveLeftCommand
        {
            get
            {
                return _moveLeftCommand ?? (_moveLeftCommand = new RelayCommand(OnMoveLeftCommand));
            }
        }

        private void OnMoveLeftCommand()
        {
            _game.MoveLeft();
        }

        public RelayCommand MoveRightCommand
        {
            get
            {
                return _moveRightCommand ?? (_moveRightCommand = new RelayCommand(OnMoveRightCommand));
            }
        }

        private void OnMoveRightCommand()
        {
            _game.MoveRight();
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
                return _game.FallingTile.YPos * _itemHeight;
            }
        }

        public int FallingTileXPos
        {
            get
            {
                return _game.FallingTile.XPos * _itemHeight;
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
            _game.StartGame(NumberColumns);
            //// _game.NewTile += OnNewTileCreated;
            _game.TileFell += OnTileFell;
            /*_game.TileMoved += OnTileMoved;*/
            _game.TileStopped += OnTileStopped;
            _game.GameOver += OnGameOver;
        }

        private void OnTileStopped(object sender, TileEventArgs e)
        {
            _dispatcher.RunOnUiThread(() => 
            {
                Columns[e.Tile.XPos].Add(e.Tile);
            });
        }

        /*private void OnTileMoved(object sender, TileEventArgs e)
        {
            FallingTileXPos = e.Tile.XPos * _itemHeight;
        }*/

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

