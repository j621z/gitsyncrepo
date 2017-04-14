using System;

namespace Microsoft.Dynamics365.UITests.Browser
{
    public interface ICommandResult
    {
        string CommandName { get; }
        
        int ExecutionAttempts { get; }
        
        DateTime? StartTime { get;  }
        
        Exception Exception { get; }
        
        bool? Success { get; }
        
        DateTime? StopTime { get; }
        
        int ExecutionTime { get; }
        
        int TransitionTime { get; set; }
        
        int ThinkTime { get; set; }
    }
}