using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.Infrastructure.Entities
{
    [Table("ManagedTask")]
    public class ManagedTask
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ManagedTaskState State { get; set; } = ManagedTaskState.Assigned;

        public string Description { get; set; }

        public string Executors { get; set; }

        public long PlannedCompletionHours { get; set; }

        public long ActualCompletionHours { get; set; }

        public DateTime DateOfIssue { get; set; }

        public DateTime? DateOfCompletion { get; set; }

        public ManagedTask ParentTask { get; set; }

        public ICollection<ManagedTask> SubTasks { get; set; }
    }
}
