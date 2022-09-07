using System;
using System.Linq;
using System.Collections.Generic;
using LearningDiary.Models;

namespace LearningDiary
{
    public class Create
    {
        public static string AddTitle()
        {
            Console.WriteLine("Give Title:");
            string titleAnswer = Console.ReadLine();
            while (String.IsNullOrEmpty(titleAnswer))
            {
                Console.WriteLine("Topic must have a title. Try again.\nGive Title.");
                titleAnswer = Console.ReadLine();
            }
            return titleAnswer;
        }

        public static string AddDescription()
        {
            Console.WriteLine("Give description or press enter");
            string descAnswer = Console.ReadLine();
            if (!String.IsNullOrEmpty(descAnswer))
                return descAnswer;
            return null;
        }

        public static decimal? AddTimeToMaster()
        {
            const bool tryAgain = true;
            Console.WriteLine("Give estimated time to master in hours or press enter");
            string timeMasterAnswer = Console.ReadLine();
            if (!String.IsNullOrEmpty(timeMasterAnswer))
            {
                while (tryAgain)
                {
                    try
                    {
                        if (!String.IsNullOrEmpty(timeMasterAnswer))
                            return decimal.Parse(timeMasterAnswer);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Input seems to be incorrect format.\nGive estimated time to master in hours and use \",\" as decimal mark");
                        timeMasterAnswer = Console.ReadLine();
                    }
                }
            }
            return null;
        }

        public static string AddSource()
        {
            Console.WriteLine("Give source or press enter");
            string sourceAnswer = Console.ReadLine();
            if (!String.IsNullOrEmpty(sourceAnswer))
                return sourceAnswer;
            return null;
        }

        public static DateTime? AddStartLearningDate()
        {
            const bool tryAgain = true;
            Console.WriteLine("Give date of starting (YYYY, MM, DD, HH:MM) or press enter");
            string startAnswer = Console.ReadLine();
            if (!String.IsNullOrEmpty(startAnswer))
            {
                while (tryAgain)
                {
                    try
                    {
                        if (!String.IsNullOrEmpty(startAnswer))
                            return Convert.ToDateTime(startAnswer);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Input seems to be incorrect format.\nGive start date in format (YYYY, MM, DD, HH:MM)");
                        startAnswer = Console.ReadLine();
                    }
                }
            }
            return null;
        }

        public static bool? AddInProgress()
        {
            Console.WriteLine("Is topic in progress (yes/no) or press enter");
            string progressAnswer = Console.ReadLine().ToLower();
            if (!String.IsNullOrEmpty(progressAnswer))
            {
                if (progressAnswer == "yes")
                    return true;
                if (progressAnswer == "no")
                    return false;
            }
            return null;
        }

        public static DateTime? AddCompletionDate()
        {
            const bool tryAgain = true;
            Console.WriteLine("Give completion date (YYYY, MM, DD, HH:MM) or press enter");
            string completeDate = Console.ReadLine();
            if (!String.IsNullOrEmpty(completeDate))
            {
                while (tryAgain)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(completeDate))
                            return DateTime.Parse(completeDate);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Input seems to be incorrect format.\nGive completion date in format (YYYY, MM, DD, HH:MM)");
                        completeDate = Console.ReadLine();
                    }
                }
            }
            return null;
        }

        public static DateTime? AddDeadline()
        {
            const bool tryAgain = true;
            Console.WriteLine("Give deadline (YYYY, MM, DD, HH:MM) or press enter");
            string startAnswer = Console.ReadLine();
            if (!String.IsNullOrEmpty(startAnswer))
            {
                while (tryAgain)
                {
                    try
                    {
                        if (!String.IsNullOrEmpty(startAnswer))
                            return Convert.ToDateTime(startAnswer);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Input seems to be incorrect format.\nGive deadline in format (YYYY, MM, DD, HH:MM)");
                        startAnswer = Console.ReadLine();
                    }
                }
            }
            return null;
        }

        public static string AddPriority()
        {
            Console.WriteLine("Give priority (Low/Medium/High) or press enter");
            string priorityAnswer = Console.ReadLine();
            while (priorityAnswer != "Low" && priorityAnswer != "Medium" && priorityAnswer != "High" && !String.IsNullOrEmpty(priorityAnswer))
            {
                Console.WriteLine("Input was incorrect please try again:");
                priorityAnswer = Console.ReadLine();
            }
            if (!String.IsNullOrEmpty(priorityAnswer))
                return priorityAnswer;
            return null;
        }

        public static bool? AddDone()
        {
            Console.WriteLine("Is task done (yes/no) or press enter");
            string progressAnswer = Console.ReadLine().ToLower();
            if (!String.IsNullOrEmpty(progressAnswer))
            {
                if (progressAnswer == "yes")
                    return true;
                if (progressAnswer == "no")
                    return false;
            }
            return null;
        }

        public static string AddNote()
        {
            Console.WriteLine("Type note:");
            string noteAnswer = Console.ReadLine();
            if (!String.IsNullOrEmpty(noteAnswer))
                return noteAnswer;
            return null;
        }

        public static void CreateTopics()
        {
            string title = AddTitle();
            Topic newTopic = new Topic(title);
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                newConnection.Topic.Add(newTopic);
                newConnection.SaveChanges();
            }

            Console.WriteLine("Give task to topic yes/no?");
            string answer = Console.ReadLine().ToLower();
            while (answer == "yes")
            {
                CreateTasks("1");
                Console.WriteLine("Give another task to topic yes/no?");
                answer = Console.ReadLine().ToLower();
            }
        }

        public static void CreateTasks(string option)
        {
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                if (option == "2")
                {
                    int newTopicId;
                    Console.WriteLine("Give Topic id to select topic where task is added:");
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out var result) && newConnection.Topic.Select(topic => topic.Id).Contains(result))
                        {
                            newTopicId = result;
                            break;
                        }
                        Console.WriteLine(
                            "Input was incorrect or Id not found, try again\nGive Topic id to select topic where task is added:");
                    }
                    string title = AddTitle();
                    Task newTask = new Task(title);
                    newTask.TopicId = newTopicId;
                    newConnection.Task.Add(newTask);
                }
                else
                {
                    string title = AddTitle();
                    Task newTask = new Task(title);
                    newConnection.Task.Add(newTask);

                    Console.WriteLine("Give note to task yes/no?");
                    string answer = Console.ReadLine().ToLower();
                    while (answer == "yes")
                    {
                        CreateNotes("1");
                        Console.WriteLine("Give another note to task yes/no?");
                        answer = Console.ReadLine().ToLower();
                    }
                }
                newConnection.SaveChanges();
            }

        }

        public static void CreateNotes(string option)
        {
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                if (option == "2")
                {
                    int newTaskId;
                    Console.WriteLine("Give Task id to select task where note is added:");
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out var result) && newConnection.Task.Select(task => task.Id).Contains(result))
                        {
                            newTaskId = result;
                            break;
                        }
                        Console.WriteLine(
                            "Input was incorrect or Id not found, try again\nGive Task id to select task where note is added:");
                    }
                    string title = AddTitle();
                    Note newNote = new Note(title);
                    newNote.TaskId = newTaskId;
                    newConnection.Note.Add(newNote);
                }
                else
                {
                    string title = AddTitle();
                    Note newNote = new Note(title);
                    newConnection.Note.Add(newNote);
                }
                newConnection.SaveChanges();
            }
        }
    }
}