using System;
using System.Collections.Generic;

namespace LearningDiary
{
    public class Topic
    {
        public int Id { get; set; } = -999;
        public string Title { get; set; }
        public string Description { get; set; }
        public double? EstimatedTimeToMaster { get; set; }
        public double? TimeSpent { get; set; }
        public string Source { get; set; }
        public DateTime? StartLearningDate { get; set; }
        public bool? InProgress { get; set; }
        public DateTime? CompletionDate { get; set; }
        public List<Task> TaskList { get; set; }
    }
}