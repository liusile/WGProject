﻿using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceImp
{
    public class SupplierProductManage : RepositoryBase<Domain.Model.SupplierProduct> , IService.ISupplierProductManage
    {
    }
}
