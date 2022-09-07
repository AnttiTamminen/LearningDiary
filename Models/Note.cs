using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace LearningDiary.Models
{
    public partial class Note
    {
        public Note(string title)
        {
            Title = title;
            Note1 = Create.AddNote();
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
                TaskId = newConnection.Task.Max(task => task.Id);
        }

        public Note(){}

        public int Id { get; set; }
        public string Title { get; set; }
        public int TaskId { get; set; }
        public string Note1 { get; set; }

        public virtual Task Task { get; set; }
    }
}
