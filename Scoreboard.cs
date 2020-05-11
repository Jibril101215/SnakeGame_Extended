using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Configuration;
using System.Globalization;

namespace Snake
{
    class Scoreboard
    {
        public List<User> users;
        public string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "userData.txt"); // Directory of data file
        

        public Scoreboard()
        {
            users = new List<User>();
            if (File.Exists(dataPath))
            {
                string[] lines = File.ReadAllLines(dataPath);
                foreach (string line in lines)
                {
                    string[] newLine = line.Split(' ');
                    int score  = Int32.Parse(newLine[newLine.Length - 1]);
                    newLine = newLine.Take(newLine.Count() - 1).ToArray();
                    string name = string.Join(" ", newLine);
                    users.Add(new User(name, score));
                }
            } else
            {
                StreamWriter score = File.CreateText(dataPath);
                score.Close();
            }
        }


        public void updateScoreboard(User currUser)
        {
            if (users.Any())
            {
                foreach (User user in users)
                {
                    if (user.name == currUser.name)
                    {
                        user.score = currUser.score;
                        return;
                    }
                }
            }
            users.Add(currUser);
        }

        public void updateFile()
        {
            InsertionSort();
            StreamWriter scores;
            scores = File.CreateText(dataPath);
            foreach (User user in users)
            {
                scores.WriteLine(user.name+' '+user.score);
            }
            
            scores.Close();
        }

        public void DisplayScoreboard()
        {
            Console.Clear();
            int i = ((Console.WindowHeight) / 2) - 3, j = 1;
            Console.SetCursorPosition(2, i);
            Console.WriteLine("SCOREBOARD (TOP 5)");
            foreach(User user in users)
            {
                i++;
                Console.SetCursorPosition(3, i);
                Console.WriteLine(j + ". " + user.name + " - " + user.score);
                if (j++ >= 5) break;
            }
        }

        private void InsertionSort()
        {
            int j;
            User temp;
            for (int i = 1; i < users.Count; i++)
            {
                temp = users[i];
                j = i;
                while (j > 0 && users[j-1].score < temp.score)
                {
                    users[j ] = users[j-1];
                    j--;
                }
                users[j] = temp;
            }
        }
        
    }
}
