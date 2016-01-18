using System;
using GalaSoft.MvvmLight;
using GameplayContext;
using SharedKernel;

namespace ViewModels
{
    public class InGameViewModel : ViewModelBase
    {
        private int _yPos = 0;
        private IGame _game;

        public InGameViewModel(IGame game)
        {
            _game = game;
        }

        public IGame Game
        {
            get
            {
                return _game;
            }
        }

        public Tile FallingTile
        {
            get
            {
                return _game.FallingTile;
            }
        }

        public void Initialise()
        {
            _game.GameChanged += OnGameChanged;
            _game.StartGame();
        }

        private void OnGameChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged (nameof(Game));
        }
    }
}

