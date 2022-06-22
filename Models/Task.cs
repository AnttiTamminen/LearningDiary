using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace LearningDiary.Models
{
    public partial class Task
    {
        public Task()
        {
            Title = Create.AddTitle();
            Description = Create.AddDescription();
            Deadline = Create.AddDeadline();
            Priority = Create.AddPriority();
            Done = Create.AddDone();
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
                TopicId = newConnection.Topics.TakeLast(1).Select(topic => topic.Id).Single();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public string Priority { get; set; }
        public bool? Done { get; set; }
        public int TopicId { get; set; }

        public virtual Topic Topic { get; set; }
    }
}
