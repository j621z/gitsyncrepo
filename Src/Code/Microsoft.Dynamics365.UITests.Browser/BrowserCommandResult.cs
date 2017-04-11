using System;

namespace Microsoft.Dynamics365.UITests.Browser
{
    public class BrowserCommandResult<TReturn>
    {
        internal BrowserCommandResult()
        {
        }

        public BrowserCommandResult(TReturn value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the number of executions of the command that occurred.
        /// </summary>
        public int ExecutionAttempts { get; internal set; }

        /// <summary>
        /// Gets the name of the command that was executed.
        /// </summary>
        public string CommandName { get; internal set; }

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
        /// Gets the result value of the command.  Implicitly can be converted.
        /// </summary>
        public TReturn Value { get; internal set; }

        /// <summary>
        /// Gets the start time when the command was executed.
        /// </summary>
        public DateTime? StartTime { get; private set; }

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

		#region Implicit conversion to/from TReturn

		public static implicit operator TReturn(BrowserCommandResult<TReturn> result)
        {
            return result.Value;
        }

        public static implicit operator BrowserCommandResult<TReturn>(TReturn result)
        {
            return new BrowserCommandResult<TReturn>(result);
        }

		#endregion
	}
}