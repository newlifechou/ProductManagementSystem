﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSDesktopClient.PMSMainService;

namespace PMSDesktopClient.ViewModel
{
    public class OrderSelectForMaterialNeedEditVM:OrderSelectBaseVM
    {
        private string goToViewName;
        public OrderSelectForMaterialNeedEditVM(MessageObject msg)
        {
            goToViewName = msg.ModelObject.ToString();

            SelectOrder = new RelayCommand<PMSMainService.DcOrder>(ActionSelectOrder);
        }
        private void ActionSelectOrder(DcOrder obj)
        {
            if (obj != null)
            {
                var materialNeed = ModelInitializer.GetMaterialNeedByOrder(obj);

                MessageObject mo = new PMSDesktopClient.MessageObject();
                mo.ViewName =goToViewName;
                mo.ModelObject = materialNeed;
                mo.IsAdd = true;

                NavigationService.GoToWithParameter(mo);
            }
        }
        public RelayCommand<DcOrder> SelectOrder { get; set; }
    }
}