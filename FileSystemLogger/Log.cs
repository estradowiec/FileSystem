

namespace FileSystemLogger
{
    using System;
    using NLog;

    /// <summary>
    /// The log.
    /// </summary>
    public static class Log
    {
        #region Members

        /// <summary>
        /// The log.
        /// </summary>
        private static readonly Logger Logs = LogManager.GetCurrentClassLogger();
        #endregion

        #region Info
        /// <summary>
        /// The info.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public static void Info(string message)
        {
            Logs.Info(message);
        }

        /// <summary>
        /// The info.
        /// </summary>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Info(string format, params object[] args)
        {
            Logs.Info(format, args);
        }
        #endregion

        #region Debug
        /// <summary>
        /// The debug.
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public static void Debug(Exception ex)
        {
            Logs.Debug(ex);
        }

        /// <summary>
        /// The debug.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public static void Debug(string message)
        {
            Logs.Debug(message);
        }

        /// <summary>
        /// The debug.
        /// </summary>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public static void Debug(string errorMessage, Exception ex)
        {
            Logs.Debug(errorMessage, ex);
        }

        /// <summary>
        /// The debug.
        /// </summary>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Debug(string format, params object[] args)
        {
            Logs.Debug(format, args);
        }
        #endregion

        #region Error
        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public static void Error(Exception ex)
        {
            Logs.Error(ex);
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public static void Error(string message)
        {
            Logs.Error(message);
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public static void Error(string errorMessage, Exception ex)
        {
            Logs.Error(errorMessage, ex);
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Error(string format, params object[] args)
        {
            Logs.Error(format, args);
        }
        #endregion
    }
}
