﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Media;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Snake
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Random randomNumbersGenerator = new Random(); // RANDOM NUMBER
            Console.BufferHeight = Console.WindowHeight;

            // USER MANAGEMENT SYSTEM
            Console.WriteLine("Enter username: ");
            User currUser = new User(Console.ReadLine());
            Console.Clear();

            // BEFORE INSTANTIATING GAME OBJECTS
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Hello, "+currUser.name+"! Press ENTER to start");
            Console.ReadLine();
            Console.Clear();

            // INITIALISE & DISPLAY 5 OBSTACLES
            // added to ObstacleList list
            ObstacleList ObstacleList = new ObstacleList();
            for(int i = 0; i < 5; i++) 
            {
                ObstacleList.AddObstacle(new Obstacle(new Position(randomNumbersGenerator.Next(5, Console.WindowHeight), randomNumbersGenerator.Next(5, Console.WindowWidth))));
            }
            foreach (Obstacle ob in ObstacleList.Obstacles) { ob.Display(); } // Dislay Obstacles


            // INITIALISE SNAKE ELEMENTS
            Snake snake = new Snake();

            // INITIALISE SNAKE DIRECTION
            Directions direction = new Directions(Arrow.right);

            // INITIALISE FOOD POSITION
            Food food = new Food(); ;
            food.UpdateFoodPosition(snake, ObstacleList, randomNumbersGenerator);
            food.Display();


            // INITIALISE USER POINTS
            int userPoints = 0;

            // INITIALISE SOUNDS
            string path = Path.Combine(Directory.GetCurrentDirectory(), "repeat.wav");
            SoundPlayer sound = new SoundPlayer(path);
            sound.PlayLooping();

            //INITIALISE AND DISPLAY SNAKE LIVES
            int lives = snake.getSnakeLives();
            string ss1 = "Remaining lives : {0}";
            Console.SetCursorPosition((Console.WindowWidth - ss1.Length), 0);
            Console.WriteLine(ss1, lives);

            // PROGAM STARTS HERE
            while (userPoints<500)
            {
                Console.SetCursorPosition((Console.WindowWidth - ss1.Length), 0);
                Console.WriteLine(ss1, lives);
                // Update Snake's current direction when a key is pressed
                if (Console.KeyAvailable) direction.ChangeDirection();
                
                // Update Snake's position
                Position snakeNewHead = snake.UpdatePosition(direction.CurrentDirection);

                //  GAME OVER 
                if (snake.SnakeElements.Contains(snakeNewHead) || ObstacleList.Position.Contains(snakeNewHead))
                {
                    if (lives == 1)
                    {
                        break;
                    }
                    else
                    {
                        lives--;
                    }
                }
                    


                snake.Display();

                // Update Snake's Head
                snake.AddElement(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.ForegroundColor = ConsoleColor.Gray;
                if (direction.Arrow == Arrow.right) Console.Write(">");
                if (direction.Arrow == Arrow.left) Console.Write("<");
                if (direction.Arrow == Arrow.up) Console.Write("^");
                if (direction.Arrow== Arrow.down) Console.Write("v");

                
                // WHEN FOOD IS EATEN
                if (snakeNewHead == food.Pos)
                {
                    // Reposition Food after eaten
                  
                    food.UpdateFoodPosition(snake, ObstacleList, randomNumbersGenerator);
                    userPoints += food.GetFoodPoints;

                    // Randomly place new obstacle
                    ObstacleList.PositionNewObstacle(snake, food, randomNumbersGenerator);
                    
                }


                // WHEN FOOD IS NOT EATEN
                else
                {
                    // moving...
                    Position last = snake.SnakeElements.Dequeue();
                    Console.SetCursorPosition(last.col, last.row);
                    Console.Write(" ");

                    // REPOSITION FOOD IF NOT EATEN
                    if (Environment.TickCount - food.LastFoodTime >= food.DisappearTime)
                    {
                        //negativePoints = 50;
                        Console.SetCursorPosition(food.Col, food.Row);
                        Console.Write(" ");
                        food.UpdateFoodPosition(snake, ObstacleList, randomNumbersGenerator);
                        userPoints -= 50;
                    }
                }
                Console.SetCursorPosition(Console.WindowWidth / 2, 0);
                Console.WriteLine("Food disappear in:   ");
                Console.SetCursorPosition(Console.WindowWidth / 2, 0);
                Console.WriteLine("Food disappear in: " + (10-((Environment.TickCount - food.LastFoodTime)/1000)));
                food.Display();

                //SPEED UP SNAKE AFTER POINTS REACHED 200
                if (userPoints > 200)
                {
                    snake.SleepTime -= 0.05; // Increase Snake's speed
                }



                
                snake.SleepTime -= 0.01; // Increase Snake's speed
                
                Thread.Sleep((int)snake.SleepTime); // Update Program's speed 
                userPoints = Math.Max(userPoints, 0);
                Console.SetCursorPosition(0, 0);

                Console.WriteLine("Your points are:    ");

                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Your points are: {0}", userPoints);

            }

            // STORES PLAYER'S DATA IN "UserData.txt"
            currUser.score = userPoints;
            Scoreboard scoreboard = new Scoreboard();
            scoreboard.updateScoreboard(currUser);
            scoreboard.updateFile();
            scoreboard.DisplayScoreboard();

            if (userPoints >= 500)
            {
                string path3 = Path.Combine(Directory.GetCurrentDirectory(), "yay.wav");
                SoundPlayer sound3 = new SoundPlayer(path3);
                sound3.Play();

                string s3 = "You won, "+currUser.name+"! Your score is {0} \n";
                Console.SetCursorPosition((Console.WindowWidth - s3.Length) / 2, 0);

                Console.Write(s3, userPoints);
                Console.ReadLine();
            } else
            {
               
                    string s1 = "Game over, " + currUser.name + "!";
                    string s2 = "Your points are: {0}";
                    Console.SetCursorPosition((Console.WindowWidth - s1.Length) / 2, (Console.WindowHeight - 2) / 2);
                    Console.ForegroundColor = ConsoleColor.Red;



                    string path2 = Path.Combine(Directory.GetCurrentDirectory(), "aww.wav");
                    SoundPlayer sound2 = new SoundPlayer(path2);
                    sound2.Play();

                    Console.WriteLine(s1);


                    userPoints = Math.Max(userPoints, 0);
                    Console.SetCursorPosition((Console.WindowWidth - s2.Length) / 2, ((Console.WindowHeight) / 2));
                    Console.WriteLine(s2, userPoints);
                    Console.SetCursorPosition((Console.WindowWidth - s2.Length) / 2, ((Console.WindowHeight + 2) / 2));
                    Console.WriteLine("Press Enter to exit game");
                    Console.ReadLine();
               
                
                 
                    
                    
                
            }
        }

       
    }
}
