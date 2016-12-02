using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.Windows.Wpf
{
    public interface IWorkInBackground
    {
        string Summary { get; }
        void Work(BackgroundWorker worker, DoWorkEventArgs e);
    }
}
