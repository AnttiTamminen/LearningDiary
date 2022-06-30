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
                if (newConnection.Topics.Any())
                {
                    foreach (Topic topic in newConnection.Topics)
                    {
                        Console.WriteLine($"Id: {topic.Id}");
                        Console.WriteLine($"Title: {topic.Title}");
                        Console.WriteLine($"Description: {topic.Id}");
                        Console.WriteLine($"Estimated time to master: {topic.TimeToMaster}");
                        Console.WriteLine($"Source: {topic.Source}");
                        Console.WriteLine($"Start learning date: {topic.StartLearningDate}");
                        Console.WriteLine($"In progress: {topic.InProgress}");
                        Console.WriteLine($"Completion date: {topic.CompletionDate}");
                        Console.WriteLine($"Time spent: {topic.TimeSpent}");
                        Console.WriteLine("Tasks:");

                        PrintTasks(topic);

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
                        Console.WriteLine($"Description: {topic.Id}");
                        Console.WriteLine($"Estimated time to master: {topic.TimeToMaster}");
                        Console.WriteLine($"Source: {topic.Source}");
                        Console.WriteLine($"Start learning date: {topic.StartLearningDate}");
                        Console.WriteLine($"In progress: {topic.InProgress}");
                        Console.WriteLine($"Completion date: {topic.CompletionDate}");
                        Console.WriteLine($"Time spent: {topic.TimeSpent}");
                        Console.WriteLine("Tasks:");

                        PrintTasks(topic);

                        Console.WriteLine("\n------------------------------------------------------------------\n");
                    }
            }
            else
                Console.WriteLine("Topic not found");
           
            Console.WriteLine("\nPress any key to continue");
            Console.ReadLine();
        }

        public static void PrintTasks(Topic topic)
        {
            using (LearningDiaryContext newConnection2 = new LearningDiaryContext())
            {
                var list1 = newConnection2.Topics.ToList().Join(newConnection2.Tasks.ToList(), topici => topici,
                    taski => taski.Topic, (topici, taski) => new {Taski = taski, ToId = topici.Id});

                if (list1.Any())
                {
                    foreach (var task in list1.Select(x => x.Taski).Where(y => y.TopicId == topic.Id))
                    {
                        Console.WriteLine("\n******************************************************************\n");
                        Console.WriteLine($"Task id: {task.Id}");
                        Console.WriteLine($"Task title: {task.Title}");
                        Console.WriteLine($"Task description: {task.Description}");
                        Console.WriteLine($"Task deadline: {task.Deadline}");
                        Console.WriteLine($"Task Priority: {task.Priority}");
                        Console.WriteLine($"Task is done: {task.Done}");
                        Console.WriteLine("Notes:");

                        PrintNotes(task);
                    }
                    Console.WriteLine("");
                }
                else
                    Console.WriteLine("0 tasks".PadLeft(15));
            }
        }

        public static void PrintNotes(Task task)
        {
            using (LearningDiaryContext newConnection3 = new LearningDiaryContext())
            {
                var list2 = newConnection3.Tasks.ToList().Join(newConnection3.Notes.ToList(), tski => tski,
                    note => note.Task, (tski, note) => new { Notet = note, TaId = tski.Id });

                if (list2.Any())
                {
                    foreach (var note in list2.Select(x => x.Notet).Where(y => y.TaskId == task.Id))
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

    }
}