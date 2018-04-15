using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels.FirstCategory.Models;
using Com.Ericmas001.Windows.ViewModels;

namespace Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels.FirstCategory
{
    public class FirstCategoryMainTabViewModel : BaseContentViewModel
    {
        public FirstCategoryParms Parms { get; }
        protected override string NormalIconImageName => "First";
        public override string BigLoadingMessage => "Get Stuff";
        public override string SmallLoadingMessage => "Getting all the stuff...";

        public string SomeBool => Parms.SomeBool.ToString().ToUpper();
        public override string TabHeader { get; } = "First " + DateTime.Now.ToString("HH:mm:ss");
        public override bool CanCloseTab => true;
        public FirstCategoryMainTabViewModel(FirstCategoryParms parms)
            : base(Application.Current.Dispatcher)
        {
            Parms = parms;
            RefreshDataAndInterface();
        }

        protected override bool ObtainData(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(2000);
            return true;
        }
    }
}
