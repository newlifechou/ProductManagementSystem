﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using PMSClient.MainService;
using GalaSoft.MvvmLight.Messaging;
//using bt = BarTender;



namespace PMSClient.ViewModel
{
    public class RecordDeliveryVM : BaseViewModelPage
    {
        public RecordDeliveryVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }


        public void RefreshDataItem()
        {
            ActionSelectionChanged(CurrentSelectItem);
        }
        private void InitializeProperties()
        {
            RecordDeliveries = new ObservableCollection<DcRecordDelivery>();
            RecordDeliveryItems = new ObservableCollection<DcRecordDeliveryItem>();
        }
        private void InitializeCommands()
        {
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            Add = new RelayCommand(ActionAdd,CanAdd);
            Edit = new RelayCommand<DcRecordDelivery>(ActionEdit,CanEdit);
            Doc = new RelayCommand<DcRecordDelivery>(ActionDoc,CanDoc);
            AddItem = new RelayCommand<DcRecordDelivery>(ActionAddItem,CanAddItem);
            EditItem = new RelayCommand<DcRecordDeliveryItem>(ActionEditItem,CanEditItem);
            SelectionChanged = new RelayCommand<DcRecordDelivery>(ActionSelectionChanged);
        }

        private bool CanDoc(DcRecordDelivery arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑发货记录");
        }

        private bool CanEditItem(DcRecordDeliveryItem arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑发货记录");
        }

        private bool CanAddItem(DcRecordDelivery arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑发货记录");
        }
        /// <summary>
        /// 权限代码=编辑发货记录
        /// </summary>
        /// <returns></returns>
        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑发货记录");
        }

        private bool CanEdit(DcRecordDelivery arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized("编辑发货记录");
        }

        private void ActionAll()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionSelectionChanged(DcRecordDelivery model)
        {
            if (model != null)
            {
                using (var service = new RecordDeliveryServiceClient())
                {
                    var result = service.GetRecordDeliveryItemByRecordDeliveryID(model.ID);
                    RecordDeliveryItems.Clear();
                    result.ToList().ForEach(i => RecordDeliveryItems.Add(i));

                    CurrentSelectItem = model;
                }
            }
        }

        /// <summary>
        /// 发货单标签打印区域
        /// </summary>
        //private bt.Application btApp;
        //private bt.Format btnFormat;
        private void ActionDoc(DcRecordDelivery model)
        {
            #region 必须使用Automation版本的Bartender才允许自动化调用，这个版本36000RMB
            //string title = model.Country;
            //StringBuilder sb = new StringBuilder();
            //DcRecordDeliveryItem[] items;
            //using (var service = new RecordDeliveryServiceClient())
            //{
            //    items = service.GetRecordDeliveryItemByRecordDeliveryID(model.ID);
            //}
            //foreach (var item in items)
            //{
            //    sb.Append(item.Composition);
            //    sb.Append("-");
            //    sb.AppendLine(item.ProductID);
            //}

            //string output = sb.ToString();

            //try
            //{
            //    btApp = new bt.Application();
            //    string templateAddress = System.IO.Path.Combine(Environment.CurrentDirectory, "DocTemplate", "10070.btw");
            //    if (!System.IO.File.Exists(templateAddress))
            //    {
            //        return;
            //    }
            //    btnFormat = btApp.Formats.Open(templateAddress, false, "");
            //    btnFormat.PrintSetup.IdenticalCopiesOfLabel = 1;
            //    btnFormat.PrintSetup.NumberSerializedLabels = 1;

            //    btnFormat.SetNamedSubStringValue("MainTitle", title);
            //    btnFormat.SetNamedSubStringValue("MainContent", output);
            //    btnFormat.Close(bt.BtSaveOptions.btSaveChanges);

            //    btnFormat.PrintOut(true, true);
            //    btnFormat.Close(bt.BtSaveOptions.btSaveChanges);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    btApp.Quit(bt.BtSaveOptions.btSaveChanges);
            //}
            #endregion
            if (model!=null)
            {
                string country = model.Country;
                StringBuilder sb = new StringBuilder();
                DcRecordDeliveryItem[] items;
                using (var service = new RecordDeliveryServiceClient())
                {
                    items = service.GetRecordDeliveryItemByRecordDeliveryID(model.ID);
                }
                int counter = 1;
                foreach (var item in items)
                {
                    //sb.Append(item.Composition.Trim());
                    sb.Append($"No {counter}");
                    sb.Append(" = ");
                    sb.AppendLine($"[{item.ProductID.Trim()}]");
                    counter++;
                }
                string mainContent = $"发往: {country}\r{sb.ToString()}";

                var pageTitle = "发货单标签打印输出";
                var tips = "请复制左边内容后点击打开模板按钮，粘贴到模板合适位置，然后打印标签";
                var template = "发货单";
                PMSHelper.ToolViewModels.LabelOutPut.SetAllParameters(PMSViews.RecordDelivery, pageTitle,
                    tips, template, mainContent);
                NavigationService.GoTo(PMSViews.LabelOutPut);
            }
        }

        private void ActionEditItem(DcRecordDeliveryItem model)
        {
            PMSHelper.ViewModels.RecordDeliveryItemEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.RecordDeliveryItemEdit);
        }

        private void ActionAddItem(DcRecordDelivery model)
        {
            //传递RecordDelivery
            PMSHelper.ViewModels.RecordDeliveryItemEdit.SetNew(model);
            NavigationService.GoTo(PMSViews.RecordDeliveryItemEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.RecordDeliveryEdit.SetNew();
            NavigationService.GoTo(PMSViews.RecordDeliveryEdit);
        }

        private void ActionEdit(DcRecordDelivery model)
        {
            PMSHelper.ViewModels.RecordDeliveryEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.RecordDeliveryEdit);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new RecordDeliveryServiceClient();
            RecordCount = service.GetDeliveryCount();
            ActionPaging();
        }
        private void ActionPaging()
        {
            var service = new RecordDeliveryServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var models = service.GetDelivery(skip, take);
            RecordDeliveries.Clear();
            models.ToList<DcRecordDelivery>().ForEach(o => RecordDeliveries.Add(o));

            CurrentSelectIndex = 0;
            CurrentSelectItem = RecordDeliveries.FirstOrDefault();
            ActionSelectionChanged(CurrentSelectItem);
        }
        #region Properties
        public ObservableCollection<DcRecordDelivery> RecordDeliveries { get; set; }
        public ObservableCollection<DcRecordDeliveryItem> RecordDeliveryItems { get; set; }
        private int currentSelectIndex;

        public int CurrentSelectIndex
        {
            get { return currentSelectIndex; }
            set { currentSelectIndex = value; RaisePropertyChanged(nameof(CurrentSelectIndex)); }
        }
        private DcRecordDelivery currentSelectItem;

        public DcRecordDelivery CurrentSelectItem
        {
            get { return currentSelectItem; }
            set { currentSelectItem = value; RaisePropertyChanged(nameof(CurrentSelectItem)); }
        }

        #endregion
        #region Commands
        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordDelivery> Edit { get; set; }
        public RelayCommand<DcRecordDelivery> Doc { get; set; }
        public RelayCommand<DcRecordDelivery> AddItem { get; set; }
        public RelayCommand<DcRecordDeliveryItem> EditItem { get; set; }
        public RelayCommand<DcRecordDelivery> SelectionChanged { get; set; }
        #endregion



    }
}
