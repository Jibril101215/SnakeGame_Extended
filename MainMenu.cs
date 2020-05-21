using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class MainMenu
    {
        private string command = "________";
        private static Position commandPos = new Position((Console.WindowHeight / 2) + 1, (Console.WindowWidth / 2));

        public MainMenu()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.SetCursorPosition(i, 2);
                Console.Write("\u2592");
                Console.SetCursorPosition(i, Console.WindowHeight - 1);
                Console.Write("\u2592");

            }


            for (int j = 1; j < Console.WindowHeight - 1; j++)
            {
                Console.SetCursorPosition(0, j);
                Console.Write("\u2592");
                Console.SetCursorPosition(Console.WindowWidth - 1, j);
                Console.Write("\u2592");
            }

            Console.SetCursorPosition((Console.WindowWidth / 2), (Console.WindowHeight / 2) - 5);
            Console.Write("Snake Game");
            Console.SetCursorPosition((Console.WindowWidth / 2), (Console.WindowHeight / 2));
            Console.Write("Play");
            Console.SetCursorPosition((Console.WindowWidth / 2), (Console.WindowHeight / 2) + 5);
            Console.Write("Exit");
            Console.SetCursorPosition(commandPos.col, commandPos.row);
            Console.Write(command);
        }


        public string Choice()
        {
            Console.SetCursorPosition(commandPos.col, commandPos.row);
            Console.Write("                             ");
            ConsoleKeyInfo userInput = Console.ReadKey(true);
            if (userInput.Key == ConsoleKey.UpArrow)
            {
                if (commandPos.col == (Console.WindowHeight / 2) + 1) commandPos.col -= 2;
                else commandPos.row = (Console.WindowHeight / 2) + 1;
            }
            else if (userInput.Key == ConsoleKey.DownArrow)
            {
                if (commandPos.row == (Console.WindowHeight / 2) + 1) commandPos.row += 5;
                else commandPos.row = (Console.WindowHeight / 2) + 1;
            }
            else if (userInput.Key == ConsoleKey.Enter)
            {
                if (commandPos.row == (Console.WindowHeight / 2) + 1) return "start";
                else return "exit";
            }

            Console.SetCursorPosition(commandPos.col, commandPos.row);
            Console.Write(command);
            return "";
        }
    }
}
