using Domain.Model;
using Service.IService;
using Service.ServiceImp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionManage.Controllers
{
    public class BOMController : BaseController
    {
        public IBOMManage BOMBz = new BOMManage();
        // GET: ProductionBOM
        public ActionResult Index(int p = 1, string searchFild = "")
        {
            var model = BOMBz.Query(
                         BOMBz.LoadAll(o => o.ParentModuleID==0 && o.ModuleName.Contains(searchFild)).OrderByDescending(o => o.ID),
                         p,
                         10);
            ViewBag.searchFild = searchFild;
            return View(model);
        }
        public ActionResult BOMAdd()
        {
            return View();
        }
        public  ActionResult BOMEidt(string ModuleName)
        {
            var entity = BOMBz.LoadAll(o => o.TopModuleName== ModuleName).ToList();
            string data = "[";
            foreach (var row in entity)
            {
                data += " { id: "+row.ID+", pId: "+row.ParentModuleID+", name: \""+row.ModuleName+ "\"" +", num: \"" + row.ModuleNum + "\", open: true ,isProduct:true},";
                for(int i=0; i<row.ProductBom.Count;i++){
                    data += " { id: " + row.ProductBom[i].BOMID+i.ToString() + ", pId: " + row.ProductBom[i].BOMID + ", name: \"" + row.ProductBom[i].ProductName + "\""+", num: \"" + row.ProductBom[i].Num + "\" , open: true ,isProduct:true},";
                }
            }
            data = data.Substring(0, data.Length - 1) + "]";
            ViewBag.data = data;
            return View(entity);
        }

    }
}