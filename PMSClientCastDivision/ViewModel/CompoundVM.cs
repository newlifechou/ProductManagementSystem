﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;
using System.Collections.ObjectModel;
using PMSClient.BasicService;

namespace PMSClient.ViewModel
{
    public class CompoundVM : BaseViewModelPage
    {
        public CompoundVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            searchComposition = "";
            Compounds = new ObservableCollection<DcBDCompound>();
        }
        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);

            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcBDCompound>(ActionEdit, CanEdit);

        }


        //用于任务定位调用
        public void SetSearchCondition(string composition, string pminumber)
        {
            SearchComposition = composition;
            //需要重新激发一下
            RaisePropertyChanged(nameof(SearchComposition));

            SetPageParametersWhenConditionChange();
        }


        private void ActionOnlyUnCompleted()
        {

        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialInventoryIn);
        }

        private bool CanEdit(DcBDCompound arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialInventoryIn);
        }

        private void ActionEdit(DcBDCompound model)
        {
            if (model != null)
            {

            }
        }

        private void ActionAdd()
        {

        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchComposition));
        }

        private void ActionAll()
        {
            SearchComposition = "";
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
            var service = new CompoundServiceClient();
            RecordCount = service.GetCompoundCount(SearchComposition);
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
            var service = new CompoundServiceClient();
            var result = service.GetCompound(skip, take, SearchComposition);
            service.Close();
            Compounds.Clear();
            result.ToList().ForEach(o => Compounds.Add(o));
        }

        #region Proeperties
        private string searchComposition;
        public string SearchComposition
        {
            get { return searchComposition; }
            set
            {
                if (searchComposition == value)
                    return;
                searchComposition = value;
                RaisePropertyChanged(() => SearchComposition);
            }
        }



        private ObservableCollection<DcBDCompound> materialInventoryIns;
        public ObservableCollection<DcBDCompound> Compounds
        {
            get { return materialInventoryIns; }
            set { materialInventoryIns = value; RaisePropertyChanged(nameof(Compounds)); }
        }

        #endregion

        #region Commands
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcBDCompound> Edit { get; private set; }
        #endregion



    }
}
