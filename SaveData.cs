using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LearningDiary.Models;

namespace LearningDiary
{
    public class SaveData
    {
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

            TopicsToDatabase(createdTopics);
        }

        public static void TopicsToDatabase(List<Topic> createdTopics)
        {
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                foreach (Topic topic in createdTopics)
                {
                    LearningDiary.Models.Topic modelTopic = new LearningDiary.Models.Topic();
                    modelTopic.Id = topic.Id;
                    modelTopic.Title = topic.Title;
                    modelTopic.Description = topic.Description;
                    modelTopic.TimeToMaster = (decimal)topic.EstimatedTimeToMaster;
                    modelTopic.Source = topic.Source;
                    modelTopic.StartLearningDate = topic.StartLearningDate;
                    modelTopic.InProgress = topic.InProgress;
                    modelTopic.CompletionDate = topic.CompletionDate;

                    newConnection.Topics.Add(modelTopic);
                }
                newConnection.SaveChanges();
            }
        }

        public static void TasksToTxtfile(Topic oneTopic, string url)
        {
            File.AppendAllText(url, "TaskList: ");
            List<Task> listOfTasks = oneTopic.TaskList;
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
                        File.AppendAllText(url,
                            string.Format($"{item.Name.ToUpper()}: {item.GetValue(listOfTasks[j])}+++"));
                }
            }
            TasksToDatabase(listOfTasks, oneTopic);
        }

        public static void TasksToDatabase(List<Task> listOfTasks, Topic oneTopic)
        {
            if (listOfTasks.Any())
            {
                using (LearningDiaryContext newConnection = new LearningDiaryContext())
                {
                    foreach (Task task in listOfTasks)
                    {
                        LearningDiary.Models.Task modelTask = new LearningDiary.Models.Task();
                        modelTask.Id = task.Id;
                        modelTask.Title = task.Title;
                        modelTask.Description = task.Description;
                        StringBuilder notes = new StringBuilder();
                        foreach (string word in task.Notes)
                            notes.Append(word + " ");
                        modelTask.Notes = notes.ToString().Trim();
                        modelTask.Deadline = task.Deadline;
                        modelTask.Priority = task.Priority.ToString();
                        modelTask.Done = task.Done;
                        modelTask.TopicId = oneTopic.Id;

                        newConnection.Tasks.Add(modelTask);
                    }

                    newConnection.SaveChanges();
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
    }
}