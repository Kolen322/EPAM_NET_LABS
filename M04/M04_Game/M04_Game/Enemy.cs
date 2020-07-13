using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M04_Game
{
    /// <summary>
    /// Class that describes the enemy in game
    /// </summary>
    class Enemy
    {
        /// <summary>
        /// Current position of enemy at map
        /// </summary>
        public Point CurrentPosition { get; private set; }
        /// <summary>
        /// Initializes a new instance of the Player class that has a specified current position
        /// </summary>
        public Enemy(Point currentPosition)
        {
            CurrentPosition = currentPosition;
        }
        /// <summary>
        /// Set new position to CurrentPosition
        /// </summary>
        /// <param name="position"> New position</param>
        public void SetCurrentPosition(Point position)
        {
            CurrentPosition = position;
        }
    }
}
