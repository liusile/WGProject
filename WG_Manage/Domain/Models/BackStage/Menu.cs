using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BackStage
{
    public class Menu
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "菜单名称")]
        public string MenuName { get; set; }
        [Display(Name = "是否生效")]
        public bool IsValid { get; set; }
        [Display(Name = "序号")]
        public int Seq { get; set; }
        [Display(Name = "父类")]
        public int ParentID { get; set; }
        [Display(Name = "菜单地址")]
        public string  MenuAddress { get; set; }

    }
}
