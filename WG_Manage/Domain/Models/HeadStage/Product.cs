using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.HeadStage
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "代号")]
        public string Code { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "产品介绍")]
        public string Describe { get; set; }
        [Display(Name = "使用说明")]
        public string Instructions { get; set; }
        [Display(Name = "产品图片1")]
        public string ImgUrl1 { get; set; }
        [Display(Name = "产品图片1说明")]
        public string ImgDescribe1 { get; set; }
        [Display(Name = "产品图片2")]
        public string ImgUrl2 { get; set; }
        [Display(Name = "产品图片2说明")]
        public string ImgDescribe2 { get; set; }
        [Display(Name = "产品图片3")]
        public string ImgUrl3 { get; set; }
        [Display(Name = "产品图片3说明")]
        public string ImgDescribe3 { get; set; }
        [Display(Name = "产品图片4")]
        public string ImgUrl4 { get; set; }
        [Display(Name = "产品图片4说明")]
        public string ImgDescribe4 { get; set; }
        [Display(Name = "产品属性")]
        public List<ProductAttribute> ProductAttributeList { get; set; }
    }
}
