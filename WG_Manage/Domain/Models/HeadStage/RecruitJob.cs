using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.HeadStage
{
    /// <summary>
    /// 招聘职位
    /// </summary>
    public class RecruitJob
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "职位名称")]
        public string JobName { get; set; }
        [Display(Name = "任职要求1")]
        public string Condition1 { get; set; }
        [Display(Name = "任职要求2")]
        public string Condition2 { get; set; }
        [Display(Name = "任职要求3")]
        public string Condition3 { get; set; }
        [Display(Name = "任职要求4")]
        public string Condition4 { get; set; }
        [Display(Name = "任职要求5")]
        public string Condition5 { get; set; }

        [Display(Name = "职位描述1")]
        public string JobDetail1 { get; set; }
        [Display(Name = "职位描述2")]
        public string JobDetail2 { get; set; }
        [Display(Name = "职位描述3")]
        public string JobDetail3 { get; set; }
        [Display(Name = "职位描述4")]
        public string JobDetail4 { get; set; }
        [Display(Name = "职位描述5")]
        public string JobDetail5 { get; set; }

        [Display(Name = "工作地址")]
        public string Address { get; set; }
        [Display(Name = "排序")]
        public int Seq { get; set; }
        [Display(Name = "是否生效")]
        public bool isValid { get; set; } = false;
        public string CreatePeople { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
