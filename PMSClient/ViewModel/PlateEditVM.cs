﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.CustomControls;
using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ViewModel
{
    public class PlateEditVM : BaseViewModelEdit
    {
        public PlateEditVM()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.InventoryState>(States);

            PlateMaterials = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.PlateMaterial>(PlateMaterials);

            PlateLastWeldMaterials = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.PlateLastWeldMaterial, PlateLastWeldMaterials);

            PlateSuppliers = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.PlateSupplier, PlateSuppliers);

            PlateDimensions = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.PlateDimension, PlateDimensions);

            PlateDefects = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.PlateDefect, PlateDefects);

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
        }

        public void SetNew()
        {
            IsNew = true;
            var model = new DcPlate();
            #region 初始化
            IsNew = true;
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.InventoryState.库存.ToString();
            model.PlateLot = UsefulPackage.PMSTranslate.PlateLot();
            model.PrintNumber = "无";
            model.PlateMaterial = PMSCommon.PlateMaterial.Cu.ToString();
            model.Dimension = PMSCommon.CustomData.PlateDimension[0];
            model.Parallelism = "F-A=M0.02mm;F-B=M0.02mm";
            model.DimensionActual = model.Dimension;
            model.Supplier = PMSCommon.CustomData.PlateSupplier[0];
            model.UseCount = "0";
            model.Hardness = "未知";
            model.LastWeldMaterial = PMSCommon.CustomData.PlateLastWeldMaterial[0].ToString();
            model.Weight = "未知";
            model.Appearance = "正常";
            model.Defects = "无";
            model.Remark = "";

            #endregion
            CurrentPlate = model;
        }
        public void SetDuplicate(DcPlate model)
        {
            if (model != null)
            {
                IsNew = true;
                CurrentPlate = model;
                CurrentPlate.ID = Guid.NewGuid();
                CurrentPlate.CreateTime = DateTime.Now;
                CurrentPlate.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentPlate.State = PMSCommon.InventoryState.库存.ToString();
            }
        }
        public void SetEdit(DcPlate model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentPlate = model;
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.Plate);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentPlate.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要作废吗？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new PlateServiceClient();
                if (IsNew)
                {
                    //新建的时候可以选择数目
                    PlateNumber dialog = new PlateNumber();
                    dialog.ShowDialog();
                    int number = dialog.CurrentPlateNumber;
                    if (number > 1)
                    {
                        string prefix = CurrentPlate.PlateLot.Substring(0, 10);
                        for (int i = 0; i < number; i++)
                        {
                            CurrentPlate.ID = Guid.NewGuid();
                            CurrentPlate.CreateTime = DateTime.Now;
                            CurrentPlate.PlateLot = prefix + (i + 1).ToString();
                            service.AddPlateByUID(CurrentPlate, uid);
                        }
                    }
                    else
                    {
                        service.AddPlateByUID(CurrentPlate, uid);
                    }

                }
                else
                {
                    service.UpdatePlateByUID(CurrentPlate, uid);
                }
                service.Close();
                PMSHelper.ViewModels.Plate.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public List<string> PlateMaterials { get; set; }
        public List<string> States { get; set; }
        public List<string> PlateLastWeldMaterials { get; set; }
        public List<string> PlateSuppliers { get; set; }
        public List<string> PlateDimensions { get; set; }
        public List<string> PlateDefects { get; set; }



        private DcPlate currentPlate;
        public DcPlate CurrentPlate
        {
            get { return currentPlate; }
            set
            {
                currentPlate = value;
                RaisePropertyChanged(nameof(CurrentPlate));
            }
        }
    }
}
