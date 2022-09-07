using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LearningDiary.Models;
using SearchObject;

namespace LearningDiary
{
    public class FindModifyRemove
    {
        public static void Update(List<Topic> topicList)
        {
            Console.WriteLine("\n\nPress:\n1) Modify Topic\n2) Remove Topic\n0) Back to main menu");
            string answer = Console.ReadLine();
            while (answer != "1" && answer != "2" && answer != "0")
            {
                Console.WriteLine(
                    "Incorrect number please try again\n\nPress:\n1) Modify Topic\n2) Remove Topic\n0) Back to main menu");
                answer = Console.ReadLine();
            }

            int id;
            if (topicList.Count > 1)
            {
                Console.WriteLine("Give Topic id");
                while (!int.TryParse(Console.ReadLine(), out id))
                    Console.WriteLine("Try again\nGive Topic id");
            }
            else
                id = topicList.First().Id;

            Console.Clear();
            PrintToConsole.PrintTopics(topicList);

            switch (answer)
            {
                case "1":
                    ModifyTopic(id);
                    break;
                case "2":
                    RemoveTopic(id);
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }

        public static void Update(List<Task> taskList)
        {
            Console.WriteLine("\n\nPress:\n1) Modify Task\n2) Remove Task\n0) Back to main menu");
            var answer = Console.ReadLine();
            while (answer != "1" && answer != "2" && answer != "0")
            {
                Console.WriteLine(
                    "Incorrect number please try again\n\nPress:\n1) Modify Task\n2) Remove Task\n0) Back to main menu");
                answer = Console.ReadLine();
            }

            int id;
            if (taskList.Count > 1)
            {
                Console.WriteLine("Give Task id");
                while (!int.TryParse(Console.ReadLine(), out id))
                    Console.WriteLine("Try again\nGive Task id");
            }
            else
            {
                id = taskList.First().Id;
            }

            Console.Clear();
            PrintToConsole.PrintTasks(taskList);

            switch (answer)
            {
                case "1":
                    ModifyTask(id);
                    break;
                case "2":
                    RemoveTask(id);
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }

        public static void Update(List<Note> noteList)
        {
            Console.WriteLine("\n\nPress:\n1) Modify Note\n2) Remove Note\n0) Back to main menu");
            var answer = Console.ReadLine();
            while (answer != "1" && answer != "2" && answer != "0")
            {
                Console.WriteLine(
                    "Incorrect number please try again\n\nPress:\n1) Modify Note\n2) Remove Note\n0) Back to main menu");
                answer = Console.ReadLine();
            }

            int id;
            if (noteList.Count > 1)
            {
                Console.WriteLine("Give Note id");
                while (!int.TryParse(Console.ReadLine(), out id))
                    Console.WriteLine("Try again\nGive Note id");
            }
            else
            {
                id = noteList.First().Id;
            }

            Console.Clear();
            PrintToConsole.PrintNotes(noteList);

            switch (answer)
            {
                case "1":
                    ModifyNote(id);
                    break;
                case "2":
                    RemoveNote(id);
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }

        public static void ModifyTopic(int id)
        {
            Console.WriteLine("\nEnter number to modify:\n1) Title\n2) Description\n3) Time to master\n4) Source\n" +
                              "5) Start learning date\n6) In progress\n7) Completion date");
            string number = Console.ReadLine();
            while (number != "1" && number != "2" && number != "3" && number != "4" && number != "5" && number != "6" && number != "7")
            {
                Console.WriteLine("Incorrect number please try again\nEnter number to modify:\n1) Title\n2) Description\n" +
                                  "3) Time to master\n4) Source\n5) Start learning date\n6) In progress\n7) Completion date");
                number = Console.ReadLine();
            }

            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                Topic topic = newConnection.Topic.Where(topic => topic.Id == id).Single();
                switch (number)
                {
                    case "1":
                        topic.Title = Create.AddTitle();
                        break;

                    case "2":
                        topic.Description = Create.AddDescription();
                        break;

                    case "3":
                        topic.TimeToMaster = Create.AddTimeToMaster();
                        break;

                    case "4":
                        topic.Source = Create.AddSource();
                        break;

                    case "5":
                        topic.StartLearningDate = Create.AddStartLearningDate();
                        break;

                    case "6":
                        topic.InProgress = Create.AddInProgress();
                        if (topic.InProgress == true)
                        {
                            topic.CompletionDate = null;
                        }
                        break;

                    case "7":
                        if (topic.InProgress == true)
                            Console.WriteLine("Topic status is 'In progress' please change status before giving completion date");
                        else
                            topic.CompletionDate = Create.AddCompletionDate();
                        break;
                }

                if ((number == "7" || number == "5") && topic.CompletionDate != null && topic.StartLearningDate != null)
                {
                    topic.TimeSpent = (decimal)((TimeSpan)(topic.CompletionDate - topic.StartLearningDate)).TotalHours;
                }

                newConnection.SaveChanges();
            }
        }

        public static void ModifyTask(int id)
        {
            Console.WriteLine("\nEnter number to modify:\n1) Title\n2) Description\n3) Deadline\n4) Priority\n5) Done");
            string number = Console.ReadLine();
            while (number != "1" && number != "2" && number != "3" && number != "4" && number != "5")
            {
                Console.WriteLine(
                    "Incorrect number please try again\nEnter number to modify:\n1) Title\n2) Description\n3) Deadline\n4) Priority\n5) Done");
                number = Console.ReadLine();
            }

            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                Task task = newConnection.Task.Where(task => task.Id == id).Single();
                switch (number)
                {
                    case "1":
                        task.Title = Create.AddTitle();
                        break;

                    case "2":
                        task.Description = Create.AddDescription();
                        break;

                    case "3":
                        task.Deadline = Create.AddDeadline();
                        break;

                    case "4":
                        task.Priority = Create.AddPriority();
                        break;

                    case "5":
                        task.Done = Create.AddDone();
                        break;
                }
                newConnection.SaveChanges();
            }
        }

        public static void ModifyNote(int id)
        {
            Console.WriteLine("\nEnter number to modify:\n1) Title\n2) Note");
            string number = Console.ReadLine();
            while (number != "1" && number != "2")
            {
                Console.WriteLine(
                    "Incorrect number please try again\nEnter number to modify:\n1) Title\n2) Note");
                number = Console.ReadLine();
            }

            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                Note note = newConnection.Note.Where(note => note.Id == id).Single();
                switch (number)
                {
                    case "1":
                        note.Title = Create.AddTitle();
                        break;

                    case "2":
                        note.Note1 = Create.AddNote();
                        break;
                }
                newConnection.SaveChanges();
            }
        }

        public static void RemoveTopic(int id)
        {
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                newConnection.Topic.Remove(newConnection.Topic.Where(topic => topic.Id == id).Single());
                foreach (var task in newConnection.Task.Where(task => task.TopicId == id))
                {
                    newConnection.Task.Remove(task);

                    RemoveNote(task);
                }
                newConnection.SaveChanges();
            }
        }

        public static void RemoveTopic(List<Topic> topicList)
        {
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                foreach (var topic in topicList)
                {
                    newConnection.Topic.Remove(topic);
                    foreach (var task in newConnection.Task.Where(task => task.TopicId == topic.Id))
                    {
                        newConnection.Task.Remove(task);

                        RemoveNote(task);
                    }
                }
                newConnection.SaveChanges();
            }
        }

        public static void RemoveTask(int id)
        {
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                newConnection.Task.Remove(newConnection.Task.Where(task => task.Id == id).Single());
                
                RemoveNote(newConnection.Task.Where(task => task.Id == id).Single());

                newConnection.SaveChanges();
            }
        }

        public static void RemoveNote(Task task)
        {
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                foreach (var note in newConnection.Note.Where(note => note.TaskId == task.Id))
                    newConnection.Note.Remove(note);

                newConnection.SaveChanges();
            }

        }

        public static void RemoveNote(int id)
        {
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                newConnection.Note.Remove(newConnection.Note.Where(note => note.Id == id).Single());

                newConnection.SaveChanges();
            }
        }

        public static void FindTopics()
        {
            Console.Clear();
            Console.WriteLine("Give Topic Id or Title:");
            List<Topic> result = Query.Search(Console.ReadLine(), ImportToVariable.DatabaseToTopiclist());
            PrintToConsole.PrintTopics(result);
            Update(result);
        }

        public static void FindTasks()
        {
            Console.Clear();
            Console.WriteLine("Give Task Id or Title:");
            List<Task> result = Query.Search(Console.ReadLine(), ImportToVariable.DatabaseToTasklist());
            PrintToConsole.PrintTasks(result);
            Update(result);
        }

        public static void FindNotes()
        {
            Console.Clear();
            Console.WriteLine("Give Note Id or Title:");
            List<Note> result = Query.Search(Console.ReadLine(), ImportToVariable.DatabaseToNotelist());
            PrintToConsole.PrintNotes(result);
            Update(result);
        }
    }
}