using System;

namespace Microsoft.Dynamics365.UITests.Browser
{
    public class ExecutionResult
    {
        /// <summary>
        /// Gets the name of the command that was executed.
        /// </summary>
        public string CommandName { get; internal set; }
        /// <summary>
        /// Gets the number of executions of the command that occurred.
        /// </summary>
        public int ExecutionAttempts { get; internal set; }

        /// <summary>
        /// Gets the start time when the command was executed.
        /// </summary>
        public DateTime? StartTime { get; private set; }
        
        /// <summary>
        /// Gets the last exception thrown by the command.
        /// </summary>
        public Exception Exception { get; internal set; }

        /// <summary>
        /// Gets a boolean indicating if the command was successful or not.  Default value
        /// is null and will represent an unprocessed command.
        /// </summary>
        public bool? Success { get; internal set; }

        /// <summary>
        /// Gets the end time when the command completed, including any retries.
        /// </summary>
        public DateTime? StopTime { get; private set; }

        /// <summary>
        /// Gets the total execution time of the command.
        /// </summary>
        public TimeSpan ExecutionTime
        {
            get
            {
                if (this.StartTime.HasValue && this.StopTime.HasValue)
                {
                    return this.StopTime.Value - this.StartTime.Value;
                }

                return TimeSpan.Zero;
            }
        }

        public TimeSpan TransitionTime = TimeSpan.Zero;

        public int ThinkTime = 0;

        internal void Start()
        {
            this.StartTime = DateTime.Now;
        }

        internal void Stop()
        {
            if (!this.StartTime.HasValue)
                throw new InvalidOperationException("Stop cannot be invoked because Start was never called.");

            this.StopTime = DateTime.Now;
        }
    }
}
