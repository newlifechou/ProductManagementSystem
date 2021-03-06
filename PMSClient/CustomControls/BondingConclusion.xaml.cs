﻿using PMSClient.MainService;
using System;
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
using System.Windows.Shapes;

namespace PMSClient.CustomControls
{
    /// <summary>
    /// BondingConclusion.xaml 的交互逻辑
    /// </summary>
    public partial class BondingConclusion : Window
    {
        public BondingConclusion()
        {
            InitializeComponent();
            var states = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.BondingState>(states);
            cboState.ItemsSource = states;
        }

        public DcRecordBonding BondingModel
        {
            set
            {
                txtProductID.Text = value.TargetProductID;
                txtProductComposition.Text = value.TargetComposition;
                txtCoverPlateNumber.Text = value.CoverPlateNumber;
                txtDefects.Text = value.Remark;
                txtPlateNumber.Text = value.PlateLot;
                txtWeldingRate.Text = value.WeldingRate.ToString();
                cboState.SelectedItem = value.State;
                txtWeldingDefect.Text = value.WeldingDefect;
            }
        }

        public string State { get { return cboState.SelectedItem.ToString(); } }
        public string PlateNumber { get { return txtPlateNumber.Text; } }
        public string Defects { get { return txtDefects.Text; } }
        public string WeldingRate { get { return txtWeldingRate.Text; } }
        public string WeldingDefect { get { return txtWeldingDefect.Text; } }

        public string CoverPlateNumber { get { return txtCoverPlateNumber.Text; } }

        public string ProductID
        {
            set
            {
                txtProductID.Text = value;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;
            this.Close();
        }

        private void btnDefects_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                txtDefects.Text += button.Content.ToString() + ";";
                txtDefects.Text = txtDefects.Text.Replace("无", "");
            }
        }

        private void BtnFinish_Click(object sender, RoutedEventArgs e)
        {
            cboState.SelectedItem = PMSCommon.BondingState.最终完成.ToString();
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.OriginalSource as Button;
            string csan_str = txtWeldingDefect.Text + btn.Content.ToString() + ";";
            txtWeldingDefect.Text = csan_str.Replace("无", "").Trim();
            e.Handled = true;
        }
    }
}
