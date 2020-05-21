using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Snake
{
    public class Snake : AnimatedObjects
    {
        /// <summary>
        /// Snake size or length
        /// </summary>
        private int size;

        /// <summary>
        /// A list containing elements of the snake
        /// </summary>
        private Queue<Position> snakeElements;

        /// <summary>
        /// Snake's speed
        /// The lower the value, the faster it becomes
        /// </summary>
        private double sleepTime;
        
        /// <summary>
        /// Number of lives the snake has
        /// </summary>
        private int snakeLives = 4;
        
        public Snake() 
        {
            sleepTime = 100;
            symbol = "O";
            color = ConsoleColor.DarkGray;
            size = 4;
            snakeElements = new Queue<Position>();
            for (int i = 0; i < size; i++)
            {
                snakeElements.Enqueue(new Position(2, i+1));
                Display();
            }

        }

        /// <summary>
        /// Returns the list of snake elements.
        /// </summary>
        public Queue<Position> SnakeElements
        {
            get { return snakeElements; }
        }

        /// <summary>
        /// Add new element to the snake.
        /// </summary>
        /// <param name="pos"></param>
        public void AddElement(Position pos)
        {
            snakeElements.Enqueue(pos);
        }

        public int CountElements(){
            return snakeElements.Count();
        }

        public double SleepTime
        {
            get { return sleepTime; }
            set { sleepTime = value; }
        }

        /// <summary>
        /// updates the position of the snake
        /// </summary>
        /// <param name="pos"></param>
        public Position UpdatePosition(Position pos)
        {
            Position snakeHead = snakeElements.Last();
            snakeHead.row += pos.row;
            snakeHead.col += pos.col;

            // reposition the snake when it goes beyond the screen
            if (snakeHead.col < 1) snakeHead.col = Console.WindowWidth - 3;   //going beyond left, appear on the right side
            if (snakeHead.row < 2) snakeHead.row = Console.WindowHeight - 3; //going up, appear from the bottom
            if (snakeHead.row >= Console.WindowHeight-2) snakeHead.row = 3;   //going down, appear from the top 
            if (snakeHead.col >= Console.WindowWidth-2) snakeHead.col = 2;   //going beyond right, appear from the left

            return snakeHead;
        }

        public int getSnakeLives() {
            return snakeLives;
        }

        public override void Display()
        {
            Position snakeHead = snakeElements.Last();
            Console.ForegroundColor = color;
            Console.SetCursorPosition(snakeHead.col, snakeHead.row);
            Console.Write(symbol);
        }



    }
}
