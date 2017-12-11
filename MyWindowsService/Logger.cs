using System;
using System.IO;
using System.Threading;

namespace MyWindowsService
{
    class Logger
    {
        FileSystemWatcher _fileWatcher;
        object obj = new object();
        bool enabled = true;
        PurchaseBook _book = new PurchaseBook();
        public Logger()
        {
            _fileWatcher = new FileSystemWatcher
            {
                Path = @"D:\\Temp",
                Filter = "*.txt",
                NotifyFilter = NotifyFilters.FileName
            };

            _fileWatcher.Created += FileWatcherCreated;
            _fileWatcher.Changed += FileWatcherChanged;
            _fileWatcher.Renamed += FileWatcherRenamed;
        }

        public void Start()
        {
            _fileWatcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            _fileWatcher.EnableRaisingEvents = false;
            enabled = false;
            Dispose();
        }
        // переименование файлов
        private void FileWatcherRenamed(object sender, RenamedEventArgs e)
        {
            StartParsing(sender, e);
            string fileEvent = "переименован в " + e.FullPath;
            string filePath = e.OldFullPath;
            RecordEntry(fileEvent, filePath);
        }
        // изменение файлов
        private void FileWatcherChanged(object sender, FileSystemEventArgs e)
        {
            StartParsing(sender, e);
            string fileEvent = "изменен";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }
        // создание файлов
        private void FileWatcherCreated(object sender, FileSystemEventArgs e)
        {
            StartParsing(sender,e);
            string fileEvent = "создан";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (obj)
            {
                using (StreamWriter writer = new StreamWriter("D:\\templog.txt", true))
                {
                    writer.WriteLine($"{DateTime.Now:dd/MM/yyyy hh:mm:ss} файл {filePath} был {fileEvent}");
                    writer.Flush();
                }
            }
        }

        public void StartParsing(object sender, FileSystemEventArgs e)
        {
            var path = @"D:\\Temp";
            _book.SaveReports(path);
        }

        public void Dispose()
        {
            _fileWatcher.Dispose();
        }


    }
}
