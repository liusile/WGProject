using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.HeadStage
{
    public  class News
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "关键字")]
        public string KeyWord { get; set; }
        [Display(Name = "图片")]
        public string ImgUrl { get; set; }
        [Display(Name = "年")]
        public string Year { get; set; }
        [Display(Name = "月")]
        public string Month { get; set; }
        [Display(Name = "日")]
        public string Day { get; set; }
        [Display(Name = "摘要")]
        public string summary { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
        [Display(Name = "内容")]
        public string CreatePeople { get; set; }
    }
}
