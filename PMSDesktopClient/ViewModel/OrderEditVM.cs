﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSDesktopClient.ServiceReference;

namespace PMSDesktopClient.ViewModel
{
    public class OrderEditVM : ViewModelBase
    {
        public OrderEditVM()
        {

        }
        public OrderEditVM(DcOrder order)
        {
            if (order != null)
            {
                CurrentOrder = order;
                isNew = false;
            }
            else
            {
                var dcOrder = new DcOrder();
                dcOrder.ID = Guid.NewGuid();
                dcOrder.CustomerName = "Midsummer";
                dcOrder.Purity = "99.99";
                dcOrder.CreateTime = DateTime.Now;
                dcOrder.DeadLine = DateTime.Now.AddDays(30);
                dcOrder.ReviewDate = DateTime.Now;
                dcOrder.PolicyMakeDate = DateTime.Now;
                dcOrder.State = "UnChecked";
                dcOrder.Priority = "Normal";
                dcOrder.CompositionOriginal = "CuGaSe2";
                dcOrder.CompositionStandard = "Cu25Ga25Se50";
                dcOrder.CompositoinAbbr = "CuGaSe";
                dcOrder.Creator = "xs.zhou";
                dcOrder.PolicyType = "Target";
                dcOrder.ReviewPassed = true;
                dcOrder.Quantity = 1;
                dcOrder.QuantityUnit = "piece";


                CurrentOrder = dcOrder;
                isNew = true;
            }

            Save = new RelayCommand(ActionSave, CanSave);
            GiveUp = new RelayCommand(ActionGiveUp);

        }

        private void ActionGiveUp()
        {
            NavigationService.GoTo("OrderView");
        }

        private bool CanSave()
        {
            return true;
        }

        private void ActionSave()
        {
            var service = new OrderServiceClient();
            if (isNew)
            {
                service.AddOrder(CurrentOrder);
            }
            else
            {
                service.UpdateOrder(CurrentOrder);
            }
            NavigationService.GoTo("OrderView");
            Messenger.Default.Send<Object>("","RefreshOrder");
        }

        private bool isNew;


        private DcOrder currentOrder;

        public DcOrder CurrentOrder
        {
            get { return currentOrder; }
            set
            {
                currentOrder = value;
                RaisePropertyChanged(nameof(CurrentOrder));
            }
        }


        public RelayCommand Save { get; set; }
        public RelayCommand GiveUp { get; set; }

    }
}
