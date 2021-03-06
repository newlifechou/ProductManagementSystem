﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSDAL;
using System.ServiceModel;

namespace PMSIRepository
{
    /// <summary>
    /// 通用的泛型类接口用于WCF服务
    /// </summary>
    /// <typeparam name="CommonModel"></typeparam>
    [ServiceContract]
    public interface IRepositoryBase<CommonModel>
    {
        [OperationContract]
        IList<CommonModel> GetAll();

        [OperationContract]
        CommonModel FindById(Guid id);

        [OperationContract]
        IList<CommonModel> GetAllInPaging(int skip, int take);

        [OperationContract]
        int Add(CommonModel model);

        [OperationContract]
        int Update(CommonModel model);

        [OperationContract]
        int Delete(Guid id);
    }
}
