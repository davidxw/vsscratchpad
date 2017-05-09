using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scratchpad
{
    public interface IContentManager
    {
        void Init();
        void Save(string currentContent);
        string Load();
        event EventHandler ContentChanged;
    }

    public class ContentChangedEventArgs : EventArgs
    {
        public string NewContent { get; set; }
    }

}
