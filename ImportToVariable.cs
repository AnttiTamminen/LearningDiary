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

        public static List<Models.Task> DatabaseToTask(Models.Topic topic)
        {
            List<Models.Task> taskList = new List<Models.Task>();
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
            {
                var tasksOfTopic = newConnection.Tasks.Where(task => task.TopicId == newConnection.Topics.ElementAt(topic.Id).Id).ToList();
                if (tasksOfTopic.Any())
                    taskList = tasksOfTopic;
            }
            return taskList;
        }
    }
}