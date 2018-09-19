using System;
using System.Text;

namespace DepotDownloader
{
    internal static class Logger
    {
        static StringBuilder  _sBuilder = new StringBuilder(128);
        static DownloadConfig _Config;

        public static void Log(string line, params object[] args)
        {
            DownloadConfig.FireOnMessageEvent("log", Format(line, args));
        }
        
        public static void Warning(string line, params object[] args)
        {
            DownloadConfig.FireOnMessageEvent("warning", Format(line, args));
        }
        
        public static void Error(string line, params object[] args)
        {
            DownloadConfig.FireOnMessageEvent("error", Format(line, args));
        }
        
        public static void Exception(Exception exception)
        {
            DownloadConfig.FireOnMessageEvent("exception", exception);
        }

        internal static void SetConfig(DownloadConfig config)
        {
            Logger._Config = config;
        }

        public static string Format(string line, params object[] args)
        {
            _sBuilder.Append(line);
            for (var i = 0; i < args.Length; i++)
            {
                _sBuilder.Replace($"{{{i}}}", $"{args[i]}");
            }
            
            line = _sBuilder.ToString();
            _sBuilder.Clear();
            return line;
        }
    }
}