using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GameplayContext;
using GameplayContext.Ports;
using SharedKernel;
using ArtworkSelectionContext;
using System.Linq;

namespace ViewModels
{
    public class InGameViewModel : ViewModelBase
    {
        private const int GameSpeed = 500;

        private IGame _game;
        private INavigationService _navigationService;
        private IGameDimensions _gameDimensions;
        private IGameTimer _timer;
        private IArtworkRequester _requester;
        private int _dropStep;
        private IDispatcher _dispatcher;
        private RelayCommand _moveRightCommand;
        private RelayCommand _moveLeftCommand;
        private RelayCommand _dropCommand;
        private string _score;

        private List<ObservableCollection<Tile>> Columns = new List<ObservableCollection<Tile>>();

        private List<ObservableCollection<Tile>> _modelColumns = new List<ObservableCollection<Tile>>();

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

        public InGameViewModel(INavigationService navigation, IGame game, IGameDimensions gameDimensions, IGameTimer timer, IDispatcher dispatcher, IArtworkRequester requester)
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

            var modelColumn1 = new ObservableCollection<Tile>();
            var modelColumn2 = new ObservableCollection<Tile>();
            var modelColumn3 = new ObservableCollection<Tile>();
            var modelColumn4 = new ObservableCollection<Tile>();
            var modelColumn5 = new ObservableCollection<Tile>();
            var modelColumn6 = new ObservableCollection<Tile>();
            var modelColumn7 = new ObservableCollection<Tile>();
            var modelColumn8 = new ObservableCollection<Tile>();
            var modelColumn9 = new ObservableCollection<Tile>();

            Columns.Add(Column1);
            Columns.Add(Column2);
            Columns.Add(Column3);
            Columns.Add(Column4);
            Columns.Add(Column5);
            Columns.Add(Column6);
            Columns.Add(Column7);
            Columns.Add(Column8);
            Columns.Add(Column9);

            _modelColumns.Add(modelColumn1);
            _modelColumns.Add(modelColumn2);
            _modelColumns.Add(modelColumn3);
            _modelColumns.Add(modelColumn4);
            _modelColumns.Add(modelColumn5);
            _modelColumns.Add(modelColumn6);
            _modelColumns.Add(modelColumn7);
            _modelColumns.Add(modelColumn8);
            _modelColumns.Add(modelColumn9);

            _navigationService = navigation;
            _game = game;
            _gameDimensions = gameDimensions;
            _timer = timer;
            _dispatcher = dispatcher;
            _requester = requester;
            _dropStep = _gameDimensions.GameHeight / Game.NumberStepsToDrop;
        }

        public string Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                RaisePropertyChanged(nameof(Score));
            }
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

        public RelayCommand DropCommand
        {
            get
            {
                return _dropCommand ?? (_dropCommand = new RelayCommand(OnDropCommand));
            }
        }

        private void OnDropCommand()
        {
            _game.Drop();
        }

        public int FallingTileYPos
        {
            get
            {
                return _game.FallingTile.YPos * _dropStep;
            }
        }

        public int FallingTileXPos
        {
            get
            {
                return _game.FallingTile.XPos * TileSize;
            }
        }

        public string FallingTileImage {
            get
            {
                return _game.FallingTile.ImageId;
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
            _game.StartGame(_requester.Artwork, _modelColumns);
            _game.TileFell += OnTileFell;
            _game.TileStopped += OnTileStopped;
            _game.NewTile += OnNewTile;
            _game.GameOver += OnGameOver;
            _game.Scored += OnScored;
        }

        private void OnScored (object sender, ScoreEventArgs e)
        {
            foreach (var coord in e.Coords)
            {
                Columns[coord.X].RemoveAt(Columns[coord.X].Count - 1 - coord.Y);
            }
        }

        private void OnNewTile(object sender, TileEventArgs e)
        {
            RaisePropertyChanged(nameof(FallingTileImage));
            Score = _game.Score.ToString();
            _timer.UpdateInterval(GameSpeed - _game.SpeedLevel);
        }

        private void OnTileStopped(object sender, TileEventArgs e)
        {
            Columns[e.Tile.XPos].Insert(0, e.Tile);
        }

        private void OnGameTimerFired(object sender, EventArgs e)
        {
            _dispatcher.RunOnUiThread(_game.Continue);
        }

        private void OnTileFell(object sender, TileEventArgs e)
        {
            RaisePropertyChanged(nameof(FallingTileYPos));
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            _timer.Stop();
            _navigationService.NavigateTo(nameof(GameOverViewModel));
        }
    }
}

