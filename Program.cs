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
            string taskPrioAnswer;
            string taskCompleteAnswer;


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

                List<Task> taskList = new List<Task>();
                Console.WriteLine("Do you want to add task to this topic? (yes/no)");
                taskAddAnswer = Console.ReadLine().ToLower();
                while (taskAddAnswer == "yes")
                {
                    taskList.Add(new Task());

                    Console.WriteLine("Give id to Task");
                    taskList[taskList.Count - 1].Id = int.Parse(Console.ReadLine());

                    Console.WriteLine("Give Title to Task");
                    taskList[taskList.Count - 1].Title = Console.ReadLine();

                    Console.WriteLine("Give description to Task");
                    taskList[taskList.Count - 1].Description = Console.ReadLine();

                    Console.WriteLine("Give deadline to Task");
                    taskList[taskList.Count - 1].Deadline = Convert.ToDateTime(Console.ReadLine());

                    Console.WriteLine("Give priority to Task (Low/Medium/High)");
                    taskPrioAnswer = Console.ReadLine();
                    if (taskPrioAnswer == "High")
                        taskList[taskList.Count - 1].Priority = Task.EnumPriority.High;
                    else if (taskPrioAnswer == "Medium")
                        taskList[taskList.Count - 1].Priority = Task.EnumPriority.Medium;
                    else if (taskPrioAnswer == "Low")
                        taskList[taskList.Count - 1].Priority = Task.EnumPriority.Low;

                    Console.WriteLine("Add note text to task");
                    taskList[taskList.Count - 1].Notes = new List<string> (Console.ReadLine().Split(' '));

                    Console.WriteLine("Is task complete (yes/no)");
                    taskCompleteAnswer = Console.ReadLine();
                    if (taskCompleteAnswer.ToLower() == "yes")
                        taskList[taskList.Count - 1].Done = true;
                    else
                        taskList[taskList.Count - 1].Done = false;

                    Console.WriteLine("Do you want to input another task to this topic? yes/no");
                    taskAddAnswer = Console.ReadLine().ToLower();
                }
                topicList[topicList.Count - 1].TaskList = taskList;

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
        public List<Task> TaskList { get; set; }

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

        public enum EnumPriority
        {
            Low,
            Medium,
            High
        }
        public EnumPriority Priority { get; set; }

        public bool Done { get; set; }
    }
}
