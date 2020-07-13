using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M04_Game
{
    /// <summary>
    /// A class that realize a Lee algorithm
    /// </summary>
    static class WaveAlg
    {
        const int Wall = 99;
        const int Way = -1;
        const int PlayerAtMap = 0;
        const int Enemy = -2;
        const int Bonus = -3;

        static List<Point> wave = new List<Point>();
        /// <summary>
        /// Return the next turn for enemy
        /// </summary>
        /// <param name="startPositionFromGame">Current position of enemy</param>
        /// <param name="finishPositionFromGame">Current position of player</param>
        /// <param name="mapFromGame">Map from current game</param>
        /// <param name="width">Width of map</param>
        /// <param name="height">Height of map</param>
        /// <returns>The next turn for the enemy</returns>
        public static Point NextMove(Point startPositionFromGame, Point finishPositionFromGame, int[,] mapFromGame, int width, int height)
        {
            if (startPositionFromGame == finishPositionFromGame)
            {
                return new Point(finishPositionFromGame.GetX(), finishPositionFromGame.GetY());
            }

            Point startPosition = new Point(startPositionFromGame.GetX(), startPositionFromGame.GetY());
            Point finishPosition = new Point(finishPositionFromGame.GetX(), finishPositionFromGame.GetY());
            int[,] map = (int[,])mapFromGame.Clone();
            // The algorithm starts from finish position, realize the waves
            List<Point> oldWave = new List<Point>();
            oldWave.Add(new Point(finishPosition.GetX(), finishPosition.GetY()));
            int nextStep = 0;
            map[finishPosition.GetY(), finishPosition.GetX()] = nextStep;
            // These arrays are needed in order to walk through all neighboring cells
            int[] changeX = { 0, 1, 0, -1 };
            int[] changeY = { -1, 0, 1, 0 };

            while (oldWave.Count > 0)
            {
                nextStep++;
                wave.Clear();
                foreach (Point point in oldWave)
                {
                    for (int cell = 0; cell < 4; cell++)
                    {
                        finishPosition.SetX(point.GetX() + changeX[cell]);
                        finishPosition.SetY(point.GetY() + changeY[cell]);
                        if (map[finishPosition.GetY(), finishPosition.GetX()] == Way || map[finishPosition.GetY(), finishPosition.GetX()] == Enemy ||
                            map[finishPosition.GetY(), finishPosition.GetX()] == Bonus || map[finishPosition.GetY(), finishPosition.GetX()] == PlayerAtMap)
                        {
                            wave.Add(new Point(finishPosition.GetX(), finishPosition.GetY()));
                            map[finishPosition.GetY(), finishPosition.GetX()] = nextStep;
                        }
                    }
                }
                oldWave = new List<Point>(wave);
            }

            // The algoritm starts from start position, and return the next turn
            wave.Clear();
            for (int cell = 0; cell < 4; cell++)
            {
                finishPosition.SetX(startPosition.GetX() + changeX[cell]);
                finishPosition.SetY(startPosition.GetY() + changeY[cell]);
                if (map[startPosition.GetY(), startPosition.GetX()] - 1 == map[finishPosition.GetY(), finishPosition.GetX()])
                {
                    startPosition.SetX(finishPosition.GetX());
                    startPosition.SetY(finishPosition.GetY());
                    wave.Add(new Point(startPosition.GetX(), startPosition.GetY()));
                    break;
                }
            }
            // The enemy may be locked among the walls, need to check it
            if (wave.Any())
            {
                return wave[0];
            }
            else
            {
                return null;
            }
        }
    }
}
