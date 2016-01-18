using System;
using GalaSoft.MvvmLight;
using GameplayContext;

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

        public int YPosition
        {
            get
            {
                return _yPos;
            }
            set
            {
                if (Set(() => YPosition, ref _yPos, value))
                {
                    RaisePropertyChanged(() => YPosition);
                }
            }
        }

        public void Initialise()
        {
            _game.GameChanged += OnGameChanged;
            _game.StartGame();
        }

        private void OnGameChanged(object sender, EventArgs e)
        {
            YPosition = _game.GetColumn(0)[0].YPos;
        }
    }
}

