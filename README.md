# DepotDownloader.Lib
This lib based on [SteamRE/DepotDownloader](https://github.com/SteamRE/DepotDownloader)
## Example
```
Regex _rustFilesRegex = new Regex("RustDedicated_Data\\\\Managed\\\\.*.dll");

var config = new DownloadConfig()
{
    AppID = 258550,
    DownloadAllPlatforms = false,
    InstallDirectory = ".temp",
    UsingFileList = true,
    FilesToDownloadRegex = new List<Regex>() { _rustFilesRegex },
};
var downloader = new global::DepotDownloader.DepotDownloader(config);
downloader.Download(true);

downloader.ClearCache();
```
