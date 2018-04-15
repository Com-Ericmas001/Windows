using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Windows.ViewModels
{
    public interface IMainTabViewModel<T>
    {
        T Parms { get; set; }
    }
}
