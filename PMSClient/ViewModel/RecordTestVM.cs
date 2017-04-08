﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;

namespace PMSClient.ViewModel
{
    public class RecordTestVM : BaseViewModelPage
    {
        public RecordTestVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void ActionRefresh(MsgObject obj)
        {
            SetPageParametersWhenConditionChange();
        }
        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
            base.Cleanup();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Add = new RelayCommand(ActionAdd);
            Edit = new RelayCommand<DcRecordTest>(ActionEdit);
            Doc = new RelayCommand<DcRecordTest>(ActionDoc);
            SelectionChanged = new RelayCommand<DcRecordTest>(ActionSelectionChanged);
            Duplicate = new RelayCommand<DcRecordTest>(ActionDuplicate);
        }

        private void ActionSelectionChanged(DcRecordTest model)
        {
            CurrentSelectItem = model;
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchProductID) && string.IsNullOrEmpty(SearchCompositonStd));
        }

        private void ActionAll()
        {
            SearchProductID = SearchCompositonStd = "";
            ActionPaging();
        }

        private void ActionSearch()
        {
            ActionPaging();
        }

        private void ActionDuplicate(DcRecordTest model)
        {
            PMSHelper.ViewModels.RecordTestEdit.SetNew(model);
            NavigationService.GoTo(PMSViews.RecordTestEdit);
        }

        private void ActionEdit(DcRecordTest model)
        {
            PMSHelper.ViewModels.RecordTestEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.RecordTestEdit);
        }

        private void ActionDoc(DcRecordTest model)
        {
            //TODO:这里添加生成doc的代码
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.RecordTestEdit.SetNew();
            NavigationService.GoTo(PMSViews.RecordTestEdit);
        }

        private void InitializeProperties()
        {
            RecordProducts = new ObservableCollection<DcRecordTest>();
            SearchCompositonStd = searchProductID = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new RecordTestServiceClient();
            RecordCount = service.GetRecordTestCountBySearchInPage(SearchProductID, SearchCompositonStd);
            ActionPaging();
        }
        private void ActionPaging()
        {
            var service = new RecordTestServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetRecordTestBySearchInPage(skip, take, SearchProductID, SearchCompositonStd);
            RecordProducts.Clear();
            orders.ToList<DcRecordTest>().ForEach(o => RecordProducts.Add(o));

            CurrentSelectItem = RecordProducts.FirstOrDefault();
        }
        #region Commands
        public RelayCommand Report { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordTest> Edit { get; set; }
        public RelayCommand<DcRecordTest> Doc { get; set; }

        private string searchProductID;
        public string SearchProductID
        {
            get { return searchProductID; }
            set
            {
                if (searchProductID == value)
                    return;
                searchProductID = value;
                RaisePropertyChanged(() => SearchProductID);
            }
        }
        private string searchCompositionStd;
        public string SearchCompositonStd
        {
            get { return searchCompositionStd; }
            set
            {
                if (searchCompositionStd == value)
                    return;
                searchCompositionStd = value;
                RaisePropertyChanged(() => SearchCompositonStd);
            }
        }

        public ObservableCollection<DcRecordTest> RecordProducts { get; set; }
        private DcRecordTest currentSelectItem;

        public DcRecordTest CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }

        public RelayCommand<DcRecordTest> SelectionChanged { get; set; }
        public RelayCommand<DcRecordTest> Duplicate { get; set; }
        #endregion
    }
}
