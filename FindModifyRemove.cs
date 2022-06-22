using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LearningDiary.Models;

namespace LearningDiary
{
    public class FindModifyRemove
    {
        public static void FindModifyTopic(string url)
        {
            string answerToStart;
            Topic wantedTopic = new Topic();
            IEnumerable<Topic> foundTopic;

            Console.WriteLine("\nFind a specific topic? (yes/no)");
            answerToStart = Console.ReadLine().ToLower();
            while (answerToStart == "yes")
            {
                Console.Clear();
                PrintToConsole.PrintTopics(url);

                Console.WriteLine("Type 1 to search with title or 2 to search with id");
                string answer12 = Console.ReadLine();
                while (answer12 != "1" && answer12 != "2")
                {
                    Console.WriteLine("Try again.\nType 1 to search with title, or 2 to search with id");
                    answer12 = Console.ReadLine();
                }

                if (answer12 == "1")
                {
                    foundTopic = FindTopicByTitle(url);

                    if (foundTopic.Any())
                    {
                        wantedTopic = foundTopic.ToList()[0];
                    }
                }
                else if (answer12 == "2")
                {
                    foundTopic = FindTopicById(url);

                    if (foundTopic.Any())
                    {
                        wantedTopic = foundTopic.ToList()[0];
                    }
                }

                if (wantedTopic.Id != -999)
                {
                    Console.WriteLine("Do you want to modify/remove this topic?");
                    answerToStart = Console.ReadLine().ToLower();
                    if (answerToStart == "yes")
                    {
                        Console.WriteLine("Type 1 to modify topic or 2 to remove it");
                        answer12 = Console.ReadLine();
                        while (answer12 != "1" && answer12 != "2")
                        {
                            Console.WriteLine("Try again.\nType 1 to modify topic, or 2 to remove it");
                            answer12 = Console.ReadLine();
                        }
                        if (answer12 == "1")
                            ModifyTopic(url, wantedTopic);
                        else if (answer12 == "2")
                            RemoveTopic(url, wantedTopic);
                    }
                }

                Console.WriteLine("\nFind another specific topic? (yes/no)");
                answerToStart = Console.ReadLine().ToLower();
            }
        }

        public static void ModifyTopic(string url, Topic topicToModify)
        {
            List<Topic> oldTopicList = ImportToVariable.FileTxtToTopiclist(url);
            int index = oldTopicList.FindIndex(topic => topic.Id == topicToModify.Id);
            oldTopicList[index] = Create.CreateTopic(false, url, topicToModify);
            File.Delete(url);
            SaveData.TopicsToTxtfile(oldTopicList, url);
        }

        public static void RemoveTopic(string url, Topic topicToRemove)
        {
            List<Topic> oldTopicList = ImportToVariable.FileTxtToTopiclist(url);
            int index = oldTopicList.FindIndex(topic => topic.Id == topicToRemove.Id);
            oldTopicList.RemoveAt(index);
            File.Delete(url);
            SaveData.TopicsToTxtfile(oldTopicList, url);
        }

        public static IEnumerable<Topic> FindTopicByTitle(string url)
        {
            Console.WriteLine("Give topic title to search");
            string searchTitle = Console.ReadLine();

            List<Topic> topicList = ImportToVariable.FileTxtToTopiclist(url);

            IEnumerable<Topic> wantedTopic = topicList.Where(aihe => aihe.Title == searchTitle);
            if (wantedTopic.Any())
            {
                PrintToConsole.PrintTopics(wantedTopic.ToList()); // tässä menee lista -> enumerable -> list. Täytyy katsoa saako yksikertaistettua
            }
            else
                Console.WriteLine("Topic was not found in current diary");
            return wantedTopic;
        }

        public static IEnumerable<Topic> FindTopicById(string url)
        {
            const bool tryAgain = true;
            Console.WriteLine("Give topic id to search");
            int? searchId;
            while (tryAgain)
            {
                try
                {
                    searchId = Int32.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Input seems to be incorrect. Try again\nGive topic id to search");

                }
            }

            List<Topic> topicList = ImportToVariable.FileTxtToTopiclist(url);

            IEnumerable<Topic> wantedTopic = topicList.Where(aihe => aihe.Id == searchId);
            if (wantedTopic.Any())
                PrintToConsole.PrintTopics(wantedTopic.ToList()); // tässä menee lista -> enumerable -> list. Täytyy katsoa saako yksikertaistettua
            else
                Console.WriteLine("Topic was not found in current diary");
            return wantedTopic;
        }

        public static void ModifyTask(string url, Task taskToModify, int topicId)
        {
            List<Topic> oldTopicList = ImportToVariable.FileTxtToTopiclist(url);
            List<Task> oldTaskList = oldTopicList[topicId].TaskList;
            int index = oldTaskList.FindIndex(task => task.Id == taskToModify.Id);
            oldTaskList[index] = Create.CreateTasks(false, url, topicId, taskToModify);
            oldTopicList[topicId].TaskList = oldTaskList;
            File.Delete(url);
            SaveData.TopicsToTxtfile(oldTopicList, url);
        }

        public static void RemoveTask(string url, Task taskToRemove, int topicId)
        {
            List<Topic> oldTopicList = ImportToVariable.FileTxtToTopiclist(url);
            List<Task> oldTaskList = oldTopicList[topicId].TaskList;
            int index = oldTaskList.FindIndex(task => task.Id == taskToRemove.Id);
            oldTaskList.RemoveAt(index);
            oldTopicList[topicId].TaskList = oldTaskList;
            File.Delete(url);
            SaveData.TopicsToTxtfile(oldTopicList, url);
        }




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
        }

        public static void RemoveMTask(int id)
        {
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                newConnection.Tasks.Remove(newConnection.Tasks.Where(task => task.Id == id).Single());

                newConnection.SaveChanges();
            }
        }

        public static bool FindMTopicWId(int id)
        {
            bool foundTopic = false;
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                if (newConnection.Topics.Select(topic => topic.Id).Contains(id))
                    foundTopic = true;
            }
            return foundTopic;
        }

        public static bool FindMTopicWTitle(string title)
        {
            bool foundTopic = false;
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                if (newConnection.Topics.Select(topic => topic.Title).Contains(title))
                    foundTopic = true;
            }
            return foundTopic;
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