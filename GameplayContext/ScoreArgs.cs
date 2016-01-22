using System;
using System.Collections.Generic;
using SharedKernel;
using System.Linq;
using System.Collections.ObjectModel;

namespace GameplayContext
{
	public class ScoreArgs : EventArgs
	{
        public List<Coordinate> Coords { get; private set; }

        public ScoreArgs(List<Coordinate> coords)
        {
            Coords = coords;
        }
	}

}

