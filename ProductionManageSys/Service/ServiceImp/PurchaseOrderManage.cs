using Domain.Model;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceImp
{
    public class PurchaseOrderManage: RepositoryBase<Domain.Model.PurchaseOrder> , IService.IPurchaseOrderManage
    {
        public override bool Update(PurchaseOrder entity, bool IsCommit = true)
        {
            _Context.Set<PurchaseOrder>().Attach(entity);
            _Context.Entry<PurchaseOrder>(entity).State = System.Data.Entity.EntityState.Modified;
            
            var list = _Context.Set<PurchaseOrderDetail>().Where(o => o.PurchaseOrderId == entity.ID);

            foreach (var detail in entity.PurchaseOrderDetail)
            {
                
                if (_Context.Set<PurchaseOrderDetail>().Any(o => o.ProductId == detail.ProductId && o.PurchaseOrderId==detail.PurchaseOrderId))
                {
                    _Context.Entry<PurchaseOrderDetail>(detail).State = System.Data.Entity.EntityState.Modified;
                }else
                {
                    _Context.Entry<PurchaseOrderDetail>(detail).State = System.Data.Entity.EntityState.Added;
                }
            }
            foreach (var detail in list)
            {
                if (_Context.Entry<PurchaseOrderDetail>(detail).State==System.Data.Entity.EntityState.Unchanged)
                {
                      _Context.Entry<PurchaseOrderDetail>(detail).State = System.Data.Entity.EntityState.Deleted;
                }
            }

            if (IsCommit)
                return _Context.SaveChanges() > 0;
            else
                return false;
        }
    }
}
