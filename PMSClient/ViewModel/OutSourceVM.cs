﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class OutSourceVM : BaseViewModelPage
    {
        public OutSourceVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }


        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcOutSource>(ActionEdit, CanEdit);
            Duplicate = new RelayCommand<DcOutSource>(ActionDuplicate, CanDuplicate);
        }
        private bool CanDuplicate(DcOutSource arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditOutSource);
        }

        private void ActionDuplicate(DcOutSource model)
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定复用这条记录吗？"))
            {
                return;
            }
            PMSHelper.ViewModels.OutSourceEdit.SetDuplicate(model);
            NavigationService.GoTo(PMSViews.OutSourceEdit);
        }

        private bool CanEdit(DcOutSource arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditOutSource);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditOutSource);
        }


        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchOrderLot) && string.IsNullOrEmpty(SearchOrderName) && string.IsNullOrEmpty(SearchSupplier));
        }

        private void ActionAll()
        {
            searchOrderLot = searchOrderName = searchSupplier = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionEdit(DcOutSource model)
        {
            PMSHelper.ViewModels.OutSourceEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.OutSourceEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.OutSourceEdit.SetNew();
            NavigationService.GoTo(PMSViews.OutSourceEdit);
        }

        private void InitializeProperties()
        {
            OutSources = new ObservableCollection<DcOutSource>();
            searchOrderLot = searchOrderName = searchSupplier = "";

        }
        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new OutSourceServiceClient())
            {
                RecordCount = service.GetOutSourcesCount(SearchOrderLot, SearchOrderName, SearchSupplier);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new OutSourceServiceClient())
            {
                var orders = service.GetOutSources(skip, take, SearchOrderLot, SearchOrderName, SearchSupplier);
                OutSources.Clear();
                orders.ToList().ForEach(o => OutSources.Add(o));
            }
        }
        #region Commands
        public RelayCommand Add { get; set; }
        public RelayCommand<DcOutSource> Edit { get; set; }


        private string searchOrderLot;
        public string SearchOrderLot
        {
            get { return searchOrderLot; }
            set
            {
                if (searchOrderLot == value)
                    return;
                searchOrderLot = value;
                RaisePropertyChanged(() => SearchOrderLot);
            }
        }
        private string searchOrderName;
        public string SearchOrderName
        {
            get { return searchOrderName; }
            set
            {
                if (searchOrderName == value)
                    return;
                searchOrderName = value;
                RaisePropertyChanged(() => SearchOrderName);
            }
        }
        private string searchSupplier;
        public string SearchSupplier
        {
            get { return searchSupplier; }
            set
            {
                if (searchSupplier == value)
                    return;
                searchSupplier = value;
                RaisePropertyChanged(() => SearchSupplier);
            }
        }

        public ObservableCollection<DcOutSource> OutSources { get; set; }
        #endregion
        public RelayCommand<DcOutSource> Duplicate { get; set; }

    }
}
