namespace Oxygen.Utils
{
    public static class Log
    {
        public static void Debug(object msg)
        {
            Plugin.log.LogDebug(msg);
        }

        public static void Error(object msg)
        {
            Plugin.log.LogError(msg);
        }

        public static void Warning(object msg)
        {
            Plugin.log.LogWarning(msg);
        }

        public static void Message(object msg)
        {
            Plugin.log.LogMessage(msg);
        }
    }
}