using System;
using System.Collections.Generic;

namespace LearningDiary
{
    public class Create
    {
        public static Topic CreateTopic(bool createNew, string url, Topic newTopic)
        {
            if (createNew)
            {
                int nextId = FileToVariable.GetLatestTopicId(url);
                newTopic.Id = nextId;
            }

            // Adding/modifying title
            Console.WriteLine("Give Title or press enter");
            string titleAnswer = Console.ReadLine();
            if (!String.IsNullOrEmpty(titleAnswer))
                newTopic.Title = titleAnswer;

            // Adding/modifying description
            Console.WriteLine("Give description or press enter");
            string descAnswer = Console.ReadLine();
            if (!String.IsNullOrEmpty(descAnswer))
                newTopic.Description = descAnswer;

            const bool tryAgain = true;
            // Adding/modifying time to master
            Console.WriteLine("Give estimated time to master in hours or press enter");
            string timeMasterAnswer = Console.ReadLine();
            if (!String.IsNullOrEmpty(timeMasterAnswer))
            {
                while (tryAgain)
                {
                    try
                    {
                        if (!String.IsNullOrEmpty(timeMasterAnswer))
                            newTopic.EstimatedTimeToMaster = double.Parse(timeMasterAnswer);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Input seems to be incorrect format.\nGive estimated time to master in hours and use \",\" as decimal mark");
                        timeMasterAnswer = Console.ReadLine();
                    }
                }
            }


            // Adding/modifying source
            Console.WriteLine("Give source or press enter");
            string sourceAnswer = Console.ReadLine();
            if (!String.IsNullOrEmpty(sourceAnswer))
            {
                newTopic.Source = sourceAnswer;
            }

            // Adding/modifying start date
            Console.WriteLine("Give date of starting (YYYY, MM, DD, HH:MM) or press enter");
            string startAnswer = Console.ReadLine();
            if (!String.IsNullOrEmpty(startAnswer))
            {
                while (tryAgain)
                {
                    try
                    {
                        if (!String.IsNullOrEmpty(startAnswer))
                            newTopic.StartLearningDate = Convert.ToDateTime(startAnswer);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Input seems to be incorrect format.\nGive start date in format (YYYY, MM, DD, HH:MM)");
                        startAnswer = Console.ReadLine();
                    }
                }
            }

            // Adding/modifying progress status
            Console.WriteLine("Is topic in progress (yes/no) or press enter");
            string progressAnswer = Console.ReadLine().ToLower();
            if (!String.IsNullOrEmpty(progressAnswer))
            {
                if (progressAnswer == "yes")
                    newTopic.InProgress = true;
                else if (progressAnswer == "no")
                    newTopic.InProgress = false;
            }

            // Adding/modifying completion date completion date
            if (newTopic.InProgress == false)
            {
                Console.WriteLine("Give completion date (YYYY, MM, DD, HH:MM) or press enter");
                string completeDate = Console.ReadLine();
                if (!String.IsNullOrEmpty(completeDate))
                {
                    while (tryAgain)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(completeDate))
                                newTopic.CompletionDate = DateTime.Parse(completeDate);
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Input seems to be incorrect format.\nGive completion date in format (YYYY, MM, DD, HH:MM)");
                            completeDate = Console.ReadLine();
                        }
                    }
                }
            }

            // Adding time spent
            if (newTopic.CompletionDate != null && newTopic.StartLearningDate != null)
                newTopic.TimeSpent = ((TimeSpan)(newTopic.CompletionDate - newTopic.StartLearningDate)).TotalHours;

            // Adding/modifying tasks 
            if (createNew)
            {
                Console.WriteLine("Do you want to add task to this topic? (yes/no)");
                string taskAddAnswer = Console.ReadLine().ToLower();
                if (taskAddAnswer == "yes")
                {
                    newTopic.TaskList = CreateTasks(taskAddAnswer);
                }
            }
            //else
            //{
            //    Console.WriteLine("Do you want to update this topics tasks or add task? (yes/no)");
            //    string taskAnswer = Console.ReadLine().ToLower();
            //    if (taskAnswer == "yes")
            //    {
            //        //JATKA TÄSTÄ
            //    }
            //}

            return newTopic;
        }

        public static List<Task> CreateTasks(string taskAddAnswer)
        {
            Console.Clear();
            List<Task> taskList = new List<Task>();
            string taskPrioAnswer;
            string taskCompleteAnswer;
            string taskDeadline;

            int nextTaskId = 0;
            const bool tryAgain = true;

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
                taskCompleteAnswer = Console.ReadLine().ToLower();
                if (taskCompleteAnswer == "yes")
                    taskList[taskList.Count - 1].Done = true;
                else if (taskCompleteAnswer == "no")
                    taskList[taskList.Count - 1].Done = false;

                Console.WriteLine("Do you want to input another task to this topic? yes/no");
                taskAddAnswer = Console.ReadLine().ToLower();
                Console.Clear();

                nextTaskId += 1;
            }
            return taskList;
        }
    }
}