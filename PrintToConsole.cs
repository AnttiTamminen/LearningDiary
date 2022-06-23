using System;
using System.Collections.Generic;
using LearningDiary.Models;

namespace LearningDiary
{
    public class PrintToConsole
    {
        public static void PrintTopics()
        {
            Console.Clear();
            List<Topic> currentTopics = ImportToVariable.DatabaseToTopiclist();

            foreach (Topic topic in currentTopics)
            {
                Console.WriteLine($"Id: {topic.Id}");
                Console.WriteLine($"Title: {topic.Title}");
                Console.WriteLine($"Description: {topic.Id}");
                // .....
                Console.WriteLine("\n------------------------------------------------------------------\n");
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadLine();
        }
    }
}