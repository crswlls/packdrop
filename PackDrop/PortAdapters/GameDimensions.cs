using System;
using GameplayContext;

namespace PackDrop
{
    public class GameDimensions : IGameDimensions
    {
        public int GameHeight { get { return 600; } }

        public int GameWidth {
            get {
                return 300;
            }
        }
    }
}

