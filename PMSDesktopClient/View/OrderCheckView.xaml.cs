﻿using System;
using System.Collections.Generic;
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
using PMSDesktopClient.ViewModel;
using PMSDesktopClient.ServiceReference;
using PMSCommon;

namespace PMSDesktopClient.View
{
    /// <summary>
    /// OderCheckView.xaml 的交互逻辑
    /// </summary>
    public partial class OrderCheckView : UserControl
    {
        public OrderCheckView()
        {
            InitializeComponent();
            this.DataContext = new OrderVM();
        }
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DcOrder order = (DcOrder)e.Row.DataContext;
            if (order != null)
            {
                switch (order.State)
                {
                    case (int)ModelState.Paused:
                        e.Row.Background = this.FindResource("PausedBrush") as SolidColorBrush;
                        break;
                    case (int)ModelState.UnCompleted:
                        e.Row.Background = this.FindResource("UnCompletedBrush") as SolidColorBrush;
                        break;
                    case (int)ModelState.Completed:
                        e.Row.Background = this.FindResource("CompletedBrush") as SolidColorBrush;
                        break;
                    default:
                        break;
                }
                if (order.Priority == (int)OrderPriority.Emerygency)
                {
                    e.Row.Background = this.FindResource("EmergencyBrush") as SolidColorBrush;
                }

            }

        }
    }
}