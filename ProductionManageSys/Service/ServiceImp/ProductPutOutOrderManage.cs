using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceImp
{
    public  class ProductPutOutOrderManage : RepositoryBase<Domain.Model.ProductPutOutOrder>, IService.IProductPutOutOrderManage
    {
        public override bool Update(ProductPutOutOrder entity, bool IsCommit = true)
        {
            _Context.Set<ProductPutOutOrder>().Attach(entity);
            _Context.Entry<ProductPutOutOrder>(entity).State = System.Data.Entity.EntityState.Modified;

            var list = _Context.Set<ProductPutOutOrderDetail>().Where(o => o.ProductPutOutOrderId == entity.ID);

            foreach (var detail in entity.ProductOrderDetail)
            {

                if (_Context.Set<ProductPutOutOrderDetail>().Any(o => o.ProductPutOutOrderId == detail.ProductPutOutOrderId && o.ProductName == detail.ProductName))
                {
                    _Context.Entry<ProductPutOutOrderDetail>(detail).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    _Context.Entry<ProductPutOutOrderDetail>(detail).State = System.Data.Entity.EntityState.Added;
                }
            }
            foreach (var detail in list)
            {
                if (_Context.Entry<ProductPutOutOrderDetail>(detail).State == System.Data.Entity.EntityState.Unchanged)
                {
                    _Context.Entry<ProductPutOutOrderDetail>(detail).State = System.Data.Entity.EntityState.Deleted;
                }
            }

            if (IsCommit)
                return _Context.SaveChanges() > 0;
            else
                return false;
        }
    }
}
