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

            Welcome();

            Console.WriteLine("Do you want to input a topic? (yes/no)");
            string answerToStart = Console.ReadLine().ToLower();
            if (answerToStart == "yes")
            {
                List<Topic> createdTopics = CreateTopics(answerToStart, url);

                TopicsToTxtfile(createdTopics, url);
            }

            Console.WriteLine("Do you want to see list of all topics? (yes/no)");
            answerToStart = Console.ReadLine().ToLower();
            if (answerToStart == "yes" && File.Exists(url) && new FileInfo(url).Length != 0)
            {
                PrintTopics(url);

                //Console.WriteLine("Find a specific topic? (yes/no)");
                //answerToStart = Console.ReadLine().ToLower();
                //if (answerToStart == "yes")
                //{
                //    Console.WriteLine("Search with title give 1 search with id give 2");
                //    ...
                //}
            }
            else if (answerToStart == "yes")
            {
                Console.Clear();
                Console.WriteLine("No topics created yet. Restart program to give first topic entry!");
            }
        }

        public static List<Topic> FileTxtToTopiclist(string url)
        {
            string[] fileTextArray = File.ReadAllText(url).Split("###");
            fileTextArray = fileTextArray.Take(fileTextArray.Length - 1).ToArray(); //removes last empty element from array (I need to change the file structure at some point
            List<Topic> topicList = new List<Topic>();
            int headingLength;
            for (int i = 0; i < fileTextArray.Length; i++)
            {
                string[] topicFieldsArray = fileTextArray[i].Split("##");
                topicList.Add(new Topic());

                headingLength = 4;
                topicList[i].Id = Convert.ToInt32(topicFieldsArray[0].Substring(headingLength,
                    topicFieldsArray[0].Length - headingLength));

                headingLength = 7;
                topicList[i].Title = topicFieldsArray[1].Substring(headingLength,
                    topicFieldsArray[1].Length - headingLength);

                headingLength = 13;
                topicList[i].Description = topicFieldsArray[2].Substring(headingLength,
                    topicFieldsArray[2].Length - headingLength);

                headingLength = 23;
                string etaMasterString = topicFieldsArray[3].Substring(headingLength,
                    topicFieldsArray[3].Length - headingLength);
                if (!String.IsNullOrEmpty(etaMasterString))
                    topicList[i].EstimatedTimeToMaster = Convert.ToDouble(etaMasterString);

                headingLength = 11;
                string timeSpentString = topicFieldsArray[4].Substring(headingLength,
                    topicFieldsArray[4].Length - headingLength);
                if (!String.IsNullOrEmpty(timeSpentString))
                    topicList[i].TimeSpent = Convert.ToDouble(timeSpentString);

                headingLength = 8;
                topicList[i].Source = topicFieldsArray[5].Substring(headingLength,
                    topicFieldsArray[5].Length - headingLength);

                headingLength = 19;
                string startDateString = topicFieldsArray[6].Substring(headingLength,
                    topicFieldsArray[6].Length - headingLength);
                if (!String.IsNullOrEmpty(startDateString))
                    topicList[i].StartLearningDate = Convert.ToDateTime(startDateString);

                headingLength = 12;
                topicList[i].InProgress = Convert.ToBoolean(topicFieldsArray[7].Substring(headingLength,
                    topicFieldsArray[7].Length - headingLength));

                headingLength = 16;
                string completeDateString = topicFieldsArray[8].Substring(headingLength,
                    topicFieldsArray[8].Length - headingLength);
                if (!String.IsNullOrEmpty(completeDateString))
                    topicList[i].CompletionDate = Convert.ToDateTime(completeDateString);

                if (topicFieldsArray[9].Contains("*TASK*"))
                    topicList[i].TaskList = FileTxtToTasklist(topicFieldsArray[9]);
            }
            return topicList;
        }

        public static List<Task> FileTxtToTasklist(string tasksFromFile)
        {
            List<Task> taskList = new List<Task>();
            string[] taskArray = tasksFromFile.Split("*TASK*");

            if (taskArray.Any())
            {
                int headingLength;
                for (int i = 1; i < taskArray.Length; i++) //taskArray first element is null so i starts from one
                {
                    taskList.Add(new Task());
                    string[] taskFieldsArray = taskArray[i].Split("+++");

                    if (taskFieldsArray.Any())
                    {
                        headingLength = 4;
                        string idString = taskFieldsArray[0].Substring(headingLength,
                            taskFieldsArray[0].Length - headingLength);
                        if (!String.IsNullOrEmpty(idString))
                            taskList[i-1].Id = Convert.ToInt32(idString);

                        headingLength = 7;
                        taskList[i-1].Title = taskFieldsArray[1].Substring(headingLength,
                            taskFieldsArray[1].Length - headingLength);

                        headingLength = 13;
                        taskList[i - 1].Description = taskFieldsArray[2].Substring(headingLength,
                            taskFieldsArray[2].Length - headingLength);

                        headingLength = 6;
                        taskList[i - 1].Notes = new List<string>(taskFieldsArray[3].Substring(headingLength,
                            taskFieldsArray[3].Length - headingLength).Trim().Split(' '));

                        headingLength = 10;
                        string deadlineString = taskFieldsArray[4].Substring(headingLength,
                            taskFieldsArray[4].Length - headingLength);
                        if (!String.IsNullOrEmpty(deadlineString))
                            taskList[i - 1].Deadline = Convert.ToDateTime(deadlineString);

                        headingLength = 10;
                        string taskPriority = taskFieldsArray[5].Substring(headingLength,
                            taskFieldsArray[5].Length - headingLength);
                        if (taskPriority == "Low")
                            taskList[i - 1].Priority = Task.EnumPriority.Low;
                        else if (taskPriority == "Medium")
                            taskList[i - 1].Priority = Task.EnumPriority.Medium;
                        else if (taskPriority == "High")
                            taskList[i - 1].Priority = Task.EnumPriority.High;

                        headingLength = 6;
                        taskList[i - 1].Done = Convert.ToBoolean(taskFieldsArray[6].Substring(headingLength,
                            taskFieldsArray[6].Length - headingLength));
                    }
                }
            }
            return taskList;
        }

        public static void FindTopicByTitle(string url, string searchTitle)
        {
            List <Topic> topicList = FileTxtToTopiclist(url);

            //if (File.Exists(url) && new FileInfo(url).Length != 0)
            //{

            //    // Creating Dictionary of Topics by title
            //    Dictionary<string, string[]> topicDictionary = new Dictionary<string, string[]>();
            //    string[] fileTextAsArray = File.ReadAllText(url).Split("###");
            //    int titleHeadingLenght = 5;
            //    for (int i = 0; i < fileTextAsArray.Length - 1; i++)
            //    {
            //        string[] topicFieldsArray = fileTextAsArray[i].Split("##");
            //        string titleKey = topicFieldsArray[1].Substring(titleHeadingLenght, topicFieldsArray[1].Length - titleHeadingLenght + 1);
            //        if (titleKey != " ")
            //            topicDictionary.Add(titleKey, topicFieldsArray);
            //    }

            //}
        }

        public static void Welcome()
        {
            Console.WriteLine("*********************************************************************\n\nWelcome to Learning diary console app!" +
                              "\n\nAnswer questions as stated.\nYou can always just press enter to skip question or answer no." +
                              "\n\n*********************************************************************\n");
        }

        public static void PrintTopics(string url)
        {
            Console.Clear();
            List<Topic> readTopics = FileTxtToTopiclist(url);
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
                if (i < taskToPrint.Count -1)
                    Console.WriteLine("\n+++++++++++++++++++++++++++\n");
            }
            Console.WriteLine();
        }

        public static void PrintNotes(List<string> note)
        {
            for (int i = 1; i < note.Count+1; i++)
            {
                if (i % 13 == 0)
                    Console.WriteLine();
                Console.Write($"{note[i - 1]} ");
            }
            Console.WriteLine();
        }

        public static void TopicsToTxtfile(List<Topic> createdTopics, string url)
        {
            for (int i = 0; i < createdTopics.Count; i++)
            {
                foreach (var value in createdTopics[i].GetType().GetProperties()) 
                {
                    if (value.Name == "TaskList" && createdTopics[i].TaskList?.Any() == true)
                    {
                        TasksToTxtfile(createdTopics[i], url);
                        File.AppendAllText(url, "##");
                    }
                    else
                        File.AppendAllText(url, string.Format($"{value.Name.ToUpper()}: {value.GetValue(createdTopics[i])}##"));
                }

                File.AppendAllText(url, "#");
            }
        }

        public static void TasksToTxtfile(Topic oneTopic, string url)
        {
            File.AppendAllText(url, "TaskList: ");
            List <Task> listOfTasks = oneTopic.TaskList;  
            for (int j = 0; j < listOfTasks.Count; j++)
            {
                File.AppendAllText(url, "*TASK*");
                foreach (var item in listOfTasks[j].GetType().GetProperties())
                {
                    if (item.Name == "Notes" && listOfTasks[j].Notes?.Any() == true)
                    {
                        File.AppendAllText(url, string.Format($"{item.Name.ToUpper()}: "));
                        NotesToTxtfile(listOfTasks[j], url);
                    }
                    else
                        File.AppendAllText(url, string.Format($"{item.Name.ToUpper()}: {item.GetValue(listOfTasks[j])}+++"));
                }
            }
        }

        public static void NotesToTxtfile(Task oneTask, string url)
        {
            for (int i = 0; i < oneTask.Notes.Count; i++)
            {
                File.AppendAllText(url, string.Format($"{oneTask.Notes[i]} "));
            }
            File.AppendAllText(url, "+++");
        }

        public static List<Topic> CreateTopics(string answerToStart, string url)
        {
            int nextId;

            //Hakee viimeisen Topic Idn tiedostosta
            if (File.Exists(url) && new FileInfo(url).Length != 0)
            {
                string[] fileTextAsArray = File.ReadAllText(url).Split("###");
                string lastTopicStr = fileTextAsArray[fileTextAsArray.Length - 2];
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
            bool tryAgain = true;

            while (answerToStart == "yes")
            {
                topicList.Add(new Topic());

                // Adding Id
                topicList[topicList.Count - 1].Id = nextId;

                // Adding title
                Console.WriteLine("Give Title");
                topicList[topicList.Count - 1].Title = Console.ReadLine();

                // Addin description
                Console.WriteLine("Give description");
                topicList[topicList.Count - 1].Description = Console.ReadLine();

                // Adding time to master
                Console.WriteLine("Give estimated time to master in hours");
                while (tryAgain)
                {
                    try
                    {
                        estimatedTimeAnswer = Console.ReadLine();
                        if (!String.IsNullOrEmpty(estimatedTimeAnswer))
                            topicList[topicList.Count - 1].EstimatedTimeToMaster = double.Parse(estimatedTimeAnswer);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Input seems to be incorrect format.\nGive estimated time tom master in hours and use \",\" as decimal mark");
                    }
                }

                // Adding source
                Console.WriteLine("Give source");
                topicList[topicList.Count - 1].Source = Console.ReadLine();

                // Adding start date
                Console.WriteLine("Give date of starting (YYYY, MM, DD, HH:MM)");
                while (tryAgain)
                {
                    try
                    {
                        startDate = Console.ReadLine();
                        if (!String.IsNullOrEmpty(startDate))
                            topicList[topicList.Count - 1].StartLearningDate = Convert.ToDateTime(startDate);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Input seems to be incorrect format.\nGive start date in format (YYYY, MM, DD, HH:MM)");
                    }
                }

                // Adding progress status
                Console.WriteLine("Is topic in progress (yes/no)");
                topicProgressAnswer = Console.ReadLine().ToLower();
                if (topicProgressAnswer == "yes")
                    topicList[topicList.Count - 1].InProgress = true;
                else 
                    topicList[topicList.Count - 1].InProgress = false;

                // Adding completion date
                if (topicList[topicList.Count - 1].InProgress == false)
                {
                    Console.WriteLine("Give completion date (YYYY, MM, DD, HH:MM)");
                    while (tryAgain)
                    {
                        try
                        {
                            completeDate = Console.ReadLine();
                            if (!String.IsNullOrEmpty(completeDate))
                                topicList[topicList.Count - 1].CompletionDate = Convert.ToDateTime(completeDate);
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Input seems to be incorrect format.\nGive completion date in format (YYYY, MM, DD, HH:MM)");
                        }
                    }
                }

                // Adding time spent
                if (topicList[topicList.Count - 1].CompletionDate != null &&
                    topicList[topicList.Count - 1].StartLearningDate != null)
                {
                    topicList[topicList.Count - 1].TimeSpent = ((TimeSpan)(topicList[topicList.Count - 1].CompletionDate - topicList[topicList.Count - 1].StartLearningDate)).TotalHours;
                }

                // Adding tasks
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
            bool tryAgain = true;

            while (taskAddAnswer == "yes")
            {
                taskList.Add(new Task());

                // Adding Id
                taskList[taskList.Count - 1].Id = nextTaskId;

                // Adding title
                Console.WriteLine("Give Title to Task");
                taskList[taskList.Count - 1].Title = Console.ReadLine();

                // Adding description
                Console.WriteLine("Give description to Task");
                taskList[taskList.Count - 1].Description = Console.ReadLine();

                // Adding deadline
                Console.WriteLine("Give deadline to Task (YYYY, MM, DD, HH:MM)");
                while (tryAgain)
                {
                    try
                    {
                        taskDeadline = Console.ReadLine();
                        if (!String.IsNullOrEmpty(taskDeadline))
                            taskList[taskList.Count - 1].Deadline = Convert.ToDateTime(taskDeadline);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Input seems to be incorrect format.\nGive deadline date in format (YYYY, MM, DD, HH:MM)");
                    }
                } 

                // Adding priority
                Console.WriteLine("Give priority to Task (Low/Medium/High)");
                taskPrioAnswer = Console.ReadLine();
                while (taskPrioAnswer != "Low" && taskPrioAnswer != "Medium" && taskPrioAnswer != "High" && !String.IsNullOrEmpty(taskPrioAnswer))
                {
                    Console.WriteLine("Input seems to be incorrect format.\nGive priority to Task as one of these: (Low/Medium/High), notice upper and lower cases");
                    taskPrioAnswer = Console.ReadLine();
                }
                if (taskPrioAnswer == "High")
                    taskList[taskList.Count - 1].Priority = Task.EnumPriority.High;
                else if (taskPrioAnswer == "Medium")
                    taskList[taskList.Count - 1].Priority = Task.EnumPriority.Medium;
                else if (taskPrioAnswer == "Low")
                    taskList[taskList.Count - 1].Priority = Task.EnumPriority.Low;

                // Adding notes
                Console.WriteLine("Add note text to task");
                taskList[taskList.Count - 1].Notes = new List<string>(Console.ReadLine().Split(' '));

                // Adding complete status
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
