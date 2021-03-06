﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using PMSCommon;

namespace PMSClient.ViewModel
{
    public class RecordDeMoldSelectVM : BaseViewModelSelect
    {
        public RecordDeMoldSelectVM()
        {
            searchVHPPlanLot = searchComposition = "";
            PageChanged = new RelayCommand(ActionPaging);

            RecordDeMolds = new ObservableCollection<DcRecordDeMold>();
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            SetPageParametersWhenConditionChange();
            GiveUp = new RelayCommand(GoBack);
            Select = new RelayCommand<DcRecordDeMold>(ActionSelect);
        }

        private void ActionSelect(DcRecordDeMold model)
        {
            if (model != null)
            {
                switch (requestView)
                {
                    case PMSViews.RecordMachineEdit:
                        PMSHelper.ViewModels.RecordMachineEdit.SetBySelect(model);
                        break;
                    default:
                        break;
                }
                GoBack();
            }
        }

        private PMSViews requestView;
        public void SetRequestView(PMSViews view)
        {
            requestView = view;
        }

        private void GoBack()
        {
            NavigationService.GoTo(requestView);
        }


        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }
        private void ActionAll()
        {
            SearchVHPPlanLot = SearchComposition = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }


        private static void GoToEditView()
        {
            NavigationService.GoTo(PMSViews.RecordDeMoldEdit);
        }

        private void ActionDuplicate(DcRecordDeMold model)
        {
            if (PMSDialogService.ShowYesNo("请问", "确定复用这条记录？"))
            {
                PMSHelper.ViewModels.RecordDeMoldEdit.SetByDuplicate(model);
                GoToEditView();
            }
        }
        private void ActionEdit(DcRecordDeMold model)
        {
            PMSHelper.ViewModels.RecordDeMoldEdit.SetEdit(model);
            GoToEditView();
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.RecordDeMoldEdit.SetNew();
            GoToEditView();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            var service = new RecordDeMoldServiceClient();
            RecordCount = service.GetRecordDeMoldsCountByVHPPlanLot(SearchVHPPlanLot, SearchComposition);
            service.Close();
            ActionPaging();
        }
        private void ActionPaging()
        {

            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new RecordDeMoldServiceClient();
            var models = service.GetRecordDeMoldsByVHPPlanLot(skip, take, SearchVHPPlanLot, SearchComposition);
            service.Close();
            RecordDeMolds.Clear();
            models.ToList().ForEach(o => RecordDeMolds.Add(o));
        }


        private string searchVHPPlanLot;
        public string SearchVHPPlanLot
        {
            get { return searchVHPPlanLot; }
            set { searchVHPPlanLot = value; RaisePropertyChanged(nameof(SearchVHPPlanLot)); }
        }
        private string searchComposition;
        public string SearchComposition
        {
            get { return searchComposition; }
            set { searchComposition = value; RaisePropertyChanged(nameof(SearchComposition)); }
        }
        public ObservableCollection<DcRecordDeMold> RecordDeMolds { get; set; }

        public RelayCommand<DcRecordDeMold> Select { get; set; }
    }
}
