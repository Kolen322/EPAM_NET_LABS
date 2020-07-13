using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M04_Game
{
    /// <summary>
    /// Class that describes a player in game
    /// </summary>
    class Player
    {
        /// <summary>
        /// Current position of player at map
        /// </summary>
        public Point CurrentPosition { get; private set; }
        /// <summary>
        /// Current score of player in game
        /// </summary>
        public int Score { get; private set; }
        /// <summary>
        /// Initializes a new instance of the Player class that has a default current position(1,1) and score 0
        /// </summary>
        public Player() : this(new Point(1, 1), 0)
        {

        }
        /// <summary>
        /// Increment player's score
        /// </summary>
        /// <param name="count">How much to add to the score</param>
        public void IncrementScore(int count)
        {
            Score+=count;
        }
        /// <summary>
        /// Initializes a new instance of the Player class that has a specified current position and score
        /// </summary>
        /// <param name="currentPosition">Current position of player at map</param>
        /// <param name="score">Current score of player in game</param>
        public Player(Point currentPosition, int score)
        {
            CurrentPosition = currentPosition;
            Score = score;
        }
    }
}
