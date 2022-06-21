using System;
using System.Collections.Generic;

namespace LearningDiary
{
    public class PrintToConsole
    {
        public static void PrintTopics(string url)
        {
            Console.Clear();
            List<Topic> readTopics = ImportToVariable.FileTxtToTopiclist(url);
            for (int i = 0; i < readTopics.Count; i++)
            {
                foreach (var value in readTopics[i].GetType().GetProperties())
                {
                    if (value.Name == "TaskList" && readTopics[i].TaskList != null)
                    {
                        Console.WriteLine($"\n{value.Name}:\n");
                        PrintTasks(readTopics[i].TaskList);
                    }
                    else
                        Console.WriteLine($"{value.Name}: {value.GetValue(readTopics[i])}");
                }
                Console.WriteLine("\n------------------------------------------------------\n");
            }
        }

        public static void PrintTopics(List<Topic> topicList)
        {
            Console.Clear();
            for (int i = 0; i < topicList.Count; i++)
            {
                foreach (var value in topicList[i].GetType().GetProperties())
                {
                    if (value.Name == "TaskList" && topicList[i].TaskList != null)
                    {
                        Console.WriteLine($"\n{value.Name}:\n");
                        PrintTasks(topicList[i].TaskList);
                    }
                    else
                        Console.WriteLine($"{value.Name}: {value.GetValue(topicList[i])}");
                }
                Console.WriteLine("\n------------------------------------------------------\n");
            }
        }

        public static void PrintTasks(List<Task> taskToPrint)
        {
            for (int i = 0; i < taskToPrint.Count; i++)
            {
                foreach (var value in taskToPrint[i].GetType().GetProperties())
                {
                    if (value.Name == "Notes" && taskToPrint[i].Notes != null)
                    {
                        Console.WriteLine($"{value.Name}:");
                        PrintNotes(taskToPrint[i].Notes);
                    }
                    else
                        Console.WriteLine($"{value.Name}: {value.GetValue(taskToPrint[i])}");
                }
                if (i < taskToPrint.Count - 1)
                    Console.WriteLine("\n+++++++++++++++++++++++++++\n");
            }
            Console.WriteLine();
        }

        public static void PrintNotes(List<string> note)
        {
            for (int i = 1; i < note.Count + 1; i++)
            {
                if (i % 13 == 0)
                    Console.WriteLine();
                Console.Write($"{note[i - 1]} ");
            }
            Console.WriteLine();
        }
    }
}