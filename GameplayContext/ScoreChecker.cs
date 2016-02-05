using System;
using System.Collections.Generic;
using System.Linq;
using SharedKernel;
using System.Collections.ObjectModel;

namespace GameplayContext
{
    public class ScoreChecker
    {
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
            var score = 0;
            var numberVerticalItemsInSeries = 1;
            string lastItemChecked = null;

            var lastPopulatedColumn = -1;

            for (int i = 0; i < columns.Count;i++)
            {
                for (int j = 0; j < columns[i].Count;j++)
                {
                    if (j != 0 && columns[i][j].ImageId == lastItemChecked)
                    {
                        numberVerticalItemsInSeries++;
                    }
                    else
                    {
                        score += CheckForMatch(i, j, columns, numberVerticalItemsInSeries, lastPopulatedColumn, true);
                        lastItemChecked = columns[i][j].ImageId;
                        numberVerticalItemsInSeries = 1;
                    }

                    lastPopulatedColumn = i;
                }
            }

            // Check whether we finished on a match
            if (lastPopulatedColumn >= 0)
            {
                score += CheckForMatch(lastPopulatedColumn, columns[lastPopulatedColumn].Count, columns, numberVerticalItemsInSeries, lastPopulatedColumn, true);
            }

            return score;
        }

        private int CalculateScore (int numberHorizontalItemsInSeries)
        {
            return 100 + ((numberHorizontalItemsInSeries - 3) * 150);
        }

        private int CheckHorizontally(List<ObservableCollection<Tile>> columns)
        {
            var score = 0;
            var numberHorizontalItemsInSeries = 1;
            string lastItemChecked = null;

            var lastPopulatedRow = -1;
            var highestNumberTiles = columns.Max(x => x.Count);
            for (int i = 0; i < highestNumberTiles;i++)
            {
                for (int j = 0; j < columns.Count;j++)
                {
                    if (i >= columns[j].Count)
                    {
                        // Column not populated
                        score += CheckForMatch(j, i, columns, numberHorizontalItemsInSeries, lastPopulatedRow, false);
                        lastItemChecked = null;
                        numberHorizontalItemsInSeries = 1;
                    }
                    else if (j != 0 && columns[j][i].ImageId == lastItemChecked)
                    {
                        // Column part of a series
                        numberHorizontalItemsInSeries++;
                        lastPopulatedRow = i;
                    }
                    else
                    {
                        // New series starting
                        score += CheckForMatch(j, i, columns, numberHorizontalItemsInSeries, lastPopulatedRow, false);
                        lastItemChecked = columns[j][i].ImageId;
                        numberHorizontalItemsInSeries = 1;
                        lastPopulatedRow = i;
                    }
                }
            }

            // Check whether we finished on a match
            if (lastPopulatedRow >= 0)
            {
                score += CheckForMatch(columns.Count, lastPopulatedRow, columns, numberHorizontalItemsInSeries, lastPopulatedRow, false);
            }

            return score;
        }

        private int CheckForMatch(int xIndex, int yIndex, List<ObservableCollection<Tile>> columns, int numberVerticalItemsInSeries, int lastPopulatedColumn, bool isVertical)
        {
            if (numberVerticalItemsInSeries > 2) {
                // We just finished finding a match
                AddCoordsToRemove (xIndex, yIndex, columns, numberVerticalItemsInSeries, lastPopulatedColumn, isVertical);
                return CalculateScore (numberVerticalItemsInSeries);
            }

            return 0;
        }

        private void AddCoordsToRemoveVertical (int xIndex, int rowIndex, List<ObservableCollection<Tile>> columns, int numberVerticalItemsInSeries, int lastPopulatedRow)
        {
            var matchFinishedOnPreviousColumn = rowIndex == 0;
            var columnToLookAt = matchFinishedOnPreviousColumn ? lastPopulatedRow : xIndex;
            var xIndexOfLastMatch = matchFinishedOnPreviousColumn ? columns [columnToLookAt].Count - 1 : rowIndex - 1;
            for (int a = xIndexOfLastMatch; a >= 0 && a > (xIndexOfLastMatch - numberVerticalItemsInSeries); a--) {
                _coordsToRemove.Add (new Coordinate (columnToLookAt, a));
            }
        }

        private void AddCoordsToRemoveHorizontal(int xIndex, int rowIndex, List<ObservableCollection<Tile>> columns, int numberVerticalItemsInSeries, int lastPopulatedRow)
        {
            var matchFinishedOnPreviousRow = xIndex == 0;
            var rowToLookAt = matchFinishedOnPreviousRow ? lastPopulatedRow : rowIndex;
            var xIndexOfLastMatch = xIndex - 1;
            if (matchFinishedOnPreviousRow) 
            {
                // work out first column that is populated
                for (int i = columns.Count - 1; i >= 0; i--)
                {
                    if (columns [i].Count > rowToLookAt)
                    {
                        xIndexOfLastMatch = i;
                        break;
                    }
                }
            }

            for (int a = xIndexOfLastMatch; a >= 0 && a > (xIndexOfLastMatch - numberVerticalItemsInSeries); a--)
            {
                _coordsToRemove.Add (new Coordinate (a, rowToLookAt));
            }
        }

        private void AddCoordsToRemove(int xIndex, int rowIndex, List<ObservableCollection<Tile>> columns, int numberVerticalItemsInSeries, int lastPopulatedRow, bool isVertical)
        {
            if (isVertical)
            {
                AddCoordsToRemoveVertical(xIndex, rowIndex, columns, numberVerticalItemsInSeries, lastPopulatedRow);
            }
            else
            {
                AddCoordsToRemoveHorizontal(xIndex, rowIndex, columns, numberVerticalItemsInSeries, lastPopulatedRow);
            }
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

