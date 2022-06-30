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
                if (newConnection.Topics.Any())
                    topicList = newConnection.Topics.ToList();
            }
            return topicList;
        }

        public static List<Task> DatabaseToTasklist()
        {
            List<Task> taskList = new List<Task>();
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                if (newConnection.Tasks.Any())
                    taskList = newConnection.Tasks.ToList();
            }
            return taskList;
        }

        public static List<Note> DatabaseToNotelist()
        {
            List<Note> noteList = new List<Note>();
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                if (newConnection.Notes.Any())
                    noteList = newConnection.Notes.ToList();
            }
            return noteList;
        }
    }
}