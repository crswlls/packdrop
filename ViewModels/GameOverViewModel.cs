using System;
using GalaSoft.MvvmLight;
using GameplayContext;

namespace ViewModels
{
    public class GameOverViewModel : ViewModelBase
    {
        private IGame _game;

        public GameOverViewModel(IGame game)
        {
            _game = game;
        }

        public string Score
        {
            get
            {
                return _game.Score.ToString();
            }
        }
    }
}

