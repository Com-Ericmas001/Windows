using Com.Ericmas001.Windows.ViewModels;

namespace Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels
{
    public class MainViewModel : BaseMainWindowViewModel
    {
        protected override bool KeepNewTab => true;

        public override string Title => "Demo TabControlApp";

        protected override NewTabViewModel CreateNewTab()
        {
            return new NewTaskViewModel();
        }
    }
}
