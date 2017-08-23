using Service.IService.HeadStage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceImp.HeadStage
{
    public class News: RepositoryBase<Domain.Models.HeadStage.News> , IService.HeadStage.INews
    {
    }
}
