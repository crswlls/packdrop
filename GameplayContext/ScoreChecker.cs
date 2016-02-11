using System;
using System.Collections.Generic;
using System.Linq;
using SharedKernel;
using System.Collections.ObjectModel;

namespace GameplayContext
{
    public class ScoreChecker
    {
        private const int MinNumberForMatch = 3;
        private readonly List<Coordinate> _coordsToRemove = new List<Coordinate>();

        public List<Coordinate> RemovedItems 
        {
            get
            {
               return _coordsToRemove;
            }
        }

        public int CheckScoreAndUpdate(List<ObservableCollection<Tile>> columns)
        {
            var score = DoCheck(columns, CheckVertically);
            if (score == 0)
            {
                score = DoCheck(columns, CheckHorizontally);
            }

            return score;
        }

        private int DoCheck(List<ObservableCollection<Tile>> columns, Func<List<ObservableCollection<Tile>>, int> checker)
        {
            _coordsToRemove.Clear();
            var score = checker(columns);
            DoRemove(columns);
            return score;
        }

        private int CheckVertically(List<ObservableCollection<Tile>> columns)
        {
            var numberVerticalItemsInSeries = 1;
            string lastItemChecked = null;

            for (int i = 0; i < columns.Count;i++)
            {
                numberVerticalItemsInSeries = 1;
                for (int j = 0; j < columns[i].Count;j++)
                {
                    if (j != 0 && columns[i][j].ImageId == lastItemChecked)
                    {
                        numberVerticalItemsInSeries++;
                        if (numberVerticalItemsInSeries >= MinNumberForMatch && (columns[i].Count == j+1 || columns[i][j+1].ImageId != lastItemChecked))
                        {
                            // We just finished finding a match
                            for (int k = j; k >= 0 && k > (j - numberVerticalItemsInSeries); k--)
                            {
                                _coordsToRemove.Add (new Coordinate (i, k));
                            }

                            return CalculateScore (numberVerticalItemsInSeries);
                        }
                    }
                    else
                    {
                        numberVerticalItemsInSeries = 1;
                    }

                    lastItemChecked = columns[i][j].ImageId;
                }
            }

            return 0;
        }

        private int CheckHorizontally(List<ObservableCollection<Tile>> columns)
        {
            var numberHorizontalItemsInSeries = 1;
            string lastItemChecked = null;
            var highestNumberTiles = columns.Max(x => x.Count);
            for (int i = 0; i < highestNumberTiles;i++)
            {
                numberHorizontalItemsInSeries = 1;
                for (int j = 0; j < columns.Count;j++)
                {
                    if (i >= columns[j].Count)
                    {
                        lastItemChecked = null;
                        continue;
                    }
                    else if (columns[j][i].ImageId == lastItemChecked)
                    {
                        numberHorizontalItemsInSeries++;
                        if (numberHorizontalItemsInSeries >= MinNumberForMatch && ((j + 1 >= columns.Count || i >= columns[j + 1].Count) || columns[j + 1][i].ImageId != lastItemChecked))
                        {
                            // Finished finding a match
                            for (int a = j; a >= 0 && a > (j - numberHorizontalItemsInSeries); a--)
                            {
                                _coordsToRemove.Add (new Coordinate (a, i));
                            }

                            return CalculateScore (numberHorizontalItemsInSeries);
                        }
                    }

                    lastItemChecked = columns[j][i].ImageId;
                }
            }

            return 0;
        }

        private int CalculateScore (int numberHorizontalItemsInSeries)
        {
            return 100 + ((numberHorizontalItemsInSeries - 3) * 150);
        }

        private void DoRemove (List<ObservableCollection<Tile>> columns)
        {
            foreach (var coord in _coordsToRemove.OrderByDescending(x => x.Y))
            {
                columns[coord.X].RemoveAt(coord.Y);
            }
        }
    }
}