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
    public class ProductPutOutOrderController : BaseController
    {
        public IProductManage ProductManageBz = new ProductManage();
        public IProductPutOutOrderManage ProductPutOutOrderManageBz = new ProductPutOutOrderManage ();
        public IProductLogManage ProductLogManageBz = new ProductLogManage();
        //入库
        public ActionResult PutIndex(int p = 1, string searchFild = "")
        {
            var model = ProductPutOutOrderManageBz.Query(
                           ProductPutOutOrderManageBz.LoadAll(o => o.Type=="入库" && o.ProductOrderNo.Contains(searchFild)).OrderByDescending(o => o.ID),
                           p,
                           10);
            ViewBag.searchFild = searchFild;
            return View(model);
        }
        public ActionResult PutDetail(string ProductOrderNo = "")
        {
            var model = ProductPutOutOrderManageBz.Get(o => o.ProductOrderNo == ProductOrderNo);
            return View(model);
        }
        public ActionResult PutEidt(string ProductOrderNo = "",string msg="")
        {
            var model = ProductPutOutOrderManageBz.Get(o=>o.ProductOrderNo == ProductOrderNo);
            ViewBag.Msg = msg;
            return View(model);
        }

        public ActionResult PutOrderApprove(int id)
        {
            var uow = new UnitOfWork();
            ProductManageBz.SetUOW(uow);
            ProductPutOutOrderManageBz.SetUOW(uow);
            ProductLogManageBz.SetUOW(uow);

            var model = ProductPutOutOrderManageBz.Get(o => o.ID == id);
            if (model.Status != OrderStatus.未审批)
            {
                TempData["Msg"] = "此订单非未审批状态，无法审批！";
            }
            else
            {
                foreach(var detail in model.ProductOrderDetail)
                {
                    var product = ProductManageBz.Get(o => o.ProductName == detail.ProductName);
                    if (product == null)//add
                    {
                        ProductManageBz.Save(new Product
                        {
                            From = "入库",
                            Location = "",
                            Price = detail.Price,
                            Type = "物料",
                            ProductName=detail.ProductName,
                            LockNum=0,
                            Remark="",
                            SumNum=detail.Num
                        },false);
                    }
                    else
                    {
                        product.SumNum += detail.Num;
                        ProductManageBz.Update(product,false);
                    }
                }
                //库存记录(验收产品种类一般不多故分开结构清晰)
                foreach (var row in model.ProductOrderDetail)
                {
                    if (row.Num > 0)
                    {
                        ProductLogManageBz.Save(new ProductLog
                        {
                            In1Out = "In",
                            Oper = CurrentUser.UserName,
                            OperTime = DateTime.Now,
                            num = row.Num,
                            ProductName = row.ProductName,
                            Remark = $"入库单号:{model.ProductOrderNo}审批入库"
                        }, false);
                    }
                }
                model.Status = OrderStatus.已审批;
                model.Oper = CurrentUser.UserName;
                var isSuccess = ProductPutOutOrderManageBz.Update(model);
                if (isSuccess)
                {
                    TempData["Msg"] = "审批成功！";
                }
                else
                {
                    TempData["Msg"] = "审批失败！";
                }
            }
            return RedirectToAction("PutIndex");
        }
        public ActionResult PutOrderReject(int id)
        {
            var uow = new UnitOfWork();
            ProductManageBz.SetUOW(uow);
            ProductPutOutOrderManageBz.SetUOW(uow);
            ProductLogManageBz.SetUOW(uow);

            var model = ProductPutOutOrderManageBz.Get(o => o.ID == id);
            if (model.Status != OrderStatus.已审批)
            {
                TempData["Msg"] = "此订单非已审批状态，无法驳回！";
            }
            else
            {
                foreach (var detail in model.ProductOrderDetail)
                {
                    var product = ProductManageBz.Get(o => o.ProductName == detail.ProductName);
                    if (product == null || product.SumNum<detail.Num)//add
                    {
                        TempData["Msg"] = "库存数量不足，无法驳回！";
                        return RedirectToAction("PutIndex");
                    }
                    else
                    {
                        product.SumNum -= detail.Num;
                        ProductManageBz.Update(product, false);
                    }
                }
                //库存记录(验收产品种类一般不多故分开结构清晰)
                foreach (var row in model.ProductOrderDetail)
                {
                    if (row.Num > 0)
                    {
                        ProductLogManageBz.Save(new ProductLog
                        {
                            In1Out = "Out",
                            Oper = CurrentUser.UserName,
                            OperTime = DateTime.Now,
                            num = row.Num,
                            ProductName = row.ProductName,
                            Remark = $"入库单号:{model.ProductOrderNo}驳回入库"
                        }, false);
                    }
                }
                model.Status = OrderStatus.未审批;
                model.Oper = CurrentUser.UserName;
                var isSuccess = ProductPutOutOrderManageBz.Update(model);
                if (isSuccess)
                {
                    TempData["Msg"] = "驳回成功！";
                }
                else
                {
                    TempData["Msg"] = "驳回失败！";
                }
            }
            return RedirectToAction("PutIndex");
        }


        public ActionResult PutOrderDel(int id)
        {
            var model = ProductPutOutOrderManageBz.Get(o => o.ID == id);
            if (model.Status != OrderStatus.未审批)
            {
                TempData["Msg"] = "此订单非未审批状态，无法删除！";
            }
            else
            {
                var isSuccess = ProductPutOutOrderManageBz.Delete(o => o.ID == id);
                if (isSuccess)
                {
                    TempData["Msg"] = "删除成功！";
                }
                else
                {
                    TempData["Msg"] = "删除失败！";
                }
            }

            return RedirectToAction("PutIndex");
        }
        public ActionResult PutSave(ProductPutOutOrder model)
        {
            try
            {
                var ProductOrderNo = model.ProductOrderNo ?? "";
                //验证
               
                if (model.ProductPutOrderDate<new DateTime(2017,1,1))
                {
                    ViewBag.Msg = "请选择订单日期";
                    return View("PutEidt", model);
                }

                model.ProductOrderDetail = model.ProductOrderDetail?.Where(o => o.ProductName != null && o.ProductName != "").ToList();
                if (model.ProductOrderDetail == null || !model.ProductOrderDetail.Any())
                {
                    ViewBag.Msg = "请添加入库产品";
                    return View("PutEidt", model);
                }

                if (string.IsNullOrWhiteSpace(ProductOrderNo))//新增
                {
                    model.ProductOrderNo = Common.Utils.GetOrderNumber();
                    while (ProductPutOutOrderManageBz.IsExist(o => o.ProductOrderNo == model.ProductOrderNo))
                    {
                        model.ProductOrderNo = Common.Utils.GetOrderNumber();
                    }
                    model.Oper = CurrentUser.UserName;
                    model.Status = OrderStatus.未审批;
                    float sumPriceTotal = 0;
                    int sumNumTotal = 0;
                    foreach (var detail in model.ProductOrderDetail)
                    {
                        float price = detail.Price;
                        int num = detail.Num;
                        float sumprice = price * (num);
                        sumPriceTotal += sumprice;
                        sumNumTotal += (num);
                        detail.SumPrice = sumprice;
                    }
                    model.SumPrice = sumPriceTotal;
                    model.SumNum = sumNumTotal;
                    model.Type = "入库";

                    bool isSuccess = ProductPutOutOrderManageBz.Save(model);
                    ViewBag.Msg = isSuccess ? "保存成功！" : "保存失败！";

                }
                else
                {
                    var oldData = ProductPutOutOrderManageBz.Get(o => o.ProductOrderNo == model.ProductOrderNo);
                    var newData = Common.Tools.CopyObjectValue(oldData, model);
                    newData.ProductOrderDetail.ForEach(o => o.ProductPutOutOrderId = newData.ID);

                    newData.Status = OrderStatus.未审批;
                    float sumPriceTotal = 0;
                    int sumNumTotal = 0;
                    foreach (var detail in newData.ProductOrderDetail)
                    {
                        float price = detail.Price;
                        int num = detail.Num;
                        float sumprice = price * num;

                        sumPriceTotal += sumprice;
                        sumNumTotal += num;
                        detail.SumPrice = sumprice;
                    }
                    newData.SumPrice = sumPriceTotal;
                    newData.SumNum = sumNumTotal;

                    newData.SumPrice = sumPriceTotal;
                    newData.Oper = CurrentUser.UserName;
                    if (newData.Status == OrderStatus.未审批)
                    {
                        bool isSuccess = ProductPutOutOrderManageBz.Update(newData);
                        ViewBag.Msg = isSuccess ? "保存成功！" : "保存失败！";
                    }
                    else
                    {
                        ViewBag.Msg = $"订单{newData.ProductOrderNo}，修改失败！";
                    }
                }

                return RedirectToAction("PutEidt", new { ProductOrderNo = model.ProductOrderNo, Msg = ViewBag.Msg });
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
                return View("PutEidt", model);
            }
        }
      
    }
}