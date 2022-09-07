using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SearchObject;
using LearningDiary.Models;

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
                        PrintToConsole.PrintTopics();
                        break;

                    case '2':
                        Console.Clear();
                        Create.CreateTopics();
                        break;

                    case '3':
                        Console.Clear();
                        Console.WriteLine("Add task to latest topic (1) or specify topic (2)");
                        string optionTask = Console.ReadLine();
                        Create.CreateTasks(optionTask);
                        break;

                    case '4':
                        Console.Clear();
                        Console.WriteLine("Add Note to latest task (1) or specify task (2)");
                        string optionNote = Console.ReadLine();
                        Create.CreateNotes(optionNote);
                        break;

                    case '5':
                        FindModifyRemove.FindTopics();
                        break;

                    case '6':
                        FindModifyRemove.FindTasks();
                        break;

                    case '7':
                        FindModifyRemove.FindNotes();
                        break;

                    case '8':
                        Console.Clear();
                        Console.WriteLine("Are you sure you want to delete all data?");
                        if (Console.ReadLine().ToLower() == "yes")
                            FindModifyRemove.RemoveTopic(ImportToVariable.DatabaseToTopiclist());
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
            Console.WriteLine("Press:\n\n1) See whole diary\n2) Input a topic\n3) Input a task\n4) Input a note\n5) Search/modify/remove Topics\n6) Search/modify/remove Tasks\n7) Search/modify/remove Notes\n8) Clear all data\n0) To exit\n");
        }
    }
}
