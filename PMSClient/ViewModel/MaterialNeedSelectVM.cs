﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using PMSClient.ViewModel.Model;

namespace PMSClient.ViewModel
{
    public class MaterialNeedSelectVM : BaseViewModelSelect
    {

        public MaterialNeedSelectVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private PMSViews requestView;
        public void SetRequestView(PMSViews viewToken)
        {
            requestView = viewToken;
        }

        private void InitializeProperties()
        {
            SearchCompositoinStandard = SearchPMINumber = "";
            MainMaterialNeedExtras = new ObservableCollection<MaterialNeedExtra>();
        }
        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(requestView));
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            Select = new RelayCommand<MaterialNeedExtra>(ActionSelect);
            SelectBatch = new RelayCommand(ActionSelectBatch);
        }

        private void ActionSelectBatch()
        {
            var count = MainMaterialNeedExtras.Where(i => i.IsSelected == true).Count();
            if (!PMSDialogService.ShowYesNo("请问", $"请问要批量添加选定的{count}记录吗？"))
            {
                return;
            }
            try
            {
                switch (requestView)
                {
                    case PMSViews.MaterialOrderItemEdit:
                        var currentMaterialOrder = PMSHelper.ViewModels.MaterialOrderItemEdit.CurrentMaterialOrder;
                        BatchSaveMaterialOrderItem(currentMaterialOrder);
                        NavigationService.GoTo(PMSViews.MaterialOrder);
                        PMSDialogService.Show("成功", "批量添加成功，请刷新列表,并修改材料批号\r\n因为批量添加的是相同的材料批号");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void BatchSaveMaterialOrderItem(DcMaterialOrder curentMaterialOrder)
        {
            using (var service = new MaterialOrderServiceClient())
            {
                int index = 1;
                MainMaterialNeedExtras.ToList().ForEach(i =>
                {
                    if (i.IsSelected)
                    {
                        var temp = PMSNewModelCollection.NewMaterialOrderItem(curentMaterialOrder);
                        temp.OrderItemNumber = DateTime.Now.ToString("yyMMdd") + curentMaterialOrder.SupplierAbbr + index.ToString();
                        index++;
                        temp.Composition = i.MaterialNeed.Composition;
                        temp.PMINumber = i.MaterialNeed.PMINumber;
                        temp.Weight = i.MaterialNeed.Weight;
                        service.AddMaterialOrderItemByUID(temp, PMSHelper.CurrentSession.CurrentUser.UserName);
                    }
                });
            }
        }

        private void ActionSelect(MaterialNeedExtra need)
        {
            if (need != null)
            {
                switch (requestView)
                {
                    case PMSViews.MaterialOrderItemEdit:
                        PMSHelper.ViewModels.MaterialOrderItemEdit.SetBySelect(need.MaterialNeed);
                        break;
                    default:
                        break;
                }
                NavigationService.GoTo(requestView);
            }
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchCompositoinStandard) && string.IsNullOrEmpty(SearchPMINumber));
        }

        private void ActionAll()
        {
            SearchCompositoinStandard = SearchPMINumber = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            var service = new MaterialNeedServiceClient();
            RecordCount = service.GetMaterialNeedCountBySearch(SearchCompositoinStandard, SearchPMINumber);
            service.Close();
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new MaterialNeedServiceClient();
            var result = service.GetMaterialNeedBySearchInPage(skip, take, SearchCompositoinStandard, SearchPMINumber);
            service.Close();
            MainMaterialNeedExtras.Clear();
            result.ToList().ForEach(o => MainMaterialNeedExtras.Add(new MaterialNeedExtra { MaterialNeed = o }));
        }


        #region Proeperties
        private string searchCompositionStandard;
        public string SearchCompositoinStandard
        {
            get { return searchCompositionStandard; }
            set
            {
                if (searchCompositionStandard == value)
                    return;
                searchCompositionStandard = value;
                RaisePropertyChanged(() => SearchCompositoinStandard);
            }
        }

        private string searchPMINumber;
        public string SearchPMINumber
        {
            get { return searchPMINumber; }
            set
            {
                if (searchPMINumber == value)
                    return;
                searchPMINumber = value;
                RaisePropertyChanged(nameof(SearchPMINumber));
            }
        }
        private ObservableCollection<MaterialNeedExtra> mainMaterialNeeds;
        public ObservableCollection<MaterialNeedExtra> MainMaterialNeedExtras
        {
            get { return mainMaterialNeeds; }
            set { mainMaterialNeeds = value; RaisePropertyChanged(nameof(MainMaterialNeedExtras)); }
        }

        #endregion

        #region Commands
        public RelayCommand<MaterialNeedExtra> Select { get; private set; }
        public RelayCommand SelectBatch { get; set; }
        #endregion



    }
}
