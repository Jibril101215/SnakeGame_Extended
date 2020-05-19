using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class ObstacleList
    {
        private List<Obstacle> _obstacles; // Stores obstacles
        private List<Position> _positions; // Stores obstacles' position value

        public ObstacleList()
        {
            _obstacles = new List<Obstacle>();
            _positions = new List<Position>();
        }

        public List<Obstacle> Obstacles { get { return _obstacles; } }
        public List<Position> Position { get { return _positions; } }

        public void AddObstacle(Obstacle obs)
        {
            _obstacles.Add(obs);
            _positions.Add(obs.Pos);
        }

        /// <summary>
        /// Position new obstacle randomly
        /// </summary>
        /// <param name="snake"></param>
        /// <param name="food"></param>
        /// <param name="rand"></param>
        public void PositionNewObstacle(Snake snake, Food food, Random rand)
        {
            Obstacle newObstacle = new Obstacle();

            do
            {
                newObstacle.Pos = new Position(rand.Next(2, Console.WindowHeight-3),
                    rand.Next(2, Console.WindowWidth-3));

            }
            while (snake.SnakeElements.Contains(newObstacle.Pos) ||
                        _positions.Contains(newObstacle.Pos) ||
                        food.Pos == newObstacle.Pos);

            this.AddObstacle(newObstacle);
            newObstacle.Display();
        }
    }
}
