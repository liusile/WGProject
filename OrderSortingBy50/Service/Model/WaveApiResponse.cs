using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model
{
    public class WaveApiResponse
    {
        public bool isSuccess { get; set; }
        public List<WaveApi> data { get; set; }
    }
}
