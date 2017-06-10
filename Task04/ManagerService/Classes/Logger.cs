using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ManagerService.Classes
{
    public class Logger
    {
        private readonly FileSystemWatcher _watcher;
        private object _obj = new object();
        private bool _enabled = true;
        private CsvDbRecorder _dbRecorder;

        public Logger()
        {
            _watcher = new FileSystemWatcher(ConfigurationManager.AppSettings["ServerPath"]);
            _dbRecorder = new CsvDbRecorder("DefaultConnection");

            _watcher.Created += WatcherOnCreated;
            _watcher.Changed += WatcherOnChanged;
        }

        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
            while (_enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
            _enabled = false;
        }

        private void WatcherOnCreated(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            RecordEntry("created ", fileSystemEventArgs.FullPath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (_obj)
            {
                using (var writer = new StreamWriter(ConfigurationManager.AppSettings["LogPath"], true))
                {
                    writer.WriteLine($"{filePath} was {fileEvent}");
                    writer.Flush();
                }
            }
        }

        private void WatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            RecordEntry("changed ", fileSystemEventArgs.FullPath);

            try
            {
                var task = new Task(() =>
                {
                    _dbRecorder.WriteDataToDataBaseFromFile(fileSystemEventArgs.FullPath);
                    //Delete file after recording to database, write to log about success
                    File.Delete(fileSystemEventArgs.FullPath);
                    RecordEntry("successfully proceeded ", fileSystemEventArgs.FullPath);
                });
                task.Start();
                task.Wait();
            }
            catch (ThreadInterruptedException)
            {
                //If something goes wrong move file from source path to directory
                //with failed files, write to log about success

                //var sourceFullName = fileSystemEventArgs.FullPath;
                //var destinationPath = ConfigurationManager.AppSettings["FailedFilesStorage"];
                //var destinationFullName = destinationPath + Path.GetFileName(sourceFullName);

                //RecordEntry("error occured when proceeded ", fileSystemEventArgs.FullPath);
                //if (!Directory.Exists(destinationPath))
                //{
                //    Directory.CreateDirectory(ConfigurationManager.AppSettings["FailedFilesStorage"]);
                //}

                //if (File.Exists(destinationFullName))
                //{
                //    File.Delete(destinationFullName);
                //}
                //File.Move(sourceFullName, destinationFullName);
            }
        }
    }
}
