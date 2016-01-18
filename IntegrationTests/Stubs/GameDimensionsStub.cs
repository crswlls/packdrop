using System;
using GameplayContext;

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
    }
}

