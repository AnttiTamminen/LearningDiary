using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LearningDiary
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = @"C:\Visual Studio\Projects\LearningDiary\Diary.txt";

            Console.WriteLine("Do you want to input a topic? (yes/no)");
            string answerToStart = Console.ReadLine().ToLower();
            if (answerToStart == "yes")
            {
                List<Topic> createdTopics = CreateTopics(answerToStart, url);

                TopicsToTxtfile(createdTopics, url);
            }

            Console.WriteLine("Do you want to see list of all topics? (yes/no)");
            string printList = Console.ReadLine().ToLower();
            if (printList == "yes" && File.Exists(url) && new FileInfo(url).Length != 0)
                PrintTopics(url);
            else
            {
                Console.Clear();
                Console.WriteLine("No topics created yet");
            }
        }

        public static void PrintTopics(string url)
        {
            Console.Clear();
            List<string> diaryContents = File.ReadAllText(url).Split("###").ToList(); // ### used to recognize Topic end
            diaryContents.RemoveAt(diaryContents.Count - 1);
            List<string> printTextList = new List<string>();
            for (int i = 0; i < diaryContents.Count; i++)
            {
                printTextList = diaryContents[i].Split("##").ToList(); // ## used to recognize Topic properties end
                for (int j = 0; j < printTextList.Count; j++)
                    Console.WriteLine(printTextList[j]);

                Console.WriteLine();
                Console.WriteLine("--------------------------------------------------------------"); // --------- Used to divide Topics in console window
            }
        }

        public static void TopicsToTxtfile(List<Topic> createdTopics, string url)
        {
            for (int i = 0; i < createdTopics.Count; i++)
            {
                foreach (var value in createdTopics[i].GetType().GetProperties()) 
                {
                    if (value.Name == "TaskList" && createdTopics[i].TaskList != null)
                        TasksToTxtfile(createdTopics[i], url);
                    else
                        File.AppendAllText(url, string.Format($"{value.Name.ToUpper()}: {value.GetValue(createdTopics[i])}##"));
                }

                File.AppendAllText(url, "#");
            }
        }

        public static void TasksToTxtfile(Topic oneTopic, string url) 
        {
            List<Task> listOfTasks = oneTopic.TaskList;  
            for (int j = 0; j < listOfTasks.Count; j++)
            {
                File.AppendAllText(url, string.Format($"\n*TASK* ( \n"));
                foreach (var item in listOfTasks[j].GetType().GetProperties())
                {
                    if (item.Name == "Notes" && listOfTasks[j].Notes != null)
                    {
                        File.AppendAllText(url, string.Format($"{item.Name.ToUpper()}:##"));
                        NotesToTxtfile(listOfTasks[j], url);
                    }
                    else
                        File.AppendAllText(url, string.Format($"{item.Name.ToUpper()}: {item.GetValue(listOfTasks[j])}##"));
                }
                File.AppendAllText(url, ")##");
            }
        }

        public static void NotesToTxtfile(Task oneTask, string url)
        {
            for (int i = 0; i < oneTask.Notes.Count; i++)
            {
                File.AppendAllText(url, string.Format($"{oneTask.Notes[i]} "));
                if (i % 10 == 0 && i != 0 && i != oneTask.Notes.Count - 1)
                {
                    File.AppendAllText(url, "\n");
                }
            }
            File.AppendAllText(url, "##");
        }

        public static List<Topic> CreateTopics(string answerToStart, string url)
        {
            //Hakee viimeisen Topic idn tiedostosta
            int nextId;

            if (File.Exists(url) && new FileInfo(url).Length != 0)
            {
                string[] fielTextAsArray = File.ReadAllText(url).Split("###");
                string lastTopicStr = fielTextAsArray[fielTextAsArray.Length - 2];
                int idHeaderLength = 4; // maaginen 4 tulee koska "ID: " on neljä merkkiä
                nextId = Convert.ToInt32(lastTopicStr.Substring(idHeaderLength, lastTopicStr.IndexOf('#') - idHeaderLength)) + 1;
            }
            else
                nextId = 0;
 
            List<Topic> topicList = new List<Topic>();
            string topicProgressAnswer;
            string estimatedTimeAnswer;
            string startDate;
            string completeDate;

            while (answerToStart == "yes")
            {
                topicList.Add(new Topic());

                topicList[topicList.Count - 1].Id = nextId;

                Console.WriteLine("Give Title");
                topicList[topicList.Count - 1].Title = Console.ReadLine();

                Console.WriteLine("Give description");
                topicList[topicList.Count - 1].Description = Console.ReadLine();

                Console.WriteLine("Give estimated time to master");
                estimatedTimeAnswer = Console.ReadLine();
                if (!String.IsNullOrEmpty(estimatedTimeAnswer))
                    topicList[topicList.Count - 1].EstimatedTimeToMaster = double.Parse(estimatedTimeAnswer);

                Console.WriteLine("Give source");
                topicList[topicList.Count - 1].Source = Console.ReadLine();

                Console.WriteLine("Give date of starting (YYYY, MM, DD, HH:MM:SS)");
                startDate = Console.ReadLine();
                if (!String.IsNullOrEmpty(startDate))
                    topicList[topicList.Count - 1].StartLearningDate = Convert.ToDateTime(startDate);

                Console.WriteLine("Is topic in progress (yes/no)");
                topicProgressAnswer = Console.ReadLine().ToLower();
                if (topicProgressAnswer == "yes")
                    topicList[topicList.Count - 1].InProgress = true;
                else 
                    topicList[topicList.Count - 1].InProgress = false;

                if (topicList[topicList.Count - 1].InProgress == false)
                {
                    Console.WriteLine("Give completion date (YYYY, MM, DD, HH:MM:SS)");
                    completeDate = Console.ReadLine();
                    if (!String.IsNullOrEmpty(completeDate))
                        topicList[topicList.Count - 1].CompletionDate = Convert.ToDateTime(completeDate);
                }

                if (topicList[topicList.Count - 1].CompletionDate != null &&
                    topicList[topicList.Count - 1].StartLearningDate != null)
                {
                    topicList[topicList.Count - 1].TimeSpent = ((TimeSpan)(topicList[topicList.Count - 1].CompletionDate - topicList[topicList.Count - 1].StartLearningDate)).TotalHours;
                }

                Console.WriteLine("Do you want to add task to this topic? (yes/no)");
                string taskAddAnswer = Console.ReadLine().ToLower();
                if (taskAddAnswer == "yes")
                {
                    topicList[topicList.Count - 1].TaskList = CreateTasks(taskAddAnswer);
                }

                Console.WriteLine("Do you want to input another topic? (yes/no)");
                answerToStart = Console.ReadLine().ToLower();
                Console.Clear();

                nextId += 1;
            }
            return topicList;
        }

        public static List<Task> CreateTasks(string taskAddAnswer)
        {
            Console.Clear();
            List<Task> taskList = new List<Task>();
            string taskPrioAnswer;
            string taskCompleteAnswer;
            string taskDeadline;

            int nextTaskId = 0;

            while (taskAddAnswer == "yes")
            {
                taskList.Add(new Task());

                taskList[taskList.Count - 1].Id = nextTaskId;

                Console.WriteLine("Give Title to Task");
                taskList[taskList.Count - 1].Title = Console.ReadLine();

                Console.WriteLine("Give description to Task");
                taskList[taskList.Count - 1].Description = Console.ReadLine();

                Console.WriteLine("Give deadline to Task (YYYY, MM, DD, HH:MM:SS)");
                taskDeadline = Console.ReadLine();
                if (!String.IsNullOrEmpty(taskDeadline))
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
                taskList[taskList.Count - 1].Notes = new List<string>(Console.ReadLine().Split(' '));

                Console.WriteLine("Is task complete (yes/no)");
                taskCompleteAnswer = Console.ReadLine();
                if (taskCompleteAnswer.ToLower() == "yes")
                    taskList[taskList.Count - 1].Done = true;
                else
                    taskList[taskList.Count - 1].Done = false;

                Console.WriteLine("Do you want to input another task to this topic? yes/no");
                taskAddAnswer = Console.ReadLine().ToLower();
                Console.Clear();

                nextTaskId += 1;
            }
            return taskList;
        }
    }

    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? EstimatedTimeToMaster { get; set; }
        public double? TimeSpent { get; set; }
        public string Source { get; set; }
        public DateTime? StartLearningDate { get; set; }
        public bool InProgress { get; set; }
        public DateTime? CompletionDate { get; set; }
        public List<Task> TaskList { get; set; }
    }

    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Notes { get; set; }
        public DateTime? Deadline { get; set; }

        public enum EnumPriority
        {
            Low,
            Medium,
            High
        }
        public EnumPriority? Priority { get; set; }

        public bool Done { get; set; }
    }
}
