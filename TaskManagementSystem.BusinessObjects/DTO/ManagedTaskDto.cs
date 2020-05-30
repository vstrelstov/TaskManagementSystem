using System;
using System.Collections.Generic;
using TaskManagementSystem.Infrastructure.Entities;

namespace TaskManagementSystem.BusinessObjects.DTO
{ 
    public class ManagedTaskDto
    {
       public Guid Id { get; set; }

        public string Name { get; set; }

        public ManagedTaskState State { get; set; }

        public string Description { get; set; }

        public string Executors { get; set; }

        public long PlannedCompletionHours { get; set; }

        public long ActualCompletionHours { get; set; }

        public DateTime DateOfIssue { get; set; }

        public DateTime? DateOfCompletion { get; set; }

        public ManagedTaskDto ParentTask { get; set; }

        public ICollection<ManagedTaskDto> SubTasks { get; set; }
    }
}
