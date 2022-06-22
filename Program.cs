using System;
using System.Collections.Generic;
using System.IO;

namespace LearningDiary
{
    public class Program
    {
        static void Main(string[] args)
        {
            string url = @"C:\Visual Studio\Projects\LearningDiary\Diary.txt";

            Welcome();

            Console.WriteLine("Do you want to input a topic? (yes/no)");
            string answerToStart = Console.ReadLine().ToLower();
            while (answerToStart == "yes")
            {
                Create.CreateMTopic();

                Console.WriteLine("Do you want to add task to this topic? (yes/no)");
                answerToStart = Console.ReadLine();
                while (answerToStart == "yes")
                {
                    Create.CreateMTask();

                    Console.WriteLine("Do you want to input another task to this topic (yes/no)");
                    answerToStart = Console.ReadLine();
                }

                Console.WriteLine("Do you want to input another topic (yes/no)");
                answerToStart = Console.ReadLine();
            }

            Console.WriteLine("Do you want to see list of all topics? (yes/no)");
            answerToStart = Console.ReadLine().ToLower();
            if (answerToStart == "yes" && File.Exists(url) && new FileInfo(url).Length != 0)
            {
                PrintToConsole.PrintTopics(url);

                FindModifyRemove.FindModifyTopic(url);

                PrintToConsole.PrintTopics(url);
            }
            else if (answerToStart == "yes")
            {
                Console.Clear();
                Console.WriteLine("No topics created yet. Restart program to give first topic entry!");
            }
        }

        public static void Welcome()
        {
            Console.WriteLine("*********************************************************************\n\nWelcome to Learning diary console app!" +
                              "\n\nAnswer questions as stated.\nYou can always just press enter to skip question or answer no." +
                              "\n\n*********************************************************************\n");
        }
    }
}
