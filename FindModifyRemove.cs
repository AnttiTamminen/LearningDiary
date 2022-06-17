using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            List<Topic> oldTopicList = FileToVariable.FileTxtToTopiclist(url);
            int index = oldTopicList.FindIndex(topic => topic.Id == topicToModify.Id);
            oldTopicList[index] = Create.CreateTopic(false, url, topicToModify);
            File.Delete(url);
            ToTxtFile.TopicsToTxtfile(oldTopicList, url);
        }

        public static void RemoveTopic(string url, Topic topicToRemove)
        {
            List<Topic> oldTopicList = FileToVariable.FileTxtToTopiclist(url);
            int index = oldTopicList.FindIndex(topic => topic.Id == topicToRemove.Id);
            oldTopicList.RemoveAt(index);
            File.Delete(url);
            ToTxtFile.TopicsToTxtfile(oldTopicList, url);
        }

        public static IEnumerable<Topic> FindTopicByTitle(string url)
        {
            Console.WriteLine("Give topic title to search");
            string searchTitle = Console.ReadLine();

            List<Topic> topicList = FileToVariable.FileTxtToTopiclist(url);

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

            List<Topic> topicList = FileToVariable.FileTxtToTopiclist(url);

            IEnumerable<Topic> wantedTopic = topicList.Where(aihe => aihe.Id == searchId);
            if (wantedTopic.Any())
                PrintToConsole.PrintTopics(wantedTopic.ToList()); // tässä menee lista -> enumerable -> list. Täytyy katsoa saako yksikertaistettua
            else
                Console.WriteLine("Topic was not found in current diary");
            return wantedTopic;
        }
    }
}