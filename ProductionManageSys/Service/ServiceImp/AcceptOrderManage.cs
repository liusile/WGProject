using Domain.Model;
using Service.IService;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceImp
{
    public class AcceptOrderManage: RepositoryBase<Domain.Model.AcceptOrder> , IService.IAcceptOrderManage
    {

        public override bool Update(AcceptOrder entity, bool IsCommit = true)
        {
            _Context.Set<AcceptOrder>().Attach(entity);
            _Context.Entry<AcceptOrder>(entity).State = System.Data.Entity.EntityState.Modified;
            
            var list = _Context.Set<AcceptOrderDetail>().Where(o => o.AcceptOrderId == entity.ID);

            foreach (var detail in entity.AcceptOrderDetail)
            {
                
                if (_Context.Set<AcceptOrderDetail>().Any(o => o.ProductId == detail.ProductId && o.AcceptOrderId==detail.AcceptOrderId))
                {
                    _Context.Entry<AcceptOrderDetail>(detail).State = System.Data.Entity.EntityState.Modified;
                }else
                {
                    _Context.Entry<AcceptOrderDetail>(detail).State = System.Data.Entity.EntityState.Added;
                }
            }
            foreach (var detail in list)
            {
                if (_Context.Entry<AcceptOrderDetail>(detail).State==System.Data.Entity.EntityState.Unchanged)
                {
                      _Context.Entry<AcceptOrderDetail>(detail).State = System.Data.Entity.EntityState.Deleted;
                }
            }

            if (IsCommit)
                return _Context.SaveChanges() > 0;
            else
                return false;
        }
    }
}
