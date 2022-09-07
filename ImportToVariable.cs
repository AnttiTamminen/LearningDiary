using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LearningDiary.Models;

namespace LearningDiary
{
    public class ImportToVariable
    {
        public static List<Topic> DatabaseToTopiclist()
        {
            List<Topic> topicList = new List<Topic>();
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                if (newConnection.Topic.Any())
                    topicList = newConnection.Topic.ToList();
            }
            return topicList;
        }

        public static List<Task> DatabaseToTasklist()
        {
            List<Task> taskList = new List<Task>();
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                if (newConnection.Task.Any())
                    taskList = newConnection.Task.ToList();
            }
            return taskList;
        }

        public static List<Note> DatabaseToNotelist()
        {
            List<Note> noteList = new List<Note>();
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                if (newConnection.Note.Any())
                    noteList = newConnection.Note.ToList();
            }
            return noteList;
        }
    }
}