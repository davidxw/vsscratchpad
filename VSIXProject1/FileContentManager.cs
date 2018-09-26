using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scratchpad
{
    /// <summary>
    /// Assumes that as the user can't type in two places at once, changes made to the UI will overwrite the content file.
    /// These changes will be picked up by other VS instances.
    /// </summary>
    public sealed class FileContentManager : IContentManager
    {
        private static readonly FileContentManager instance = new FileContentManager();

        private FileContentManager() { }

        private string fileSavePath;
        private FileSystemWatcher watcher;
        private string currentContent = null;

        public event EventHandler ContentChanged;

        public static FileContentManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void Init()
        {
            var fileSaveLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            fileSavePath = System.IO.Path.Combine(fileSaveLocation, Constants.FileName);

            if (!System.IO.File.Exists(fileSavePath))
            {
                System.IO.File.Create(fileSavePath);
            }

            watcher = new FileSystemWatcher(fileSaveLocation);
            watcher.Filter = Constants.FileName;
            watcher.Changed += Watcher_Changed;

            watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            RefreshCurrentContentFromFile();

            ContentChanged(this, new ContentChangedEventArgs { NewContent = currentContent });
        }

        public void Save(string newContent)
        {
            if (newContent == currentContent)
                return;

            watcher.EnableRaisingEvents = false;

            System.IO.File.WriteAllText(fileSavePath, newContent);

            watcher.EnableRaisingEvents = true;

            currentContent = newContent;
        }

        public string Load()
        {
            RefreshCurrentContentFromFile();

            return currentContent;
        }

        private void RefreshCurrentContentFromFile()
        {
            Thread.Sleep(250);

            try
            {
                if (!File.Exists(fileSavePath))
                {
                    return;
                }

                var fileContent = System.IO.File.ReadAllText(fileSavePath);

                currentContent = fileContent;
            }
            catch (System.IO.IOException ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
    }

}
