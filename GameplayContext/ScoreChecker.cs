﻿using System;
using System.Collections.Generic;
using System.Linq;
using SharedKernel;
using System.Collections.ObjectModel;

namespace GameplayContext
{
    public class ScoreChecker
    {
        public int CheckScoreAndUpdate(List<ObservableCollection<Tile>> columns)
        {
            var score = 0;

            // Check vertically
            var numberVerticalItemsInSeries = 1;
            string lastItemChecked = null;
            var coordsToRemove = new List<Coordinate>();
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
                        score += CheckForMatch(i, j, columns, numberVerticalItemsInSeries, coordsToRemove, lastPopulatedColumn);
                        lastItemChecked = columns[i][j].ImageId;
                        numberVerticalItemsInSeries = 1;
                    }

                    lastPopulatedColumn = i;
                }
            }

            // Check whether we finished on a match
            if (lastPopulatedColumn >= 0)
            {
                score += CheckForMatch(lastPopulatedColumn, columns[lastPopulatedColumn].Count - 1, columns, numberVerticalItemsInSeries, coordsToRemove, lastPopulatedColumn);
            }

            DoRemove(columns, coordsToRemove);

            return score;
        }

        private int CheckForMatch(int xIndex, int yIndex, List<ObservableCollection<Tile>> columns, int numberVerticalItemsInSeries, List<Coordinate> coordsToRemove, int lastPopulatedColumn)
        {
            if (numberVerticalItemsInSeries > 2) {
                // We just finished finding a match
                AddCoordsToRemove (xIndex, yIndex, columns, numberVerticalItemsInSeries, coordsToRemove, lastPopulatedColumn);
                return CalculateScore (numberVerticalItemsInSeries);
            }

            return 0;
        }

        private void AddCoordsToRemove(int xIndex, int yIndex, List<ObservableCollection<Tile>> columns, int numberVerticalItemsInSeries, List<Coordinate> coordsToRemove, int lastPopulatedColumn)
        {
            var matchFinishedOnPreviousColumn = yIndex == 0;
            var columnToLookAt = matchFinishedOnPreviousColumn ? lastPopulatedColumn : xIndex;
            var xIndexOfLastMatch = matchFinishedOnPreviousColumn ? columns[columnToLookAt].Count - 1 : yIndex - 1;
            for (int a = xIndexOfLastMatch; a >= 0 && a > (xIndexOfLastMatch - numberVerticalItemsInSeries); a--)
            {
                coordsToRemove.Add(new Coordinate(columnToLookAt, a));
            }
        }


        private int CalculateScore (int numberHorizontalItemsInSeries)
        {
            return 100 + ((numberHorizontalItemsInSeries - 3) * 150);
        }

        private void DoRemove (List<ObservableCollection<Tile>> columns, List<Coordinate> coordsToRemove)
        {
            foreach (var coord in coordsToRemove.OrderByDescending(x => x.Y))
            {
                columns[coord.X].RemoveAt(coord.Y);
            }
        }
    }
}

