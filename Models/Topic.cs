using System;
using System.Collections.Generic;

#nullable disable

namespace LearningDiary.Models
{
    public partial class Topic
    {
        public Topic(string title)
        {
            Tasks = new HashSet<Task>();
            Title = title;
            Description = Create.AddDescription();
            TimeToMaster = Create.AddTimeToMaster();
            Source = Create.AddSource();
            StartLearningDate = Create.AddStartLearningDate();
            InProgress = Create.AddInProgress();
            if (InProgress == false)
                CompletionDate = Create.AddCompletionDate();
            if (CompletionDate != null && StartLearningDate != null)
                TimeSpent = (decimal)((TimeSpan)(CompletionDate - StartLearningDate)).TotalHours;
        }

        public Topic() {}

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? TimeToMaster { get; set; }
        public decimal? TimeSpent { get; set; }
        public string Source { get; set; }
        public DateTime? StartLearningDate { get; set; }
        public bool? InProgress { get; set; }
        public DateTime? CompletionDate { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
