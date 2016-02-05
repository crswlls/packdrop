using System;
using System.Collections.Generic;

namespace GameplayContext
{
	public class ScoreEventArgs : EventArgs
	{
        public List<Coordinate> Coords { get; private set; }

        public ScoreEventArgs(List<Coordinate> coords)
        {
            Coords = coords;
        }
	}

}