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

        public static void CreateMTopics()
        {
            string title = AddTitle();
            Topic newTopic = new Topic(title);
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                newConnection.Topics.Add(newTopic);
                newConnection.SaveChanges();
            }
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

        public static void CreateMTasks()
        {
            Console.WriteLine("Add task to latest topic (1) or specify topic (2)");
            string option = Console.ReadLine();
            string title = AddTitle();
            Task newTask = new Task(title);
            if (option == "2")
            {
                //newTask.TopicId = // Tähän lisää
            }
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                newConnection.Tasks.Add(newTask);
                newConnection.SaveChanges();
            }
        }

        public static void CreateNotes()
        {
            Console.WriteLine("Add Note to latest task (1) or specify task (2)");
            string option = Console.ReadLine();
            Models.Note newNote = new Models.Note();
            if (option == "2")
            {
                //newNote.TaskId = // Tähän lisää
            }
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                newConnection.Notes.Add(newNote);
                newConnection.SaveChanges();
            }
        }
    }
}