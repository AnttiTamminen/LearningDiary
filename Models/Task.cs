using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace LearningDiary.Models
{
    public partial class Task
    {
        public Task(string title)
        {
            Notes = new HashSet<Note>();
            Title = title;
            Description = Create.AddDescription();
            Deadline = Create.AddDeadline();
            Priority = Create.AddPriority();
            Done = Create.AddDone();
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
                TopicId = newConnection.Topic.Max(topic => topic.Id);
        }
        public Task()
        {
            Notes = new HashSet<Note>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public string Priority { get; set; }
        public bool? Done { get; set; }
        public int TopicId { get; set; }

        public virtual Topic Topic { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
