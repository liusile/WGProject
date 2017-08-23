using Domain;
using Domain.Models.HeadStage;
using Service.IService.HeadStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceImp.HeadStage
{
    public class Product : RepositoryBase<Domain.Models.HeadStage.Product>, IService.HeadStage.IProduct
    {
        public override  Domain.Models.HeadStage.Product  Get(Expression<Func<Domain.Models.HeadStage.Product, bool>> PredicateBuilder)
        {
            return _Context.Product.Include("ProductAttributeList").FirstOrDefault(PredicateBuilder);
        }
    }
}
