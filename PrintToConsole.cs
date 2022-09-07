using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LearningDiary.Models;

namespace LearningDiary
{
    public class PrintToConsole
    {
        public static void PrintTopics()
        {
            Console.Clear();
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                if (newConnection.Topic.Any())
                {
                    foreach (Topic topic in newConnection.Topic)
                    {
                        Console.WriteLine($"Id: {topic.Id}");
                        Console.WriteLine($"Title: {topic.Title}");
                        Console.WriteLine($"Description: {topic.Description}");
                        Console.WriteLine($"Estimated time to master: {topic.TimeToMaster}");
                        Console.WriteLine($"Source: {topic.Source}");
                        Console.WriteLine($"Start learning date: {topic.StartLearningDate}");
                        Console.WriteLine($"In progress: {topic.InProgress}");
                        Console.WriteLine($"Completion date: {topic.CompletionDate}");
                        Console.WriteLine($"Time spent: {topic.TimeSpent}");
                        Console.WriteLine("Tasks:");

                        PrintTasks(topic.Id);

                        Console.WriteLine("\n------------------------------------------------------------------\n");
                    }
                }
                else
                    Console.WriteLine("No topics found");

                Console.WriteLine("\nPress any key to continue");
                Console.ReadLine();
            }
        }

        public static void PrintTopics(List<Topic> topicList)
        {
            Console.Clear();
            if (topicList.Any())
            {
                    foreach (Topic topic in topicList)
                    {
                        Console.WriteLine($"Id: {topic.Id}");
                        Console.WriteLine($"Title: {topic.Title}");
                        Console.WriteLine($"Description: {topic.Description}");
                        Console.WriteLine($"Estimated time to master: {topic.TimeToMaster}");
                        Console.WriteLine($"Source: {topic.Source}");
                        Console.WriteLine($"Start learning date: {topic.StartLearningDate}");
                        Console.WriteLine($"In progress: {topic.InProgress}");
                        Console.WriteLine($"Completion date: {topic.CompletionDate}");
                        Console.WriteLine($"Time spent: {topic.TimeSpent}");
                        Console.WriteLine("Tasks:");

                        PrintTasks(topic.Id);

                        Console.WriteLine("\n------------------------------------------------------------------\n");
                    }
            }
            else
                Console.WriteLine("Topic not found");
        }

        public static void PrintTasks(int topiId)
        {
            using (LearningDiaryContext newConnection2 = new LearningDiaryContext())
            {
                // query gets all tasks that are in topic which id is given to method. Join creates a object that has tasks and topic id fields
                // from that list only tasks with specified topicId are selected
                IEnumerable<Task> taskSelection = newConnection2.Topic.ToList().Join(newConnection2.Task.ToList(), topic => topic,
                    taski => taski.Topic, (topic, taski) => new {Taski = taski, ToId = topic.Id}).Where(x => x.ToId == topiId).Select(t => t.Taski); 

                if (taskSelection.Any())
                {
                    foreach (var task in taskSelection)
                    {
                        Console.WriteLine("\n******************************************************************\n");
                        Console.WriteLine($"Task id: {task.Id}");
                        Console.WriteLine($"Task title: {task.Title}");
                        Console.WriteLine($"Task description: {task.Description}");
                        Console.WriteLine($"Task deadline: {task.Deadline}");
                        Console.WriteLine($"Task Priority: {task.Priority}");
                        Console.WriteLine($"Task is done: {task.Done}");
                        Console.WriteLine("Notes:");

                        PrintNotes(task.Id);
                    }
                    Console.WriteLine("");
                }
                else
                    Console.WriteLine("0 tasks".PadLeft(15));
            }
        }

        public static void PrintTasks(List<Task> taskList)
        {
            Console.Clear();
                if (taskList.Any())
                {
                    foreach (var task in taskList)
                    {
                        Console.WriteLine("\n******************************************************************\n");
                        Console.WriteLine($"Task id: {task.Id}");
                        Console.WriteLine($"Task title: {task.Title}");
                        Console.WriteLine($"Task description: {task.Description}");
                        Console.WriteLine($"Task deadline: {task.Deadline}");
                        Console.WriteLine($"Task Priority: {task.Priority}");
                        Console.WriteLine($"Task is done: {task.Done}");
                        Console.WriteLine("Notes:");

                        PrintNotes(task.Id);
                    }
                    Console.WriteLine("");
                }
                else
                    Console.WriteLine("No task found.");

                Console.WriteLine("\nPress any key to continue");
                Console.ReadLine();
        }

        public static void PrintNotes(int tasId)
        {
            using (LearningDiaryContext newConnection3 = new LearningDiaryContext())
            {
                IEnumerable<Note> noteSelection = newConnection3.Task.ToList().Join(newConnection3.Note.ToList(), task => task,
                    notei => notei.Task, (task, notei) => new { Notet = notei, TaId = task.Id }).Where(x => x.TaId == tasId).Select(t => t.Notet);

                if (noteSelection.Any())
                {
                    foreach (var note in noteSelection)
                    {
                        Console.WriteLine(
                            "\n................................................................\n");
                        Console.WriteLine($"Note title: {note.Title}");
                        Console.WriteLine($"Note text: {note.Note1}");
                    }
                }
                else
                    Console.WriteLine("0 notes".PadLeft(15));
            }
        }

        public static void PrintNotes(List<Note> noteList)
        {
            Console.Clear();
                if (noteList.Any())
                {
                    foreach (var note in noteList)
                    {
                        Console.WriteLine(
                            "\n................................................................\n");
                        Console.WriteLine($"Note title: {note.Title}");
                        Console.WriteLine($"Note text: {note.Note1}");
                    }
                }
                else
                    Console.WriteLine("No note found.");

                Console.WriteLine("\nPress any key to continue");
                Console.ReadLine();
        }
    }
}