using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LearningDiary.Models;
using Utility;

namespace LearningDiary
{
    public class FindModifyRemove
    {
        public static void ModifyMTopic(int id)
        {
            Console.WriteLine("Enter number to modify:\n1) - Title\n2) - Description\n3) - Time to master\n4) - Source\n5) - Start learning date\n6) - In progress\n7) - Completion date");
            string number = Console.ReadLine();
            while (number != "1" && number != "2" && number != "3" && number != "4" && number != "5" && number != "6" && number != "7")
            {
                Console.WriteLine("Incorrect number please try again\n1) - Title\n2) - Description\n3) - Time to master\n4) - Source\n5) - Start learning date\n6) - In progress\n7) - Completion date");
                number = Console.ReadLine();
            }

            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                if (number == "1")
                {
                    newConnection.Topics.Where(topic => topic.Id == id).Single().Title = Create.AddTitle();
                    newConnection.Topics.Update(newConnection.Topics.Where(topic => topic.Id == id).Single());
                }
                else if (number == "2")
                {
                    newConnection.Topics.Where(topic => topic.Id == id).Single().Description = Create.AddDescription();
                    newConnection.Topics.Update(newConnection.Topics.Where(topic => topic.Id == id).Single());
                }
                else if (number == "3")
                {
                    newConnection.Topics.Where(topic => topic.Id == id).Single().TimeToMaster = Create.AddTimeToMaster();
                    newConnection.Topics.Update(newConnection.Topics.Where(topic => topic.Id == id).Single());
                }
                else if (number == "4")
                {
                    newConnection.Topics.Where(topic => topic.Id == id).Single().Source = Create.AddSource();
                    newConnection.Topics.Update(newConnection.Topics.Where(topic => topic.Id == id).Single());
                }
                else if (number == "5")
                {
                    newConnection.Topics.Where(topic => topic.Id == id).Single().StartLearningDate = Create.AddStartLearningDate();
                    newConnection.Topics.Update(newConnection.Topics.Where(topic => topic.Id == id).Single());
                }
                else if (number == "6")
                {
                    newConnection.Topics.Where(topic => topic.Id == id).Single().InProgress = Create.AddInProgress();
                    newConnection.Topics.Update(newConnection.Topics.Where(topic => topic.Id == id).Single());
                    if (newConnection.Topics.Where(topic => topic.Id == id).Single().InProgress == true)
                    {
                        newConnection.Topics.Where(topic => topic.Id == id).Single().CompletionDate = null;
                    }
                }
                else if (number == "7")
                {
                    if (newConnection.Topics.Where(topic => topic.Id == id).Single().InProgress == true)
                        Console.WriteLine("Topic status is 'In progress' please change status before giving completion date");
                    else
                    {
                        newConnection.Topics.Where(topic => topic.Id == id).Single().CompletionDate = Create.AddCompletionDate();
                        newConnection.Topics.Update(newConnection.Topics.Where(topic => topic.Id == id).Single());
                    }
                }

                newConnection.Topics.Where(topic => topic.Id == id).Single().TimeSpent = (decimal)((TimeSpan)(
                    newConnection.Topics.Where(topic => topic.Id == id).Single().CompletionDate - newConnection.Topics
                        .Where(topic => topic.Id == id).Single().StartLearningDate)).TotalHours;
            }
        }

        public static void RemoveMTopic(int id)
        {
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                newConnection.Topics.Remove(newConnection.Topics.Where(topic => topic.Id == id).Single());

                newConnection.SaveChanges();
            }
            Console.WriteLine("\nPress any key to continue");
            Console.ReadLine();
        }

        public static void RemoveMTask(int id)
        {
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                newConnection.Tasks.Remove(newConnection.Tasks.Where(task => task.Id == id).Single());

                newConnection.SaveChanges();
            }
            Console.WriteLine("\nPress any key to continue");
            Console.ReadLine();
        }

        public static void FindTopics()
        {
            Console.Clear();
            Console.WriteLine("Give Topic Id or Title:");
            PrintToConsole.PrintTopics(Query.Search(Console.ReadLine(), ImportToVariable.DatabaseToTopiclist()));
        }

        public static bool FindMTaskWId(int id)
        {
            bool foundTask = false;
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                if (newConnection.Tasks.Select(task => task.Id).Contains(id))
                    foundTask = true;
            }
            return foundTask;
        }

        public static bool FindMTaskWTitle(string title)
        {
            bool foundTask = false;
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                if (newConnection.Tasks.Select(task => task.Title).Contains(title))
                    foundTask = true;
            }
            return foundTask;
        }
    }
}