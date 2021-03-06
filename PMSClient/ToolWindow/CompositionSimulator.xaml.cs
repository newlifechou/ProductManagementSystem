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
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using PMSClient.Simulator;
using CommonHelper;

namespace PMSClient.ToolWindow
{
    /// <summary>
    /// CompositionSimulator.xaml 的交互逻辑
    /// </summary>
    public partial class CompositionSimulator : Window
    {
        public CompositionSimulator()
        {
            InitializeComponent();
            txtCondition.Text = point + composition_cigs;
        }

        public void SetForBridgeLine()
        {
            point = "13\r\n";
            txtCondition.Text = point + composition_sag;
        }

        public event EventHandler<string> FillIn;

        private string point = "5\r\n";
        private string composition_cigs = "Cu+22.8\r\nIn+20\r\nGa+7\r\nSe+50.2";
        private string composition_inse = "In+2\r\nSe+3";
        private string composition_cgs = "Cu+1\r\nGa+1\r\nSe+2";
        private string composition_sag = "Se+44\r\nAs+33\r\nGe+22";
        private string composition_bitese = "Bi+39\r\nTe+59\r\nSe+2";
        private string composition_bisbte = "Bi+9\r\nSb+31\r\nTe+60";

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string input = txtCondition.Text;
                if (string.IsNullOrEmpty(input)) return;
                double offset = 0;
                double.TryParse(TxtXRFOffset.Text.Trim(), out offset);
                double currentOffset = offset;

                //CompositionSimulatorHelper helper = new CompositionSimulatorHelper(currentOffset);
                //string csv = helper.SimulateCompositionToCsvFormat(input);

                var service = new CompositionSimulatorService(currentOffset);
                string csv = service.Simulate(input);
                txtCsv.Text = csv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnFill_Click(object sender, RoutedEventArgs e)
        {
            //trigger event
            OnFillIn();
        }

        private void OnFillIn()
        {
            string csv = txtCsv.Text;
            if (string.IsNullOrEmpty(csv))
                return;
            //if (FillIn != null)
            //    FillIn(this, csv);
            FillIn?.Invoke(this, csv);
        }

        private void KeepTop_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = (bool)KeepTop.IsChecked;
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.OriginalSource as Button;
            if (btn == null) return;
            switch (btn.Content.ToString())
            {
                case "CIGS":
                    txtCondition.Text = point + composition_cigs;
                    break;
                case "InSe":
                    txtCondition.Text = point + composition_inse;
                    break;
                case "CuGaSe":
                    txtCondition.Text = point + composition_cgs;
                    break;
                case "BiTeSe":
                    txtCondition.Text = point + composition_bitese;
                    break;
                case "BiSbTe":
                    txtCondition.Text = point + composition_bisbte;
                    break;
                case "SeAsGe":
                    txtCondition.Text = point + composition_sag;
                    break;
                default:
                    break;
            }
        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.RecordTestEditVM vm = PMSHelper.ViewModels.RecordTestEdit;
                string compostion = vm.CurrentRecordTest.Composition;
                string customer = vm.CurrentRecordTest.Customer;
                string dimension = vm.CurrentRecordTest.Dimension;
                var elements = Regex.Matches(compostion, @"[A-Za-z]+");


                var numbers = Regex.Matches(compostion, @"\d+(\.\d+)?");
                StringBuilder sb = new StringBuilder();
                sb.Clear();

                if (customer.ToLower().Contains("bridgeline"))
                {
                    //如果是大靶，尺寸以4开头
                    if (dimension.StartsWith("4"))
                    {
                        sb.AppendLine("17");
                    }
                    else
                    {
                        sb.AppendLine("13");
                    }
                }
                else
                {
                    sb.AppendLine("5");
                }

                for (int i = 0; i < elements.Count; i++)
                {
                    sb.Append(elements[i].Value);
                    sb.Append("+");
                    sb.AppendLine(numbers[i].Value);
                }

                txtCondition.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                PMSDialogService.ShowWarning("读取错误，请手动按照格式输入");
            }
        }

        private void BtnOneClick_Click(object sender, RoutedEventArgs e)
        {
            BtnRead_Click(sender, e);
            BtnCreate_Click(sender, e);
            BtnFill_Click(sender, e);
        }

        private void BtnOneFile_Click(object sender, RoutedEventArgs e)
        {
            var result = XSHelper.XS.Dialog.ShowSaveDialog(XSHelper.XS.File.GetDesktopPath(),
                  "(*.csv)|*.csv", "成分.csv");
            if (result.HasSelected)
            {
                XSHelper.XS.File.SaveText(result.SelectPath, txtCsv.Text);
            }
        }
    }
}
