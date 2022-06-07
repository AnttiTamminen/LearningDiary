using System;
using System.Collections.Generic;

namespace LearningDiary
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Topic> topicList = new List<Topic>();
            int id;
            string title;
            string description;
            double estimatedTime;
            double timeSpent;
            string source;
            DateTime startDate;
            string topicProgressAnswer;
            bool inProgress;
            DateTime completionDate;
            string taskAddAnswer;


            Console.WriteLine("Do you want to input a topic? (yes/no)");
            string answerToStart = Console.ReadLine().ToLower();
            while (answerToStart == "yes")
            {
                Console.WriteLine("Give topic Id");
                id = int.Parse(Console.ReadLine());

                Console.WriteLine("Give Title");
                title = Console.ReadLine();

                Console.WriteLine("Give description");
                description = Console.ReadLine();

                Console.WriteLine("Give estimated time to master");
                estimatedTime = double.Parse(Console.ReadLine());

                Console.WriteLine("Give time spent");
                timeSpent = double.Parse(Console.ReadLine());

                Console.WriteLine("Give source");
                source = Console.ReadLine();

                Console.WriteLine("Give date of starting");
                startDate = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Is topic in progress (yes/no)");
                topicProgressAnswer = Console.ReadLine().ToLower();
                if (topicProgressAnswer == "yes")
                    inProgress = true;
                else
                    inProgress = false;

                if (inProgress == false)
                {
                    Console.WriteLine("Give completion date");
                    completionDate = Convert.ToDateTime(Console.ReadLine());
                }
                else
                    completionDate = new DateTime(1, 1, 1);

                topicList.Add(new Topic(id, title, description, estimatedTime, timeSpent, source, startDate, inProgress,
                    completionDate));

                Console.WriteLine("Do you want to add task to this topic? (yes/no)");
                taskAddAnswer = Console.ReadLine().ToLower();
                while (taskAddAnswer == "yes")
                {
                    Console.WriteLine("Give id to Task");
                    topicList[topicList.Count - 1].Task1.Id = int.Parse(Console.ReadLine());

                    //JATKA TÄSTÄ
                }

                Console.WriteLine("Do you want to input another topic?");
                answerToStart = Console.ReadLine().ToLower();
            }

            Console.WriteLine("Do you want to see list of all topics? (yes/no)");
            string printList = Console.ReadLine().ToLower();
            if (printList == "yes")
            {
                for (int i = 0; i < topicList.Count; i++)
                {
                    Console.WriteLine("Title: " + topicList[i].Title + " " + "Id: " + topicList[i].Id);
                }
            }
        }
    }

    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double EstimatedTimeToMaster { get; set; }
        public double TimeSpent { get; set; }
        public string Source { get; set; }
        public DateTime StartLearningDate { get; set; }
        public bool InProgress { get; set; }
        public DateTime CompletionDate { get; set; }
        public Task Task1 { get; set; }
        public Task Task2 { get; set; }
        public Task Task3 { get; set; }

        public Topic(int id, string title, string description, double estimatedTimeToMaster, 
            double timeSpent, string source, DateTime startDate, bool inProgress, DateTime completionDate)
        {
            Id = id;
            Title = title;
            Description = description;
            EstimatedTimeToMaster = estimatedTimeToMaster;
            TimeSpent = timeSpent;
            Source = source;
            StartLearningDate = startDate;
            InProgress = inProgress;
            CompletionDate = completionDate;
        }

    }

    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Notes { get; set; }
        public DateTime Deadline { get; set; }
        public enum Priority{}
        public bool Done { get; set; }
    }
}
