using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.HeadStage
{
    public class Company
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "公司名称")]
        public string Name { get; set; }
        [Display(Name = "公司简介")]
        public string Generalization { get; set; }
        [Display(Name = "公司文化")]
        public string Culture { get; set; }
    }
}
