using Common;
using Domain.Models.HeadStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Service.IService.HeadStage
{
    public interface IProduct : IRepository<Domain.Models.HeadStage.Product>
    {
     //   Product  Get(Expression<Func<Product, bool>> PredicateBuilder);
    }
}
