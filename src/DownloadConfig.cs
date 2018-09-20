using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DepotDownloader
{
    public class DownloadConfig
    {
        public int    CellID { get; set; }
        public bool   DownloadAllPlatforms { get; set; }
        public bool   DownloadManifestOnly { get; set; }
        public string InstallDirectory { get; set; }

        public bool         UsingFileList        { get; set; } = false;
        public List<string> FilesToDownload      { get; set; } = new List<string>();
        public List<Regex>  FilesToDownloadRegex { get; set; } = new List<Regex>();


        public string BetaPassword { get; set; }

        public ulong  ManifestId   { get; set; } = ContentDownloader.INVALID_MANIFEST_ID;

        public bool   VerifyAll    { get; set; }

        public int MaxServers   { get; set; } = 8;
        public int MaxDownloads { get; set; } = 4;


        public string Username   { get; set; } = null;
        public string Password   { get; set; } = null;
        public string SuppliedPassword { get; set; }
        public bool   RememberPassword { get; set; } = false;

        public uint   AppID      { get; set; } = ContentDownloader.INVALID_APP_ID;
        public string Branch     { get; set; } = "public";
        public string OS         { get; set; } = null;
        
        public bool   ForceDepot { get; set; } = false;
        public uint   DepotID    { get; set; } = ContentDownloader.INVALID_DEPOT_ID;

        /// <summary>
        /// Call when downloading progress changed
        /// </summary>
        public event Action<string> OnReportProgressEvent;
        
        /// <summary>
        /// 1 - type    (log, warning, error, exception)
        /// 2 - message (exception - Exception object, in other string)
        /// </summary>
        public event Action<string, object> OnMessageEvent;

        /// <summary>
        /// 1 - install directory
        /// 2 - full path
        /// </summary>
        public Func<string, string> SavePathProcessor;

        internal void FireReportProgressEvent(string message)
        {
            OnReportProgressEvent?.Invoke(message);
        }
        
        internal void FireOnMessageEvent(string type, object message)
        {
            OnMessageEvent?.Invoke(type, message);
        }
    }
}
