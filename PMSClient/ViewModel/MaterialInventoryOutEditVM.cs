﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.MainService;
using System.Collections.ObjectModel;


namespace PMSClient.ViewModel
{
    public class MaterialInventoryOutEditVM : BaseViewModelEdit
    {
        public MaterialInventoryOutEditVM()
        {
            InitializeProperties();
            InitialCommands();
        }

        public void SetNew()
        {

            var empty = new DcMaterialNeed();
            empty.Id = Guid.NewGuid();
            empty.CreateTime = DateTime.Now;
            empty.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            empty.State = PMSCommon.SimpleState.UnDeleted.ToString();
            empty.Composition = "需求原料成分";
            empty.PMINumber = DateTime.Now.ToString("yyMMdd");
            empty.Purity = "5N";
            empty.Weight = 1;

            IsNew = true;
            CurrentMaterialNeed = empty;
        }

        public void SetEdit(DcMaterialNeed model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentMaterialNeed = model;
            }
        }

        public void SetBySelect(DcOrder order)
        {
            if (order != null)
            {
                CurrentMaterialNeed.Composition = order.CompositionStandard;
                CurrentMaterialNeed.PMINumber = order.PMINumber;
                RaisePropertyChanged(nameof(CurrentMaterialNeed));
            }
        }


        private void InitializeProperties()
        {
            States = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.SimpleState));
            states.ToList().ForEach(s => States.Add(s));
        }

        private void InitialCommands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(PMSViews.MaterialNeed));
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
        }

        private void ActionSelect()
        {
            PMSHelper.ViewModels.MissonSelect.SetRequestView(PMSViews.MaterialNeedEdit);
            NavigationService.GoTo(PMSViews.MissonSelect);
        }

        private void ActionSave()
        {

            try
            {
                var service = new MaterialNeedServiceClient();
                if (IsNew)
                {
                    service.AddMaterialNeed(CurrentMaterialNeed);
                }
                else
                {
                    service.UpdateMaterialNeed(CurrentMaterialNeed);
                }
                NavigationService.GoTo(PMSViews.MaterialNeed);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private DcMaterialNeed currentMaterialNeed;
        public DcMaterialNeed CurrentMaterialNeed
        {
            get { return currentMaterialNeed; }
            set
            {
                currentMaterialNeed = value;
                RaisePropertyChanged(nameof(CurrentMaterialNeed));
            }
        }
        public ObservableCollection<string> States { get; set; }

        public RelayCommand Select { get; set; }
    }
}
