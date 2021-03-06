﻿using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PMSClient.ViewModel;
using PMSClient.View;
using PMSClient.MainService;
using PMSClient.Tool;
using System.Timers;
using fm = System.Windows.Forms;
using System.Windows.Threading;
using PMSClient.Helpers;
using PMSClient.Components.PMSSettingHelper;

namespace PMSClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainDesktop : Window
    {
        private ViewLocator _views;
        private ToolViewLocator _toolviews;
        private Timer _timerMain;
        private fm.NotifyIcon _notifyIcon;
        public MainDesktop()
        {
            InitializeComponent();
            LanguageLoad();
        }

        private void LanguageLoad()
        {
            string lang = Properties.Settings.Default.Language;
            if (string.IsNullOrEmpty(lang))
            {
                lang = "zh-cn";
            }
            string langPath = "Language/" + lang + ".xaml";
            App.Current.Resources.MergedDictionaries[2].Source = new Uri(langPath, UriKind.Relative);
            cboLanguage.SelectedIndex = lang == "zh-cn" ? 0 : 1;
            //设定当前程序的语言类型，供其他程序使用
            PMSHelper.Language = lang;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 标题处理
            try
            {
                var titleName = (App.Current as App).FindResource("AppNameDesktop").ToString();
                //var versonName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                //这里自定义版本号
                var versonName = Properties.Settings.Default.Version;
                Title = $"{titleName}-{versonName}";
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex, "Main Window_Loaded");
            }
            #endregion

            #region 初始化变量并注册mvvmlight消息
            _views = PMSHelper.DesktopViews;
            _toolviews = PMSHelper.ToolViews;

            Messenger.Default.Register<PMSViews>(this, MainNavigationToken.Navigate, ActionNavigation);
            Messenger.Default.Register<string>(this, MainNavigationToken.StatusMessage, ActionStatusMessage);
            #endregion

            //导航到首页
            RefreshLogInformation();
            NavigateTo(_views.LogIn);

            #region 托盘部分
            // InitializeTray();
            #endregion
            //首次检测心跳
            HeartBeatCheck();

            //加载底部工具栏
            BottomToolBar.Content = new Navbar();

            //下载最新权限表到本地-只在启动的时候下载一次
            PMSHelper.CurrentSession.DownloadAccessSheet();
            //读取本地权限表
            PMSHelper.CurrentSession.ReadAccessSheetFromLocal();

            //加载配置
            PMSClient.Components.PMSSettingHelper.PMSSettingService.CacheSettings();

            #region 设置主定时器
            //设置主定时器
            _timerMain = new Timer();
            _timerMain.Interval = 30000;
            _timerMain.Elapsed += _timerMain_Tick;
            _timerMain.Start();
            #endregion
        }
        #region 定时器设定
        /// <summary>
        /// 主定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timerMain_Tick(object sender, EventArgs e)
        {
            if (HeartBeatCheck())
            {
                //NoticeCheck();
            }
        }

        private bool HeartBeatCheck()
        {
            var is_lan_ok = HeartBeatLAN();
            var is_www_ok = HeartBeatWWW();
            return is_www_ok || is_lan_ok;
        }

        private bool HeartBeatWWW()
        {
            System.Diagnostics.Debug.WriteLine("外网心跳检测");
            try
            {
                if (PMSHelper.CurrentSession?.CurrentUserRole?.GroupName == "管理员")
                {
                    using (var remote_heartbeat = new PMSClient.RemoteHeartBeatService.HeartBeatSeriveClient())
                    {
                        if (remote_heartbeat.Beat() == "ok")
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                TxtRemoteHeartBeat.Text = "外网服务器通信正常";
                            });
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    TxtRemoteHeartBeat.Text = "外网心跳检测出错";
                });
                PMSHelper.CurrentLog.Error(ex, "Heart BeatWWW");
                return false;
            }

        }

        private bool HeartBeatLAN()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("内网心跳检测");

                int all_ready = 0;
                int.TryParse(PMSSettingService.ReadKeyFromCache("history_log_count"), out all_ready);

                using (var lan_heartbeat = new PMSClient.HeartBeatService.HeartBeatSeriveClient())
                {
                    //读取日志信息
                    int count_all = lan_heartbeat.GetOperationCallTimes();
                    int count_yesterday = lan_heartbeat.GetOperationCallTimesYesterday();
                    int count_today = lan_heartbeat.GetOperationCallTimesToday();

                    if (lan_heartbeat.Beat() == "ok")
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            txtHeartBeat.Text = "内网服务器通信正常";

                            if (PMSHelper.CurrentSession?.CurrentUserRole?.GroupName != "")
                            {
                                TxtInformationLog.Text = $"访问信息 昨日:{count_yesterday}次 今日:{count_today}次 共{((count_all + all_ready) / 10000)}w次";
                            }
                        });
                        //txtHeartBeat.Text = "服务器通信正常";
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    txtHeartBeat.Text = "内网心跳检测出错";
                    TxtInformationLog.Text = "Error";
                });
                PMSHelper.CurrentLog.Error(ex, "Heart BeatLAN");
                return false;
            }
        }

        public int noticeCount = 0;
        private void NoticeCheck()
        {
            System.Diagnostics.Debug.WriteLine(noticeCount);
            //循环检测是否有新消息
            PMSNotice.CheckIt();
            if (!PMSNotice.HasNewNotice)
            {
                noticeCount = 0;
                return;
            }
            //每循环n次显示气泡信息一次
            if (noticeCount % 60 == 0)
            {
                #region NotifyIcon
                if (PMSHelper.Language == "zh-cn")
                {
                    SetNotifyIcon("PMS", "有新消息,请到导航界面->新消息 \r\n点击[我知道了]按钮", 6000);
                }
                else
                {
                    SetNotifyIcon("PMS", "There are New Messages. Please Go To Navigation -> Notice\r\nClick Button [I Know]", 6000);
                }
                #endregion
                noticeCount = 0;
            }
            noticeCount++;
        }

        #endregion

        #region 托盘通知
        private void InitializeTray()
        {
            _notifyIcon = new fm.NotifyIcon();
            _notifyIcon.Text = "PMS生产管理系统";
            _notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            _notifyIcon.Visible = true;

            fm.ContextMenu menu = new fm.ContextMenu();
            fm.MenuItem item = new fm.MenuItem();
            item.Text = "关闭";
            item.Click += (s, e) =>
            {
                this.Close();
            };
            menu.MenuItems.Add(item);
            _notifyIcon.ContextMenu = menu;
            _notifyIcon.Visible = true;
            //SetNotifyIcon("欢迎", "欢迎使用PMS生产管理系统\r\n请在左上方登录使用", 2000);
        }
        public void SetNotifyIcon(string title, string message, int showtime)
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = true;
                _notifyIcon.BalloonTipTitle = title;
                _notifyIcon.BalloonTipText = message;
                _notifyIcon.ShowBalloonTip(showtime);
            }
        }
        private void RemoveNofiyIcon()
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
                _notifyIcon = null;
            }
        }
        #endregion
        /// <summary>
        /// 导航区域
        /// </summary>
        private void ActionNavigation(PMSViews token)
        {
            PMSHelper.CurrentLog.Log(token.ToString());
            switch (token)
            {
                case PMSViews.LogIn:
                    RefreshLogInformation();
                    NavigateTo(_views.LogIn);
                    break;
                case PMSViews.Navigation:
                    RefreshLogInformation();
                    NavigateTo(_views.Navigation);
                    break;
                case PMSViews.NavigationWorkFlow:
                    NavigateTo(_views.NavigationWorkFlow);
                    break;
                case PMSViews.Order:
                    NavigateTo(_views.Order);
                    break;
                case PMSViews.OrderEdit:
                    NavigateTo(_views.OrderEdit);
                    break;
                case PMSViews.OrderSelect:
                    NavigateTo(_views.OrderSelect);
                    break;
                case PMSViews.OutSource:
                    NavigateTo(_views.OutSource);
                    break;
                case PMSViews.OutSourceEdit:
                    NavigateTo(_views.OutSourceEdit);
                    break;
                case PMSViews.OutSourceSelect:
                    NavigateTo(_views.OutSourceSelect);
                    break;
                case PMSViews.MissonSelect:
                    NavigateTo(_views.MissonSelect);
                    break;
                case PMSViews.MaterialNeed:
                    NavigateTo(_views.MaterialNeed);
                    break;
                case PMSViews.MaterialNeedEdit:
                    NavigateTo(_views.MaterialNeedEdit);
                    break;
                case PMSViews.MaterialNeedSelect:
                    NavigateTo(_views.MaterialNeedSelect);
                    break;
                case PMSViews.MaterialOrder:
                    NavigateTo(_views.MaterialOrder);
                    break;
                case PMSViews.MaterialOrderEdit:
                    NavigateTo(_views.MaterialOrderEdit);
                    break;
                case PMSViews.MaterialOrderItemEdit:
                    NavigateTo(_views.MaterialOrderItemEdit);
                    break;
                case PMSViews.MaterialOrderItemSelect:
                    NavigateTo(_views.MaterialOrderItemSelect);
                    break;
                case PMSViews.MaterialOrderItemList:
                    NavigateTo(_views.MaterialOrderItemList);
                    break;
                case PMSViews.MaterialOrderItemListUnCompleted:
                    NavigateTo(_views.MaterialOrderItemListUnCompleted);
                    break;
                case PMSViews.MaterialInventoryIn:
                    NavigateTo(_views.MaterialInventoryIn);
                    break;
                case PMSViews.MaterialInventoryInEdit:
                    NavigateTo(_views.MaterialInventoryInEdit);
                    break;
                case PMSViews.MaterialInventoryInSelect:
                    NavigateTo(_views.MaterialInventoryInSelect);
                    break;
                case PMSViews.MaterialInventoryInUnCompleted:
                    NavigateTo(_views.MaterialInventoryInUnCompleted);
                    break;
                case PMSViews.MaterialInventoryOut:
                    NavigateTo(_views.MaterialInventoryOut);
                    break;
                case PMSViews.MaterialInventoryOutEdit:
                    NavigateTo(_views.MaterialInventoryOutEdit);
                    break;
                case PMSViews.Misson:
                    NavigateTo(_views.Misson);
                    break;
                case PMSViews.Plan:
                    NavigateTo(_views.Plan);
                    break;
                case PMSViews.PlanTrace:
                    NavigateTo(_views.PlanTrace);
                    break;
                case PMSViews.PlanEdit:
                    NavigateTo(_views.PlanEdit);
                    break;
                case PMSViews.PlanSelect:
                    NavigateTo(_views.PlanSelect);
                    break;
                case PMSViews.PlanConclusion:
                    NavigateTo(_views.PlanConclusion);
                    break;
                case PMSViews.PlanConclusionEdit:
                    NavigateTo(_views.PlanConclusionEdit);
                    break;
                case PMSViews.RecordMilling:
                    NavigateTo(_views.RecordMilling);
                    break;
                case PMSViews.RecordMillingEdit:
                    NavigateTo(_views.RecordMillingEdit);
                    break;
                case PMSViews.RecordVHP:
                    NavigateTo(_views.RecordVHP);
                    break;
                case PMSViews.RecordVHPQuickEdit:
                    NavigateTo(_views.RecordVHPQuickEdit);
                    break;
                case PMSViews.RecordDeMold:
                    NavigateTo(_views.RecordDeMold);
                    break;
                case PMSViews.RecordDeMoldEdit:
                    NavigateTo(_views.RecordDeMoldEdit);
                    break;
                case PMSViews.RecordDeMoldSelect:
                    NavigateTo(_views.RecordDeMoldSelect);
                    break;
                case PMSViews.RecordMachine:
                    NavigateTo(_views.RecordMachine);
                    break;
                case PMSViews.RecordMachineEdit:
                    NavigateTo(_views.RecordMachineEdit);
                    break;
                case PMSViews.RecordMachineSelect:
                    NavigateTo(_views.RecordMachineSelect);
                    break;
                case PMSViews.RecordTest:
                    NavigateTo(_views.RecordTest);
                    break;
                case PMSViews.RecordTestEdit:
                    NavigateTo(_views.RecordTestEdit);
                    break;
                case PMSViews.RecordTestSelect:
                    NavigateTo(_views.RecordTestSelect);
                    break;
                case PMSViews.RecordTestDoc:
                    NavigateTo(_views.RecordTestDoc);
                    break;
                case PMSViews.RecordBonding:
                    NavigateTo(_views.RecordBonding);
                    break;
                case PMSViews.RecordBondingSimpleEdit:
                    NavigateTo(_views.RecordBondingSimpleEdit);
                    break;
                case PMSViews.RecordBondingSelect:
                    NavigateTo(_views.RecordBondingSelect);
                    break;
                case PMSViews.Delivery:
                    NavigateTo(_views.Delivery);
                    break;
                case PMSViews.DeliveryEdit:
                    NavigateTo(_views.DeliveryEdit);
                    break;
                case PMSViews.DeliveryItemEdit:
                    NavigateTo(_views.DeliveryItemEdit);
                    break;
                case PMSViews.DeliveryItemList:
                    NavigateTo(_views.DeliveryItemList);
                    break;
                case PMSViews.Product:
                    NavigateTo(_views.Product);
                    break;
                case PMSViews.ProductEdit:
                    NavigateTo(_views.ProductEdit);
                    break;
                case PMSViews.ProductUnCompleted:
                    NavigateTo(_views.ProductUnCompleted);
                    break;
                case PMSViews.ProductSelect:
                    NavigateTo(_views.ProductSelect);
                    break;
                case PMSViews.Plate:
                    NavigateTo(_views.Plate);
                    break;
                case PMSViews.PlateEdit:
                    NavigateTo(_views.PlateEdit);
                    break;
                case PMSViews.PlateSelect:
                    NavigateTo(_views.PlateSelect);
                    break;
                case PMSViews.MaterialNeedCalcuationTool:
                    NavigateTo(_toolviews.MaterialNeedCalculation);
                    break;
                case PMSViews.LabelOutPut:
                    NavigateTo(_toolviews.LabelOutPut);
                    break;
                case PMSViews.DensityEstamator:
                    NavigateTo(_toolviews.DensityEstamator);
                    break;
                case PMSViews.Customer:
                    NavigateTo(_views.Customer);
                    break;
                case PMSViews.CustomerEdit:
                    NavigateTo(_views.CustomerEdit);
                    break;
                case PMSViews.StatisticOrder:
                    NavigateTo(_views.OrderStatistic);
                    break;
                case PMSViews.StatisticPlan:
                    NavigateTo(_views.PlanStatistic);
                    break;
                case PMSViews.StatisticProduct:
                    NavigateTo(_views.ProductStatistic);
                    break;
                case PMSViews.StatisticDelivery:
                    NavigateTo(_views.DeliveryStatistic);
                    break;
                case PMSViews.FeedBack:
                    NavigateTo(_views.FeedBack);
                    break;
                case PMSViews.FeedBackEdit:
                    NavigateTo(_views.FeedBackEdit);
                    break;
                case PMSViews.IntegretedSearch:
                    NavigateTo(_views.IntegretedSearch);
                    break;
                case PMSViews.Tool:
                    NavigateTo(_views.Tool);
                    break;
                case PMSViews.BDCompound:
                    NavigateTo(_views.Compound);
                    break;
                case PMSViews.BDCompoundEdit:
                    NavigateTo(_views.CompoundEdit);
                    break;
                case PMSViews.Debug:
                    NavigateTo(_views.Debug);
                    break;
                case PMSViews.ToDo:
                    NavigateTo(_views.ToDo);
                    break;
                case PMSViews.ToDoEdit:
                    NavigateTo(_views.ToDoEdit);
                    break;
                case PMSViews.FillingTool:
                    NavigateTo(_views.FillingTool);
                    break;
                case PMSViews.FillingToolEdit:
                    NavigateTo(_views.FillingToolEdit);
                    break;
                case PMSViews.MillingTool:
                    NavigateTo(_views.MillingTool);
                    break;
                case PMSViews.MillingToolEdit:
                    NavigateTo(_views.MillingToolEdit);
                    break;
                case PMSViews.History:
                    //TODO:实现历史页面导航
                    break;
                case PMSViews.Failure:
                    NavigateTo(_views.Failure);
                    break;
                case PMSViews.FailureEdit:
                    NavigateTo(_views.FailureEdit);
                    break;
                case PMSViews.PMICounter:
                    NavigateTo(_views.PMICounter);
                    break;
                case PMSViews.PMICounterEdit:
                    NavigateTo(_views.PMICounterEdit);
                    break;
                case PMSViews.RemainInventory:
                    NavigateTo(_views.RemainInventory);
                    break;
                case PMSViews.RemainInventoryEdit:
                    NavigateTo(_views.RemainInventoryEdit);
                    break;
                case PMSViews.OutsideProcess:
                    NavigateTo(_views.OutsideProcess);
                    break;
                case PMSViews.OutsideProcessEdit:
                    NavigateTo(_views.OutsideProcessEdit);
                    break;
                case PMSViews.Sample:
                    NavigateTo(_views.Sample);
                    break;
                case PMSViews.SampleSelect:
                    NavigateTo(_views.SampleSelect);
                    break;
                case PMSViews.SampleEdit:
                    NavigateTo(_views.SampleEdit);
                    break;
                case PMSViews.RawMaterialSheet:
                    NavigateTo(_views.RawMaterialSheet);
                    break;
                case PMSViews.RawMaterialSheetEdit:
                    NavigateTo(_views.RawMaterialSheetEdit);
                    break;
                case PMSViews.SimpleMaterial:
                    NavigateTo(_views.SimpleMaterial);
                    break;
                case PMSViews.SimpleMaterialEdit:
                    NavigateTo(_views.SimpleMaterialEdit);
                    break;
                case PMSViews.ConsumableInventory:
                    NavigateTo(_views.ConsumableInventory);
                    break;
                case PMSViews.ConsumableInventorySelect:
                    NavigateTo(_views.ConsumableInventorySelect);
                    break;
                case PMSViews.ConsumableInventoryEdit:
                    NavigateTo(_views.ConsumableInventoryEdit);
                    break;
                case PMSViews.ConsumablePurchase:
                    NavigateTo(_views.ConsumablePurchase);
                    break;
                case PMSViews.ConsumablePurchaseEdit:
                    NavigateTo(_views.ConsumablePurchaseEdit);
                    break;
                case PMSViews.MaintainPlan:
                    NavigateTo(_views.MaintenancePlan);
                    break;
                case PMSViews.MaintainPlanSelect:
                    NavigateTo(_views.MaintenancePlanSelect);
                    break;
                case PMSViews.MaintainPlanEdit:
                    NavigateTo(_views.MaintenancePlanEdit);
                    break;
                case PMSViews.MaintainRecord:
                    NavigateTo(_views.MaintenanceRecord);
                    break;
                case PMSViews.MaintainRecordEdit:
                    NavigateTo(_views.MaintenanceRecordEdit);
                    break;
                default:
                    break;
            }
        }

        #region 主窗口操作和事件处理
        private void NavigateTo(UserControl view)
        {
            if (view != null)
            {
                mainArea.Content = view;
            }
        }

        private void ActionStatusMessage(string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                txtStateMessage.Text = obj;
            }
            else
            {
                txtStateMessage.Text = "状态栏";
            }
        }
        /// <summary>
        /// 必须放在Closed事件中，否则退出过程中取消就出问题了
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            _timerMain.Stop();
            RemoveNofiyIcon();
            Messenger.Default.Unregister(this);
            base.OnClosed(e);
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ////执行一些警示通知
            //var warningMe = new ExitHelper();
            //warningMe.ProcessWhenExitApp();


            bool result = false;
            if (PMSHelper.Language == "zh-cn")
            {
                result = PMSDialogService.ShowYesNo("请问", "确定要退出吗？");
            }
            else
            {
                result = PMSDialogService.ShowYesNo("Quit", "Are you sure to quit?");
            }
            if (!result)
            {
                //this.WindowState = WindowState.Minimized;
                e.Cancel = true;
                return;
            }
        }
        #endregion
        #region 公共方法

        public void LogOut()
        {
            if (PMSHelper.CurrentSession.CurrentUser != null)
            {
                PMSHelper.CurrentSession.LogOut();
                _views.LogIn.ClearLog();
                SetNotifyIcon("注销成功", "您的账户已经注销成功", 3000);
                NavigationService.GoTo(PMSViews.LogIn);
            }
        }
        public void RefreshLogInformation()
        {
            var _logInformation = PMSHelper.CurrentSession;
            string logNavigationBar = "";
            string logStatusBar = "";
            if (_logInformation.CurrentUser != null)
            {
                if (PMSHelper.Language == "zh-cn")
                {
                    logNavigationBar = $"当前用户:[{ _logInformation.CurrentUser.RealName}] 角色:[{_logInformation.CurrentUserRole.GroupName}]";
                    logStatusBar = $"[{ _logInformation.CurrentUser.RealName}]";
                }
                else
                {
                    logNavigationBar = $"Current User:[{ _logInformation.CurrentUser.RealName}] Role:[{_logInformation.CurrentUserRole.GroupName}]";
                    logStatusBar = $"[{ _logInformation.CurrentUser.RealName}]";
                }
            }
            else
            {
                if (PMSHelper.Language == "zh-cn")
                {
                    logNavigationBar = logStatusBar = "未登录";
                }
                else
                {
                    logNavigationBar = logStatusBar = "no user";
                }

            }
            txtCurrentUserName.Text = logStatusBar;
            PMSHelper.ViewModels.Navigation.SetLogInformation(logNavigationBar);
        }
        #endregion

        private void cboLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboLanguage.SelectedItem != null)
            {
                Properties.Settings.Default.Language = cboLanguage.SelectedIndex == 0 ? "zh-cn" : "us-en";
                Properties.Settings.Default.Save();
            }
        }

        private void BtnMainLog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string errorPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Error");
                System.Diagnostics.Process.Start(errorPath);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex, "BtnMainLog_Click");
            }
        }

        private void BtnUnLock_Click(object sender, RoutedEventArgs e)
        {
            if (PMSHelper.CurrentSession.CurrentUser == null)
            {
                PMSDialogService.Show("你还没有登录");
                return;
            }
            if (PMSDialogService.ShowYesNo("确定？", "确定要解除你设置的所有编辑锁定？"))
            {

                //管理员解除所有锁定
                //普通用户接触自己用户名的锁定
                if (PMSHelper.CurrentSession.CurrentUserRole.GroupName == "管理员")
                {
                    Helpers.EditLockHelper.UnLockAll();
                }
                else
                {
                    Helpers.EditLockHelper.UnLockByCurrentUser();
                }
                PMSDialogService.Show("解除成功");
            }
        }
    }
}
