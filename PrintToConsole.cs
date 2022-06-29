using System;
using System.Collections.Generic;
using System.Linq;
using LearningDiary.Models;

namespace LearningDiary
{
    public class PrintToConsole
    {
        public static void PrintAllTopics()
        {
            Console.Clear();
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {

                List<Topic> topiikit = newConnection.Topics.ToList();
                List<Task> taskit = newConnection.Tasks.ToList();

                var lista = topiikit.Join(taskit, topici => topici, taski => taski.Topic,
                    (topici, taski) => new { TopicT = topici, TaskT = taski }).ToList();




                foreach (Topic topic in newConnection.Topics)
                {
                    Console.WriteLine($"Id: {topic.Id}");
                    Console.WriteLine($"Title: {topic.Title}");
                    Console.WriteLine($"Description: {topic.Id}");
                    Console.WriteLine($"Estimated time to master: {topic.TimeToMaster}");
                    Console.WriteLine($"Source: {topic.Source}");
                    Console.WriteLine($"Start learning date: {topic.StartLearningDate}");
                    Console.WriteLine($"In progress: {topic.InProgress}");
                    Console.WriteLine($"Completion date: {topic.CompletionDate}");
                    Console.WriteLine($"Time spent: {topic.TimeSpent}");
                    Console.WriteLine("Tasks:");

                    //if (topic.Tasks.Count != 0)
                    //{
                    //    foreach (var task in topic.Tasks)
                    //    {
                    //        Console.WriteLine($"Task id:");
                    //        Console.WriteLine("******************************************************************\n");
                    //    }
                    //}
                    //else
                    //    Console.Write("No tasks");


                    Console.WriteLine("\n------------------------------------------------------------------\n");
                }
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadLine();
        }
    }
}