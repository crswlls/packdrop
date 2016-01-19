using System;
using GameplayContext;
using ViewModels;

namespace IntegrationTests
{
    public class GameDimensionsStub : IGameDimensions
    {
        public int GameHeight
        {
            get
            {
                return Game.NumberStepsToDrop;
            }
        }

        public int GameWidth {
            get {
                return InGameViewModel.NumberColumns;
            }
        }
    }
}

