

#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningDiary.Models
{
    public partial class Note
    {
        public Note()
        {
            Title = Create.AddTitle();
            Note1 = Create.AddNote();
            using (LearningDiaryContext newConnection = new LearningDiaryContext())
                TaskId = newConnection.Tasks.TakeLast(1).Select(task => task.Id).Single();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int TaskId { get; set; }
        public string Note1 { get; set; }

        public virtual Task Task { get; set; }
    }
}
