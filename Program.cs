using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Utility;

namespace LearningDiary
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Options();
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        Create.CreateTopics();
                        break;

                    case '2':
                        Console.Clear();
                        Console.WriteLine("Add task to latest topic (1) or specify topic (2)");
                        string optionTask = Console.ReadLine();
                        Create.CreateTasks(optionTask);
                        break;

                    case '3':
                        Console.Clear();
                        Console.WriteLine("Add Note to latest task (1) or specify task (2)");
                        string optionNote = Console.ReadLine();
                        Create.CreateNotes(optionNote);
                        break;

                    case '4':
                        Console.Clear();
                        PrintToConsole.PrintAllTopics();
                        break;

                    case '5':
                        FindModifyRemove.FindTopics();
                        break;

                    case '0':
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                }
            }
        }
        private static void Options()
        {
            Console.Clear();
            Console.WriteLine("*********************************************************************\n\n" +
            "LEARNING DIARY 5000".PadLeft(40) +
            "\n\n*********************************************************************\n");
            Console.WriteLine("Press:\n1) To input a topic\n2) To input a task\n3) To input a note\n4) To see all Topics\n5) Search Topics\n0) To exit\n");
        }
    }
}
