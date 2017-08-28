using Domain.Models;
using Service.ServiceImp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Service.IService
{
    interface IButtonService
    {
        List<Button> CreateButtonList(AbstractSizeChangeContext sizeContext);
        void ReSizeButons(AbstractSizeChangeContext sizeContext);
        void UpdateButtons();
    }
}
