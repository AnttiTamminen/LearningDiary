using System;
using System.Collections.Generic;

namespace LearningDiary
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Notes { get; set; }
        public DateTime? Deadline { get; set; }

        public enum EnumPriority
        {
            Low,
            Medium,
            High
        }
        public EnumPriority? Priority { get; set; }

        public bool? Done { get; set; }
    }
}