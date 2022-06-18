using System;
using System.Linq;
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
                while (taskAddAnswer == "yes")
                {
                    newTopic.TaskList.Add(new Task());
                    newTopic.TaskList[newTopic.TaskList.Count - 1] =
                        CreateTasks(true, url, newTopic.Id, newTopic.TaskList[newTopic.TaskList.Count - 1]);
                    Console.WriteLine("Do you want to add another task to this topic? (yes/no)");
                    taskAddAnswer = Console.ReadLine().ToLower();
                }
            }
            else
            {
                Console.WriteLine("Do you want to modify (1) remove (2) add (3) this topics tasks");
                string taskAnswer = Console.ReadLine().ToLower();
                string taskMod = "";

                if (newTopic.TaskList != null && (taskAnswer == "1" || taskAnswer == "2"))
                {
                    Console.WriteLine("Give task id (1) or Title (2)");
                    taskMod = Console.ReadLine();
                }
                else if (newTopic.TaskList == null && (taskAnswer == "1" || taskAnswer == "2"))
                    Console.WriteLine("Topic does not have any tasks to modify/remove");

                if (taskAnswer == "1" && newTopic.TaskList != null) 
                {
                    if (taskMod == "1")
                    {
                        Console.WriteLine("Give task id");
                        string idTask = Console.ReadLine();
                        Task taskToModify = newTopic.TaskList.Where(task => task.Id == Int32.Parse(idTask)).Single();
                        FindModifyRemove.ModifyTask(url, taskToModify, newTopic.Id);
                    }
                    else if (taskMod == "2")
                    {
                        Console.WriteLine("Give task title");
                        string titleTask = Console.ReadLine();
                        Task taskToModify = newTopic.TaskList.Where(task => task.Title == titleTask).Single();
                        FindModifyRemove.ModifyTask(url, taskToModify, newTopic.Id);
                    }
                }
                else if (taskAnswer == "2" && newTopic.TaskList != null)
                {
                    if (taskMod == "1")
                    {
                        Console.WriteLine("Give task id");
                        string idTask = Console.ReadLine();
                        Task taskToRemove = newTopic.TaskList.Where(task => task.Id == Int32.Parse(idTask)).Single();
                        FindModifyRemove.RemoveTask(url, taskToRemove, newTopic.Id);
                    }
                    else if (taskMod == "2")
                    {
                        Console.WriteLine("Give task title");
                        string titleTask = Console.ReadLine();
                        Task taskToRemove = newTopic.TaskList.Where(task => task.Title == titleTask).Single();
                        FindModifyRemove.RemoveTask(url, taskToRemove, newTopic.Id);
                    }
                }
                else if (taskAnswer == "3") 
                {
                    if (newTopic.TaskList == null)
                        newTopic.TaskList = new List<Task>();
                    newTopic.TaskList.Add(new Task());
                    newTopic.TaskList[newTopic.TaskList.Count - 1] = CreateTasks(true, url, newTopic.Id, newTopic.TaskList[newTopic.TaskList.Count - 1]);
                }
            }

            return newTopic;
        }

        public static Task CreateTasks(bool nTask, string url, int topicId, Task newTask)
        {
            Console.Clear();

            const bool tryAgain = true;

            if (!nTask)
                newTask.Id = FileToVariable.GetLatestTaskId(url, topicId); 

            // Adding title
            Console.WriteLine("Give Title to Task or press enter");
            string answer = Console.ReadLine();
            if (!String.IsNullOrEmpty(answer))
                newTask.Title = answer;

            // Adding description
            Console.WriteLine("Give description to Task or press enter");
            answer = Console.ReadLine();
            if (!String.IsNullOrEmpty(answer))
                newTask.Title = answer;

            // Adding deadline
            Console.WriteLine("Give deadline to Task (YYYY, MM, DD, HH:MM) or press enter");
            answer = Console.ReadLine();
            if (!String.IsNullOrEmpty(answer))
            {
                while (tryAgain)
                {
                    try
                    {
                        newTask.Deadline = Convert.ToDateTime(answer);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Input seems to be incorrect format.\nGive deadline date in format (YYYY, MM, DD, HH:MM)");
                        answer = Console.ReadLine();
                    }
                }
            }

            // Adding priority
            Console.WriteLine("Give priority to Task (Low/Medium/High) or press enter"); 
            answer = Console.ReadLine();
            while (answer != "Low" && answer != "Medium" && answer != "High" && !String.IsNullOrEmpty(answer))
            {
                Console.WriteLine("Input seems to be incorrect format.\nGive priority to Task as one of these: (Low/Medium/High), notice upper and lower cases");
                answer = Console.ReadLine();
            }
            if (answer == "High")
                newTask.Priority = Task.EnumPriority.High;
            else if (answer == "Medium")
                newTask.Priority = Task.EnumPriority.Medium;
            else if (answer == "Low")
                newTask.Priority = Task.EnumPriority.Low;

            // Adding notes
            Console.WriteLine("Write note to task or press enter");
            answer = Console.ReadLine();
            if (!String.IsNullOrEmpty(answer))
                newTask.Notes = new List<string>(answer.Split(' '));

            // Adding complete status
            Console.WriteLine("Is task complete (yes/no) or press enter");
            answer = Console.ReadLine();
            if (!String.IsNullOrEmpty(answer))
            {
                if (answer == "yes")
                    newTask.Done = true;
                else if (answer == "no")
                    newTask.Done = false;
            }

            return newTask;
        }
    }
}