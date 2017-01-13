﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        List<DcOrder> GetAllOrderInPage(int skip, int take);
        [OperationContract]
        List<DcOrder> GetOrderBySearchInPage(int skip, int take, string customer, string compositionstd);
        [OperationContract]
        int GetOrderCountBySearch(string customer, string compositionstd);
        [OperationContract]
        int AddOrder(DcOrder order);
        [OperationContract]
        int UpdateOrder(DcOrder order);
        [OperationContract]
        int DeleteOrder(Guid id);
    }
}