using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.HeadStage
{
    /// <summary>
    /// 申请职位表
    /// </summary>
    public class ApplyJob
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "申请人")]
        public string Name { get; set; }
        [Display(Name = "年龄")]
        public string Age { get; set; }
        [Display(Name = "邮箱")]
        public string Email { get; set; }
        [Display(Name = "申请职位")]
        public string ApplyJobName { get; set; }
        [Display(Name = "申请人详细说明")]
        public string Talk { get; set; }
        [Display(Name = "申请时间")]
        public string ApplyTime { get; set; } = DateTime.Now.ToString();
        [Display(Name = "状态 0:未阅，1已阅，2，未通过，3，通过")]
        public int Status { get; set; }
        [Display(Name = "审核人")]
        public string ApprovePepple { get; set; }
        [Display(Name = "审核时间")]
        public string ApproveTime { get; set; }
    }
}
