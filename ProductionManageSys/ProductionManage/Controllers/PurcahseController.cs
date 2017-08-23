using Domain.Model;
using ProductionManage.Models;
using Service;
using Service.IService;
using Service.ServiceImp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionManage.Controllers
{
    public class PurcahseController : BaseController
    {
        public IPurchaseOrderManage PurchaseOrderManageBz = new PurchaseOrderManage();
        public ISupplier  SupplierManageBz = new SupplierManage();
        // GET: Purcahse  
        public ActionResult Index(int p = 1, string searchFild = "")
        {
            var model = PurchaseOrderManageBz.Query(
                            PurchaseOrderManageBz.LoadAll(o => o.PurchaseOrderNo.Contains(searchFild)).OrderByDescending(o => o.ID),
                            p,
                            10);
            ViewBag.searchFild = searchFild;
           
            return View(model);
        }
        public ActionResult PurchaseEidt(string PurchaseOrderNo = "",string Msg="")
        {
            var model = PurchaseOrderManageBz.Get(o => o.PurchaseOrderNo == PurchaseOrderNo);
            if (model!=null &&　model.Status != OrderStatus.未审批)
            {
                TempData["Msg"] = "此订单非未审批状态，无法修改！";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.GetSupplierList = GetSupplierList();
                ViewBag.Msg = Msg;
                return View(model);
            }
        }
        public ActionResult PurchaseDetail(string PurchaseOrderNo = "", string Msg = "")
        {
            var model = PurchaseOrderManageBz.Get(o => o.PurchaseOrderNo == PurchaseOrderNo);
            ViewBag.GetSupplierList = GetSupplierList();
            ViewBag.Msg = Msg;
            return View(model);
        }
        /// <summary>
        /// 供应商列表
        /// </summary>
        /// <returns></returns>
        public  List<SelectListItem> GetSupplierList()
        {
            var list= SupplierManageBz.LoadAll(o => true).Select(o => new SelectListItem { Text = o.CorpName, Value = o.CorpName.ToString() }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "请选择",
                Value = ""
            });
            return list;  
        }
        
        public ActionResult PurchaseDel(int id)
        {
            var model = PurchaseOrderManageBz.Get(o => o.ID == id);
            if (model.Status != OrderStatus.未审批)
            {
                TempData["Msg"] = "此订单非未审批状态，无法删除！";
            }
            else
            {
                var isSuccess = PurchaseOrderManageBz.Delete(o => o.ID == id);
                if (isSuccess)
                {
                    TempData["Msg"] = "删除成功！";
                }
                else
                {
                    TempData["Msg"] = "删除失败！";
                }
            }
            
            return RedirectToAction("Index");
        }
        public ActionResult Save(PurchaseOrder model)
        {
            try
            {
                var PurchaseOrderNo = model.PurchaseOrderNo ?? "";
                //验证
                if (string.IsNullOrEmpty(model.Supplier)){
                    ViewBag.Msg = "请选择供应商";
                    return View("PurchaseEidt", model);
                }
                if (string.IsNullOrEmpty(model.PurchaseDate))
                {
                    ViewBag.Msg = "请选择采购日期";
                    return View("PurchaseEidt", model);
                }
                if (string.IsNullOrEmpty(model.Supplier))
                {
                    ViewBag.Msg = "请选择采购日期";
                    return View("PurchaseEidt", model);
                }
               
                model.PurchaseOrderDetail = model.PurchaseOrderDetail?.Where(o => o.ProductName !=null && o.ProductName != "").ToList();
                if (model.PurchaseOrderDetail==null || !model.PurchaseOrderDetail.Any())
                {
                    ViewBag.Msg = "请选择需采购产品";
                    return View("PurchaseEidt", model);
                }

                if (string.IsNullOrWhiteSpace(PurchaseOrderNo))//新增
                {
                    model.PurchaseOrderNo = Common.Utils.GetOrderNumber();
                    while (PurchaseOrderManageBz.IsExist(o => o.PurchaseOrderNo == model.PurchaseOrderNo))
                    {
                        model.PurchaseOrderNo = Common.Utils.GetOrderNumber();
                    }
                    model.Purchaser = CurrentUser.UserName;
                    model.Status = OrderStatus.未审批;
                    float sumPriceTotal = 0;
                    int sumNumTotal = 0;
                    foreach (PurchaseOrderDetail detail in model.PurchaseOrderDetail)
                    {
                        float price = detail.Price;
                        int num = detail.PurchaseNum;
                        float sumprice = price * (num);
                        sumPriceTotal += sumprice;
                        sumNumTotal += (num);
                        detail.SumPrice = sumprice;
                        detail.PurchaseNumOver = 0;
                        detail.PurchaseNumWait = num;
                    }
                    model.SumPrice = sumPriceTotal;
                    model.PurchaseNum = sumNumTotal;
                    model.PurchaseNumWait = sumNumTotal;
                    model.Oper = CurrentUser.UserName;
                    bool isSuccess = PurchaseOrderManageBz.Save(model);
                    ViewBag.Msg = isSuccess ? "保存成功！" : "保存失败！";
                   
                }
                else
                {
                    var oldData = PurchaseOrderManageBz.Get(o => o.PurchaseOrderNo == model.PurchaseOrderNo);
                    var newData = Common.Tools.CopyObjectValue(oldData, model);
                    newData.PurchaseOrderDetail.ForEach(o => o.PurchaseOrderId = newData.ID);

                    newData.Status = OrderStatus.未审批;
                    float sumPriceTotal = 0;
                    int sumNumTotal = 0;
                    foreach (PurchaseOrderDetail detail in newData.PurchaseOrderDetail)
                    {
                        float price = detail.Price;
                        int num = detail.PurchaseNum;
                        float sumprice = price * num;
                        
                        sumPriceTotal += sumprice;
                        sumNumTotal += num;
                        detail.SumPrice = sumprice;
                        detail.PurchaseNumOver = 0;
                        detail.PurchaseNumWait = num;
                    }
                    
                    newData.SumPrice = sumPriceTotal;
                    newData.PurchaseNum = sumNumTotal;
                    newData.PurchaseNumWait = sumNumTotal;
                    newData.Oper = CurrentUser.UserName;
                    if (newData.Status == OrderStatus.未审批)
                    {
                        bool isSuccess = PurchaseOrderManageBz.Update(newData);
                        ViewBag.Msg = isSuccess ? "保存成功！" : "保存失败！";
                    }
                    else
                    {
                        ViewBag.Msg = $"订单{newData.PurchaseOrderNo}，修改失败！";
                    }
                }
               
                 return RedirectToAction("PurchaseEidt", new { PurchaseOrderNo = model.PurchaseOrderNo,Msg=ViewBag.Msg });
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
                return View("PurchaseEidt", model);
            }
        }
        
        public ActionResult PurchaseApprove(int id)
        {
            var model=PurchaseOrderManageBz.Get(o => o.ID == id);
            if (model.Status != OrderStatus.未审批)
            {
                TempData["Msg"] = "此订单非未审批状态，无法审批！";
            }
            else
            {
                model.Status = OrderStatus.待验收;
                model.Oper = CurrentUser.UserName;
                var isSuccess = PurchaseOrderManageBz.Update(model);
                if (isSuccess)
                {
                    TempData["Msg"] = "审批成功！";
                }
                else
                {
                    TempData["Msg"] = "审批失败！";
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PurchaseReject(int id)
        {
            var model = PurchaseOrderManageBz.Get(o => o.ID == id);
            if (model.Status != OrderStatus.待验收)
            {
                TempData["Msg"] = "此订单非待验收状态，无法驳回！";
            }
            else
            {
                model.Status = OrderStatus.未审批;
                model.Oper = CurrentUser.UserName;
                var isSuccess = PurchaseOrderManageBz.Update(model);
                if (isSuccess)
                {
                    TempData["Msg"] = "驳回成功！";
                }
                else
                {
                    TempData["Msg"] = "驳回失败！";
                }
            }
            return RedirectToAction("Index");
        }
 
    }
}