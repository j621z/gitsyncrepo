using System;

namespace Microsoft.Dynamics365.UITests.Browser
{
    public class BrowserCommandResult<TReturn>
    {
        internal BrowserCommandResult()
        {
            Result = new ExecutionResult();
        }

        public BrowserCommandResult(TReturn value)
        {
            this.Value = value;
        }
       
        /// <summary>
        /// Gets the result value of the command.  Implicitly can be converted.
        /// </summary>
        public TReturn Value { get; internal set; }
        
        public ExecutionResult Result { get; internal set; }

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