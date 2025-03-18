namespace Eventify.Backend.Common.Logging
{
    public static class Logger
    {
        // Extension to log information messages
        public static void LogInformation(this ILogger logger, string message)
        {
            logger.LogInformation(message);
        }

        // Extension to log warning messages
        public static void LogWarning(this ILogger logger, string message)
        {
            logger.LogWarning(message);
        }

        // Extension to log error messages
        public static void LogError(this ILogger logger, string message, Exception exception = null)
        {
            if (exception != null)
            {
                logger.LogError(exception, message);
            }
            else
            {
                logger.LogError(message);
            }
        }
    }
}
