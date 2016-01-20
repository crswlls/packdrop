using System;
using System.Collections.Generic;
using System.Linq;
using SharedKernel;

namespace GameplayContext
{
    public class ScoreChecker
    {
        public int CheckScoreAndUpdate(List<List<Tile>> columns)
        {
            var score = 0;

            // Check vertically
            var numberVerticalItemsInSeries = 1;
            string lastItemChecked = null;
            var coordsToRemove = new List<Coordinate>();

            for (int i = 0; i < columns.Count;i++)
            {
                for (int j = 0; j < columns[i].Count;j++)
                {
                    if (columns[i][j].ImageId == lastItemChecked)
                    {
                        numberVerticalItemsInSeries++;
                    }
                    else
                    {
                        if (numberVerticalItemsInSeries > 2)
                        {
                            // We just finished finding a match
                            var columnToLookAt = j == 0 ? i - 1 : i;
                            var indexOfLastMatch = j == 0 ? columns[columnToLookAt].Count - 1 : j - 1;
                            for (int a = indexOfLastMatch; a > (indexOfLastMatch - numberVerticalItemsInSeries); a--)
                            {
                                coordsToRemove.Add(new Coordinate(columnToLookAt,a));
                            }

                            score += CalculateScore(numberVerticalItemsInSeries);
                        }

                        lastItemChecked = columns[i][j].ImageId;
                        numberVerticalItemsInSeries = 1;
                    }
                }
            }

            DoRemove(columns, coordsToRemove);

            return score;
        }

        private int CalculateScore (int numberHorizontalItemsInSeries)
        {
            return 100 + ((numberHorizontalItemsInSeries - 3) * 150);
        }

        private void DoRemove (List<List<Tile>> columns, List<Coordinate> coordsToRemove)
        {
            foreach (var coord in coordsToRemove.OrderByDescending(x => x.Y))
            {
                columns[coord.X].RemoveAt(coord.Y);
            }
        }

        private class Coordinate
        {
            public int X { get; private set;}
            public int Y { get; private set;}

            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}

