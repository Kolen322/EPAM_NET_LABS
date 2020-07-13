using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M04_Game
{
    /// <summary>
    /// A class of the console game
    /// </summary>
    class Game
    {
        const int Wall = 99;
        const int Way = -1;
        const int PlayerAtMap = 0;
        const int Enemy = -2;
        const int Bonus = -3;

        private Player _player { get; set; }
        private Enemy[] _enemies { get; set; }
        private bool _playerAlive { get; set; }

        /// <summary>
        /// The map of game
        /// </summary>
        public int[,] Map { get; private set; }
        /// <summary>
        /// Map widht
        /// </summary>
        public int MapWidth { get; private set; }
        /// <summary>
        /// Map Height
        /// </summary>
        public int MapHeight { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Game class that has a default map, which has 2 enemies, 4 bonuses
        /// </summary>
        public Game()
        {
            MapWidth = 16;
            MapHeight = 9;
            _playerAlive = true;
            _player = new Player();
            _enemies = new Enemy[] { new Enemy(new Point(1, 6)), new Enemy(new Point(13, 7)) };
            Map = new int[,]
            {
               {Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall},
               {Wall,PlayerAtMap,Way,Way,Bonus,Way,Way,Way,Way,Way,Way,Way,Way,Way,Bonus,Wall},
               {Wall,Way,Wall,Way,Wall,Wall,Wall,Way,Wall,Wall,Wall,Wall,Wall,Way,Wall,Wall},
               {Wall,Way,Wall,Way,Wall,Wall,Wall,Way,Wall,Wall,Way,Wall,Wall,Way,Wall,Wall},
               {Wall,Way,Wall,Way,Wall,Wall,Wall,Way,Wall,Wall,Way,Wall,Wall,Way,Wall,Wall},
               {Wall,Way,Way,Wall,Wall,Wall,Wall,Way,Wall,Wall,Way,Wall,Wall,Way,Wall,Wall},
               {Wall,Enemy,Way,Wall,Wall,Wall,Wall,Bonus,Way,Wall,Wall,Wall,Way,Way,Wall,Wall},
               {Wall,Way,Bonus,Way,Way,Way,Way,Way,Way,Way,Way,Way,Way,Enemy,Wall,Wall},
               {Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall,Wall}};
        }
        /// <summary>
        /// Start the game
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Rules: you need to collect all bonuses on the map, avoiding enemies and walls");
            Console.WriteLine("Designations:\nP - player\n# - wall\nE - enemy\nB - bonus");
            Console.WriteLine("Press any key to start game");
            Console.ReadKey();
            while (_playerAlive)
            {
                Console.Clear();
                DrawMap();
                ConsoleKeyInfo move = Console.ReadKey(true);
                switch (move.Key)
                {
                    case ConsoleKey.RightArrow:
                        Map[_player.CurrentPosition.GetY(), _player.CurrentPosition.GetX()] = Way;
                        _player.CurrentPosition.IncrementX();
                        break;
                    case ConsoleKey.UpArrow:
                        Map[_player.CurrentPosition.GetY(), _player.CurrentPosition.GetX()] = Way;
                        _player.CurrentPosition.DecrementY();
                        break;
                    case ConsoleKey.DownArrow:
                        Map[_player.CurrentPosition.GetY(), _player.CurrentPosition.GetX()] = Way;
                        _player.CurrentPosition.IncrementY();
                        break;
                    case ConsoleKey.LeftArrow:
                        Map[_player.CurrentPosition.GetY(), _player.CurrentPosition.GetX()] = Way;
                        _player.CurrentPosition.DecrementX();
                        break;
                    default: break;
                }
                if (Map[_player.CurrentPosition.GetY(), _player.CurrentPosition.GetX()] == Bonus)
                {
                    _player.IncrementScore(1);
                    if (_player.Score == 4)
                    {
                        Console.WriteLine("You win");
                        break;
                    }
                }
                else if (Map[_player.CurrentPosition.GetY(), _player.CurrentPosition.GetX()] == Enemy ||
                    Map[_player.CurrentPosition.GetY(), _player.CurrentPosition.GetX()] == Wall)
                {
                    Console.WriteLine($"GAMEOVER, your's score: {_player.Score}");
                    _playerAlive = false;
                    break;
                }
                // Change the map before the enemies begin to look for a player 
                Map[_player.CurrentPosition.GetY(), _player.CurrentPosition.GetX()] = PlayerAtMap;
                foreach (var enemy in _enemies)
                {
                    Point nextMove = WaveAlg.NextMove(enemy.CurrentPosition, _player.CurrentPosition, Map, MapWidth, MapHeight);
                    if (nextMove != null)
                    {
                        Map[enemy.CurrentPosition.GetY(), enemy.CurrentPosition.GetX()] = Way;
                        enemy.SetCurrentPosition(nextMove);
                        Map[enemy.CurrentPosition.GetY(), enemy.CurrentPosition.GetX()] = Enemy;
                        if (_player.CurrentPosition == enemy.CurrentPosition)
                        {
                            _playerAlive = false;
                            break;
                        }
                    }
                }
            }
        }

        private void DrawMap()
        {
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    switch (Map[y, x])
                    {
                        case Wall: Console.Write("#"); break;
                        case PlayerAtMap: Console.Write("P"); break;
                        case Way: Console.Write(" "); break;
                        case Enemy: Console.Write("E"); break;
                        case Bonus: Console.Write("B"); break;
                        default: break;
                    }
                }
                Console.WriteLine();
            }
        }

    }
}

