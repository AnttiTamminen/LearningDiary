using System;
using System.Collections.Generic;

namespace LearningDiary
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Topic> topicList = new List<Topic>();
            string topicProgressAnswer;
            string taskAddAnswer;
            string taskPrioAnswer;
            string taskCompleteAnswer;


            Console.WriteLine("Do you want to input a topic? (yes/no)");
            string answerToStart = Console.ReadLine().ToLower();
            while (answerToStart == "yes")
            {
                topicList.Add(new Topic());

                Console.WriteLine("Give topic Id");
                topicList[topicList.Count - 1].Id = int.Parse(Console.ReadLine());

                Console.WriteLine("Give Title");
                topicList[topicList.Count - 1].Title = Console.ReadLine();

                Console.WriteLine("Give description");
                topicList[topicList.Count - 1].Description = Console.ReadLine();

                Console.WriteLine("Give estimated time to master");
                topicList[topicList.Count - 1].EstimatedTimeToMaster = double.Parse(Console.ReadLine());

                Console.WriteLine("Give time spent");
                topicList[topicList.Count - 1].TimeSpent = double.Parse(Console.ReadLine());

                Console.WriteLine("Give source");
                topicList[topicList.Count - 1].Source = Console.ReadLine();

                Console.WriteLine("Give date of starting");
                topicList[topicList.Count - 1].StartLearningDate = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Is topic in progress (yes/no)");
                topicProgressAnswer = Console.ReadLine().ToLower();
                if (topicProgressAnswer == "yes")
                    topicList[topicList.Count - 1].InProgress = true;
                else
                    topicList[topicList.Count - 1].InProgress = false;

                if (topicList[topicList.Count - 1].InProgress == false)
                {
                    Console.WriteLine("Give completion date");
                    topicList[topicList.Count - 1].CompletionDate = Convert.ToDateTime(Console.ReadLine());
                }
                else
                    topicList[topicList.Count - 1].CompletionDate = new DateTime(1, 1, 1);

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
                foreach (var topic in topicList)
                {
                    Console.WriteLine("Title: " + topic.Title + " " + "Id: " + topic.Id);
                }
            }
        }
        //public static List<Topic> CreateTopics()
        //{
        //    List<Topic> topicList = new List<Topic>();
        //    string topicProgressAnswer;
        //    string taskAddAnswer;
        //    string taskPrioAnswer;
        //    string taskCompleteAnswer;
        //}
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
