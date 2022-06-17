using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LearningDiary
{
    public class FileToVariable
    {
        public static List<Topic> FileTxtToTopiclist(string url)
        {
            string[] fileTextArray = File.ReadAllText(url).Split("###");
            fileTextArray = fileTextArray.Take(fileTextArray.Length - 1).ToArray(); //removes last empty element from array (I need to change the file structure at some point)
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
                string inProgressString = topicFieldsArray[7]
                    .Substring(headingLength, topicFieldsArray[7].Length - headingLength);
                if (!String.IsNullOrEmpty(inProgressString))
                    topicList[i].InProgress = Convert.ToBoolean(inProgressString);

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
                for (int i = 1; i < taskArray.Length; i++) //taskArray first element is null so i starts from one (need to clean this sometime)
                {
                    taskList.Add(new Task());
                    string[] taskFieldsArray = taskArray[i].Split("+++");

                    if (taskFieldsArray.Any())
                    {
                        headingLength = 4;
                        string idString = taskFieldsArray[0].Substring(headingLength,
                            taskFieldsArray[0].Length - headingLength);
                        if (!String.IsNullOrEmpty(idString))
                            taskList[i - 1].Id = Convert.ToInt32(idString);

                        headingLength = 7;
                        taskList[i - 1].Title = taskFieldsArray[1].Substring(headingLength,
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

        public static int GetLatestTopicId(string url)
        {
            int nextId;
            if (File.Exists(url) && new FileInfo(url).Length != 0)
            {
                List<Topic> existingTopics = FileTxtToTopiclist(url);
                nextId = existingTopics.Max(topic => topic.Id) + 1; 
            }
            else
                nextId = 0;

            return nextId;
        }
    }
}