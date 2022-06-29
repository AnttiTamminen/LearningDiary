using System;
using System.Collections.Generic;
using System.Linq;
using LearningDiary.Models;

namespace LearningDiary
{
    public class PrintToConsole
    {
        public static void PrintAllTopics()
        {
            Console.Clear();
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                var list1 = newConnection.Topics.ToList().Join(newConnection.Tasks.ToList(), topici => topici,
                    taski => taski.Topic, (topici, taski) => new { Taskt = taski, TopId = topici.Id });

                var list2 = newConnection.Tasks.ToList().Join(newConnection.Notes.ToList(), tski => tski,
                    note => note.Task, (tski, note) => new { Notet = note, TaId = tski.Id });

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

                    if (list1.Where(x => x.TopId == topic.Id).ToList().Any())
                    {
                        foreach (var task in list1.Where(x => x.TopId == topic.Id).ToList().Select(y => y.Taskt))
                        {
                            Console.WriteLine("\n******************************************************************\n");
                            Console.WriteLine($"Task id: {task.Id}");
                            Console.WriteLine($"Task title: {task.Title}");
                            Console.WriteLine($"Task description: {task.Description}");
                            Console.WriteLine($"Task deadline: {task.Deadline}");
                            Console.WriteLine($"Task Priority: {task.Priority}");
                            Console.WriteLine($"Task is done: {task.Done}");
                            Console.WriteLine("Notes:");

                            if (list2.Where(x => x.TaId == task.Id).ToList().Any())
                            {
                                foreach (var note in list2.Where(x => x.TaId == task.Id).ToList().Select(y => y.Notet))
                                {
                                    Console.WriteLine("\n................................................................\n");
                                    Console.WriteLine($"Note title: {note.Title}");
                                    Console.WriteLine($"Note text: {note.Note1}");
                                }
                            }
                            else
                                Console.WriteLine("0 notes".PadLeft(15));

                            
                        }
                        Console.WriteLine("");
                    }
                    else
                        Console.WriteLine("0 tasks".PadLeft(15));

                    Console.WriteLine("\n------------------------------------------------------------------\n");
                }
            }
            Console.WriteLine("\nPress any key to continue");
            Console.ReadLine();
        }

        public static void PrintAllTopics(List<Topic> topicList)
        {
            Console.Clear();
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                var list1 = topicList.Join(newConnection.Tasks.ToList(), topici => topici,
                    taski => taski.Topic, (topici, taski) => new { Taskt = taski, TopId = topici.Id });

                var list2 = newConnection.Tasks.ToList().Join(newConnection.Notes.ToList(), tski => tski,
                    note => note.Task, (tski, note) => new { Notet = note, TaId = tski.Id });

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

                    if (list1.Where(x => x.TopId == topic.Id).ToList().Any())
                    {
                        foreach (var task in list1.Where(x => x.TopId == topic.Id).ToList().Select(y => y.Taskt))
                        {
                            Console.WriteLine("\n******************************************************************\n");
                            Console.WriteLine($"Task id: {task.Id}");
                            Console.WriteLine($"Task title: {task.Title}");
                            Console.WriteLine($"Task description: {task.Description}");
                            Console.WriteLine($"Task deadline: {task.Deadline}");
                            Console.WriteLine($"Task Priority: {task.Priority}");
                            Console.WriteLine($"Task is done: {task.Done}");
                            Console.WriteLine("Notes:");

                            if (list2.Where(x => x.TaId == task.Id).ToList().Any())
                            {
                                foreach (var note in list2.Where(x => x.TaId == task.Id).ToList().Select(y => y.Notet))
                                {
                                    Console.WriteLine("\n................................................................\n");
                                    Console.WriteLine($"Note title: {note.Title}");
                                    Console.WriteLine($"Note text: {note.Note1}");
                                }
                            }
                            else
                                Console.WriteLine("0 notes".PadLeft(15));


                        }
                        Console.WriteLine("");
                    }
                    else
                        Console.WriteLine("0 tasks".PadLeft(15));

                    Console.WriteLine("\n------------------------------------------------------------------\n");
                }
            }
            Console.WriteLine("\nPress any key to continue");
            Console.ReadLine();
        }
    }
}