using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.HeadStage
{
    public class ProductAttribute
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "产品ID")]
        public int ProductID { get; set; }
        [Display(Name = "属性名称")]
        public string AtrName { get; set; }
        [Display(Name = "属性值")]
        public string AtrValue { get; set; }
       
    }
}
