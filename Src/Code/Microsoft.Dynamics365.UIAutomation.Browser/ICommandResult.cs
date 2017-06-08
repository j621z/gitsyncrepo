using System;

namespace Microsoft.Dynamics365.UIAutomation.Browser
{
    public interface ICommandResult
    {
        string CommandName { get; }
        
        int ExecutionAttempts { get; }
        
        DateTime? StartTime { get;  }
        
        Exception Exception { get; }
        
        bool? Success { get; }
        
        DateTime? StopTime { get; }
        
        double ExecutionTime { get; }

        double TransitionTime { get; set; }

        double ThinkTime { get; set; }

        int Depth { get; set; }
    }
}