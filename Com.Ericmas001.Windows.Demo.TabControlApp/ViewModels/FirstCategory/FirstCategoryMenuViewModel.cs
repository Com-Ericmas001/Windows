﻿using System.Linq;
using System.Windows.Input;
using Com.Ericmas001.Windows.Demo.TabControlApp.Services.Interfaces;
using Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels.FirstCategory.Models;
using Com.Ericmas001.Windows.Util;
using Com.Ericmas001.Windows.ViewModels;
using Com.Ericmas001.Windows.ViewModels.Sections;
using GalaSoft.MvvmLight.CommandWpf;

namespace Com.Ericmas001.Windows.Demo.TabControlApp.ViewModels.FirstCategory
{
    public class FirstCategoryMenuViewModel : TabSection
    {
        private readonly IFieldHelperService m_FieldHelperService;
        public HistoricItemsViewModel SomeTextVm { get; }
        private bool m_SomeBool;
        public bool SomeBool
        {
            get => m_SomeBool;
            set => Set(ref m_SomeBool, value);
        }

        public string SomeTextHelp => m_FieldHelperService.SomeTextHelp();
        public string SomeBoolHelp => m_FieldHelperService.SomeBoolHelp();

        private RelayCommand m_OkCommand;
        public ICommand OkCommand => m_OkCommand ?? (m_OkCommand = new RelayCommand(OnOkCommand, ValidateOkCommand));
        public FirstCategoryMenuViewModel(IFieldHelperService fieldHelperService)
        {
            m_FieldHelperService = fieldHelperService;
            var settings = SettingFile<FirstCategorySettings>.Load();
            SomeTextVm = new HistoricItemsViewModel(settings.SomeTextHistory);
            SomeBool = settings.SomeBool;
        }
        private bool ValidateOkCommand()
        {
            return true;
        }

        private void OnOkCommand()
        {
            SomeTextVm.AddCurrentItem();
            SettingFile<FirstCategorySettings>.Save(new FirstCategorySettings
            {
                SomeBool = SomeBool,
                SomeTextHistory = SomeTextVm.AllItems.ToArray()
            });
            CreateNewTab<FirstCategoryMainTabViewModel, FirstCategoryParms>(new FirstCategoryParms
            {
                SomeBool = SomeBool,
                SomeText = SomeTextVm.CurrentItem
            });
        }
    }
}
