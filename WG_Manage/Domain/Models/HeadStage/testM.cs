using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.HeadStage
{
   public class testM
    {
        [Key]
        public int ID { get; set; }
       
        public string AtrName { get; set; }
        public List<testD> testDList { get; set; }
    }

    public class testD
    {
        [Key]
        public int ID { get; set; }

        public int dfdf { get; set; }

        public string AtrNamedd { get; set; }
    }
}
