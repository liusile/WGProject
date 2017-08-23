using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.HeadStage
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "员工代号")]
        public string Code { get; set; }
        [Display(Name = "员工名称")]
        public string Name { get; set; }
        [Display(Name = "员工图片")]
        public string ImgUrl { get; set; }
        [Display(Name = "员工描述")]
        public string Describe { get; set; }
        [Display(Name = "是否生效")]
        public bool isValid { get; set; }
    }
}
