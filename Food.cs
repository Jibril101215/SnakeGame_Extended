using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Food : AnimatedObjects 
    {
        private int lastFoodTime; // last time food was eaten
        private const int foodDissapearTime = 10000; // food time
        private FoodType foodTypes;
        private int foodPoint;

        public Food()
        {
            lastFoodTime = Environment.TickCount;
            color = ConsoleColor.Yellow;
            foodTypes = FoodType.bigfood;
            foodPoint = 50;
        }

        public Food(Position _position,
            string _symbol = "@",
            ConsoleColor _color = ConsoleColor.Yellow) : base(_position, _symbol, _color) { }

        public int LastFoodTime
        {
            get { return lastFoodTime; }
        }

        public int DisappearTime
        {
            get { return foodDissapearTime; }
        }


        /// <summary>
        /// Position food randomly
        /// </summary>
        /// <param name="snake"></param>
        /// <param name="ObstacleList"></param>
        /// <param name="rand"></param>
        public void UpdateFoodPosition(Snake snake, ObstacleList ObstacleList, Random rand)
        {
            Random r = new Random();
            ChangeFoodType((FoodType)r.Next(0,3));
            Console.SetCursorPosition(Pos.col, Pos.row);
            Console.WriteLine("  ");
            do
            {
                this.Pos = new Position(rand.Next(1, Console.WindowHeight),
                    rand.Next(0, Console.WindowWidth));
            }
            while (snake.SnakeElements.Contains(this.Pos) || ObstacleList.Position.Contains(this.Pos));

            lastFoodTime = Environment.TickCount;
        }
        
        public int GetFoodPoints
        {
            get{ return foodPoint; }
            set{ foodPoint = value; }
        }

        public void ChangeFoodType(FoodType food)
        {
            if (food==FoodType.smallfood)
            {
                symbol = "\u2663\u2663";
                foodPoint = 10;
            }
            else if (food==FoodType.mediumfood)
            {
                symbol = "\u2666\u2666";
                foodPoint = 20;
            }
            else
            {
                symbol = "\u2665\u2665";
                foodPoint = 50;
            }
        }
        
    }

    
}
