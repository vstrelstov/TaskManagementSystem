using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagementSystem.Infrastructure.Entities
{
    public enum ManagedTaskState
    {
        Assigned,
        Executing,
        Paused,
        Completed
    }
}
