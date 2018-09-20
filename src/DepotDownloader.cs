using System.IO;
using System.Threading.Tasks;

namespace DepotDownloader
{
    public class DepotDownloader
    {
        private DownloadConfig _config;

        public DepotDownloader(DownloadConfig config)
        {
            this._config = config;
            ContentDownloader.Config = config;
            Logger.SetConfig(config);
            ContentDownloader.InitializeSteam3();
        }

        public void Download(bool wait = false)
        {
            ConfigStore.LoadFromFile(Path.Combine( Directory.GetCurrentDirectory(), "DepotDownloader.config" ));
            var task1 = ContentDownloader.DownloadAppAsync();
            var task = Task.Run(async () => await task1);
            if (wait)
            {
                task.GetAwaiter().GetResult();
            }
            ContentDownloader.ShutdownSteam3();
        }

        public void ClearCache()
        {
            ContentDownloader.ClearCache();
        }
    }
}