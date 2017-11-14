﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class PlanVM : BaseViewModelPage
    {
        public PlanVM()
        {
            IntitializeProperties();
            IntitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void ActionRefresh(string obj)
        {
            SetPageParametersWhenConditionChange();
        }

        private void IntitializeProperties()
        {
            searchComposition = searchVHPDate = "";
            PlanWithMissons = new ObservableCollection<DcPlanWithMisson>();
        }

        private void IntitializeCommands()
        {
            GoToMisson = new RelayCommand(() => NavigationService.GoTo(PMSViews.Misson), CanGoToMisson);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            GoToSearchPlan = new RelayCommand(() => NavigationService.GoTo(PMSViews.PlanSearch));
            Label = new RelayCommand<DcPlanWithMisson>(ActionLabel);
            SearchMisson = new RelayCommand<DcPlanWithMisson>(ActionSearchMisson);
            SelectionChanged = new RelayCommand<DcPlanWithMisson>(ActionSelectionChanged);
            Output = new RelayCommand<MainService.DcPlanWithMisson>(ActionOutput);
        }

        private void ActionOutput(DcPlanWithMisson model)
        {
            PMSDialogService.ShowYes("计划数据导出时间会比较长，请再完成之前不要进行其他操作");

            int pageIndex = 1;
            int pageSize = 20;
            int recordCount = 0;
            using (var service = new MissonServiceClient())
            {
                recordCount = service.GetPlanExtraCount(SearchVHPDate, SearchComposition);
            }

            int pageCount = recordCount / PageSize + (recordCount % PageSize == 0 ? 0 : 1);

            int skip = 0, take = 0;
            take = pageSize;
            skip = (pageIndex - 1) * pageSize;

            string outputfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "计划导出数据.csv");
            StreamWriter sw = new StreamWriter(new FileStream(outputfile, FileMode.Append), System.Text.Encoding.GetEncoding("GB2312"));

            StringBuilder sb = new StringBuilder();
            using (var service = new MissonServiceClient())
            {
                try
                {
                    while (pageIndex <= pageCount)
                    {
                        var orders = service.GetPlanExtra(skip, take, SearchVHPDate, SearchComposition);
                        orders.ToList().ForEach(o =>
                        {
                            #region 需要导出的数据列
                            sb.Append(o.Plan.SearchCode);
                            sb.Append(",");
                            sb.Append(o.Misson.CompositionStandard);
                            sb.Append(",");
                            sb.Append(o.Plan.MoldDiameter);
                            sb.Append(",");
                            sb.Append(o.Plan.Thickness);
                            sb.Append(",");
                            sb.Append(o.Plan.Quantity);
                            sb.Append(",");
                            sb.Append(o.Plan.CalculationDensity);
                            sb.Append(",");
                            sb.Append(o.Plan.SingleWeight);
                            sb.Append(",");
                            sb.Append(o.Plan.Temperature);
                            sb.Append(",");
                            sb.Append(o.Plan.Pressure);
                            sb.Append(",");
                            sb.Append(o.Plan.FillingRequirement);
                            sb.Append(",");
                            sb.Append(o.Plan.MillingRequirement);
                            #endregion

                            sb.AppendLine();
                        });

                        sw.Write(sb.ToString());
                        sw.Flush();
                        sb.Clear();

                        pageIndex++;
                        skip = (pageIndex - 1) * pageSize;
                    }
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
            sw.Close();

            PMSDialogService.ShowYes("计划数据导出完成到桌面，请右键-打开方式-Excel打开文件");

        }

        private bool CanGoToMisson()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.ReadMisson);
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchComposition) && string.IsNullOrEmpty(SearchVHPDate));
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionSelectionChanged(DcPlanWithMisson model)
        {
            if (model != null)
            {
                CurrentPlanWithMisson = model;
            }
        }

        private void ActionSearchMisson(DcPlanWithMisson model)
        {
            if (model != null)
            {
                PMSHelper.ViewModels.Misson.SetSearchCondition("", model.Misson.PMINumber);
                NavigationService.GoTo(PMSViews.Misson);
            }
        }

        private void ActionLabel(DcPlanWithMisson model)
        {
            if (model != null)
            {

                var sb = new StringBuilder();
                sb.Append(model.Plan.PlanType);
                sb.Append(" ");
                sb.Append(model.Plan.ProcessCode);
                sb.Append(" ");
                sb.AppendLine(UsefulPackage.PMSTranslate.PlanLot(model));
                sb.AppendLine(model.Misson.CompositionStandard);
                sb.Append("模具:");
                sb.AppendLine(model.Plan.MoldDiameter.ToString());
                sb.Append("产品:");
                sb.AppendLine(model.Misson.Dimension);
                sb.Append("订单:");
                sb.AppendLine(model.Misson.PMINumber);
                sb.AppendLine();
                sb.AppendLine("++++++一般标签复制上面内容，样品标签复制下面内容+++++++");
                sb.AppendLine();
                sb.AppendLine(model.Misson.CompositionStandard);
                sb.AppendLine("样品      g");
                sb.AppendLine(UsefulPackage.PMSTranslate.PlanLot(model));

                var mainContent = sb.ToString();

                var pageTitle = "热压毛坯标签打印输出";
                var tips = @"点击打开模板按钮，粘贴不同内容到模板合适位置，热压编号是自动生成的，可能不正确，请再自行修改，然后打印标签";
                var template = "毛坯标签";
                var helpimage = "productionlabel.png";
                PMSHelper.ToolViewModels.LabelOutPut.SetAllParameters(PMSViews.Plan, pageTitle,
                    tips, template, mainContent, helpimage);
                NavigationService.GoTo(PMSViews.LabelOutPut);
            }
        }
        private void ActionAll()
        {
            SearchComposition = SearchVHPDate = "";
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            using (var service = new MissonServiceClient())
            {
                RecordCount = service.GetPlanExtraCount(SearchVHPDate, SearchComposition);
            }
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
            //只显示Checked过的计划
            using (var service = new MissonServiceClient())
            {
                var orders = service.GetPlanExtra(skip, take, SearchVHPDate, SearchComposition);
                PlanWithMissons.Clear();
                orders.ToList().ForEach(o => PlanWithMissons.Add(o));
            }
            CurrentPlanWithMisson = PlanWithMissons.FirstOrDefault();
        }

        private DcPlanWithMisson currentPlanWithMisson;

        public DcPlanWithMisson CurrentPlanWithMisson
        {
            get { return currentPlanWithMisson; }
            set { currentPlanWithMisson = value; RaisePropertyChanged(nameof(CurrentPlanWithMisson)); }
        }

        private string searchComposition;
        public string SearchComposition
        {
            get { return searchComposition; }
            set { searchComposition = value; RaisePropertyChanged(nameof(searchComposition)); }
        }
        private string searchVHPDate;
        public string SearchVHPDate
        {
            get { return searchVHPDate; }
            set { searchVHPDate = value; RaisePropertyChanged(nameof(searchVHPDate)); }
        }

        #region Commands
        public RelayCommand GoToMisson { get; set; }
        public RelayCommand GoToSearchPlan { get; set; }
        public RelayCommand<DcPlanWithMisson> Label { get; set; }
        public RelayCommand<DcPlanWithMisson> SearchMisson { get; set; }
        public RelayCommand<DcPlanWithMisson> SelectionChanged { get; set; }
        public RelayCommand<DcPlanWithMisson> Output { get; set; }
        #endregion

        #region Properties
        public ObservableCollection<DcPlanWithMisson> PlanWithMissons { get; set; }
        #endregion

    }
}
