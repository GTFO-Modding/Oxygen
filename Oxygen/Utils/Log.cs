﻿using BepInEx.Logging;

namespace Oxygen.Utils
{
    static class Log
    {
        static Log() { }

        private static readonly ManualLogSource source = new(Plugin.MODNAME);
        public static void Debug(object msg) => source.LogDebug(msg);
        public static void Error(object msg) => source.LogError(msg);
        public static void Fatal(object msg) => source.LogFatal(msg);
        public static void Info(object msg) => source.LogInfo(msg);
        public static void Message(object msg) => source.LogMessage(msg);
        public static void Warning(object msg) => source.LogWarning(msg);
    }
}