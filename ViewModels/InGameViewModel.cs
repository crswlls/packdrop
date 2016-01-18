using System;
using GalaSoft.MvvmLight;

namespace ViewModels
{
    public class InGameViewModel : ViewModelBase
    {
        private int _yPos = 20;

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
            YPosition = 200;
        }
    }
}

