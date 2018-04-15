using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using Com.Ericmas001.Windows.Demo.TabControlApp.Services.Interfaces;
using Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels.FirstCategory.Models;
using Com.Ericmas001.Windows.ViewModels;

namespace Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels.FirstCategory
{
    public class FirstCategoryMainTabViewModel : BaseContentViewModel, IMainTabViewModel<FirstCategoryParms>
    {
        private readonly IFieldHelperService m_FieldHelperService;
        public FirstCategoryParms Parms { get; set; }
        protected override string NormalIconImageName => "First";
        public override string BigLoadingMessage => "Get Stuff";
        public override string SmallLoadingMessage => "Getting all the stuff...";

        public string SomeText => Parms.SomeText + " " + m_FieldHelperService.SomeTextHelp();
        public string SomeBool => Parms.SomeBool.ToString().ToUpper() + " " + m_FieldHelperService.SomeBoolHelp();
        public override string TabHeader { get; } = "First " + DateTime.Now.ToString("HH:mm:ss");
        public override bool CanCloseTab => true;
        public FirstCategoryMainTabViewModel(IFieldHelperService fieldHelperService)
            : base(Application.Current.Dispatcher)
        {
            m_FieldHelperService = fieldHelperService;
            RefreshDataAndInterface();
        }

        protected override bool ObtainData(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(2000);
            return true;
        }
    }
}
