﻿using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight;
using PMSClient.Tool.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PMSClient.BasicService;

namespace PMSClient.Tool
{
    public class MaterialNeedCalcualtionVM : ViewModelBase
    {
        public MaterialNeedCalcualtionVM()
        {
            Compounds = new ObservableCollection<DcBDCompound>();
            Molds = new ObservableCollection<DcBDVHPMold>();
            CalculationItems = new ObservableCollection<MaterialNeedCalculationItem>();

            InitializeCurrentItem(5.7);
            InitializeBasicData();

            Add = new RelayCommand(ActionAdd);
            Delete = new RelayCommand<MaterialNeedCalculationItem>(ActionDelete);
            GiveUp = new RelayCommand(() => NavigationService.GoTo(requestView));
            CompoundsSelectionChanged = new RelayCommand<DcBDCompound>(ActionCompoundSelectionChanged);
            MoldsSelectionChanged = new RelayCommand<DcBDVHPMold>(ActionMoldsSelectionChanged);
        }
        private void ActionMoldsSelectionChanged(DcBDVHPMold model)
        {
            if (model != null && CurrentCalculationItem != null)
            {
                CurrentCalculationItem.Diameter = model.InnerDiameter;

                RaisePropertyChanged(nameof(CurrentCalculationItem));
            }
        }
        private void ActionCompoundSelectionChanged(DcBDCompound model)
        {
            if (model != null)
            {
                //每次更新密度，就清空所有计算项
                InitializeCurrentItem(model.Density);
            }
        }

        private PMSViews requestView;
        public void SetRequestView(PMSViews view)
        {
            requestView = view;
        }
        private void InitializeCurrentItem(double density)
        {
           CalculationItems.Clear();
            CurrentCalculationItem = new MaterialNeedCalculationItem()
            {
                ID = Guid.NewGuid(),
                Diameter = 233,
                Thickness = 5.5,
                Quantity = 1,
                Weight = 0,
                WeightLoss = 0
            };
            CurrentDensity = density;
            TotalWeight = 0;
        }

        private void InitializeBasicData()
        {
            try
            {
                using (var service = new VHPMoldServiceClient())
                {
                    var result = service.GetVHPMold();
                    Molds.Clear();
                    result.OrderBy(i => i.InnerDiameter).ToList().ForEach(i => Molds.Add(i));
                }
                using (var service = new CompoundServiceClient())
                {
                    var result = service.GetAllCompounds();
                    Compounds.Clear();
                    result.OrderBy(i => i.MaterialName).ToList().ForEach(i => Compounds.Add(i));
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void ActionAdd()
        {
            if (CurrentCalculationItem != null)
            {
                var model = new MaterialNeedCalculationItem();
                model.ID = CurrentCalculationItem.ID;
                model.Diameter = CurrentCalculationItem.Diameter;
                model.Thickness = CurrentCalculationItem.Thickness;
                model.Quantity = CurrentCalculationItem.Quantity;
                model.WeightLoss = CurrentCalculationItem.WeightLoss;
                model.Remark = CurrentCalculationItem.Remark;

                CalculateCurrentWeight(model);

                CalculationItems.Add(model);

                CalcualteTotalWeight();
            }
        }



        private void ActionDelete(MaterialNeedCalculationItem item)
        {
            if (item != null)
            {
                CalculationItems.Remove(item);
                CalcualteTotalWeight();
            }
        }

        private void CalculateCurrentWeight(MaterialNeedCalculationItem item)
        {
            if (item != null)
            {
                item.Weight = Math.PI * item.Diameter * item.Diameter * item.Thickness / 4 / 1000 * CurrentDensity + item.WeightLoss;
            }
        }
        private void CalcualteTotalWeight()
        {
            double result = 0;
            foreach (var item in CalculationItems)
            {
                result += item.Weight;
            }
            TotalWeight = result;
        }

        public RelayCommand Add { get; set; }
        public RelayCommand<MaterialNeedCalculationItem> Delete { get; set; }

        private MaterialNeedCalculationItem currentCalculationItem;

        public MaterialNeedCalculationItem CurrentCalculationItem
        {
            get { return currentCalculationItem; }
            set { currentCalculationItem = value;RaisePropertyChanged(nameof(CurrentCalculationItem)); }
        }

        private double currentDensity;

        public double CurrentDensity
        {
            get { return currentDensity; }
            set { currentDensity = value; RaisePropertyChanged(nameof(CurrentDensity)); }
        }
        private double totalWeight;
        public double TotalWeight
        {
            get { return totalWeight; }
            set
            {
                totalWeight = value;
                RaisePropertyChanged(nameof(TotalWeight));
            }
        }



        public ObservableCollection<MaterialNeedCalculationItem> CalculationItems { get; set; }
        public ObservableCollection<DcBDCompound> Compounds { get; set; }
        public ObservableCollection<DcBDVHPMold> Molds { get; set; }


        public RelayCommand GiveUp { get; set; }


        public RelayCommand<DcBDCompound> CompoundsSelectionChanged { get; set; }
        public RelayCommand<DcBDVHPMold> MoldsSelectionChanged { get; set; }
    }
}