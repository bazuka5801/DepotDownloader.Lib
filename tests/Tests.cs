using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using DepotDownloader;
using NUnit.Framework;

namespace DepotDonwloader.Lib.Tests
{
    [TestFixture]
    public class Tests
    {
        private readonly Regex _rustFilesRegex = new Regex("RustDedicated_Data\\\\Managed\\\\.*.dll");
        private DownloadConfig _config;
        private string _directory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(global::DepotDownloader.DepotDownloader)).Location);
        
        [Test]
        public void Test_RustDownloading()
        {
            Directory.SetCurrentDirectory(_directory);
            _config = new DownloadConfig()
            {
                AppID = 258550,
                DownloadAllPlatforms = false,
                InstallDirectory = ".temp",
                UsingFileList = true,
                FilesToDownloadRegex = new List<Regex>() { _rustFilesRegex },
                SavePathProcessor = SavePathProcessor,
            };
            var downloader = new global::DepotDownloader.DepotDownloader(_config);
            downloader.Download(true);
            
            downloader.ClearCache();
            Assert.IsFalse(Directory.Exists(".temp\\.DepotDownloader"));
            Directory.Delete(".temp", true);
        }
        
        [Test]
        public void Test_RustDownloadingWithLogger()
        {
            Directory.SetCurrentDirectory(_directory);
            _config = new DownloadConfig()
            {
                AppID = 258550,
                DownloadAllPlatforms = false,
                InstallDirectory = ".temp",
                UsingFileList = true,
                FilesToDownloadRegex = new List<Regex>() { _rustFilesRegex },
                SavePathProcessor = SavePathProcessor
            };
            _config.OnMessageEvent += (type, message) => Console.WriteLine($"[{type}] {message}");
            _config.OnReportProgressEvent += (message) => Console.WriteLine($"[Progress] {message}");
            var downloader = new global::DepotDownloader.DepotDownloader(_config);
            downloader.Download(true);
            
            downloader.ClearCache();
            Assert.IsFalse(Directory.Exists(".temp\\.DepotDownloader"));
            Directory.Delete(".temp", true);
        }

        private string SavePathProcessor(string path)
        {
            return Path.Combine(_config.InstallDirectory, Path.GetFileName(path));
        }
    }
}