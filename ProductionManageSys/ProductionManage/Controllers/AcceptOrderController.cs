using Domain.Model;
using ProductionManage.Models;
using Service;
using Service.IService;
using Service.Model;
using Service.ServiceImp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionManage.Controllers
{
    public class AcceptOrderController : BaseController
    {
        public IAcceptOrderManage AcceptOrderManageBz = new AcceptOrderManage();
        public IProductManage ProductManageBz = new ProductManage();
        public IProductLogManage ProductLogManageBz = new ProductLogManage();
        public IPurchaseOrderManage PurchaseOrderManageBz = new PurchaseOrderManage();
        public ISupplier SupplierManageBz = new SupplierManage();
        // GET: AcceptOrder
        public ActionResult Index(int p = 1, string searchFild = "")
        {
            var model = AcceptOrderManageBz.Query(
                            AcceptOrderManageBz.LoadAll(o => o.AcceptOrderNo.Contains(searchFild)).OrderByDescending(o => o.ID),
                            p,
                            10);
            ViewBag.searchFild = searchFild;

            return View(model);
        }
        public ActionResult AcceptAdd()
        {
            var acceptorder = new AcceptOrderView();
            return View("AcceptEidt", acceptorder);
        }
        public ActionResult AcceptDetail(string AcceptOrderNo = "")
        {

            var model = AcceptOrderManageBz.Get(o => o.AcceptOrderNo == AcceptOrderNo);
            var purchaseOrder = PurchaseOrderManageBz.Get(o => o.PurchaseOrderNo == model.PurchaseOrderNo).PurchaseOrderDetail;
            if (model == null || purchaseOrder == null)
            {
                return HttpNotFound();
            }

            var acceptorder = new AcceptOrderView
            {
                AcceptDate = model.AcceptDate,
                Accepter = model.Accepter,
                AcceptNum = model.AcceptNum,
                AcceptOrderNo = model.AcceptOrderNo,
                Oper = model.Oper,
                ID = model.ID,
                PurchaseOrderNo = model.PurchaseOrderNo,
                Remark = model.Remark,
                Status = model.Status,
                SumPrice = model.SumPrice,
                AcceptOrderDetailView = new List<AcceptOrderDetailView>()
            };
            foreach (var row in purchaseOrder)
            {
                var acceptOrderDetail = model.AcceptOrderDetail.Find(o => o.ProductId == row.ProductId);
                acceptorder.AcceptOrderDetailView.Add(
                    new AcceptOrderDetailView
                    {
                        AcceptNum = acceptOrderDetail?.AcceptNum ?? 0,
                        AcceptOrderId = acceptOrderDetail?.AcceptOrderId ?? 0,
                        Price = row.Price,
                        ProductId = row.ProductId,
                        ProductName = row.ProductName,
                        Remark = row.Remark,
                        SumPrice = acceptOrderDetail?.SumPrice ?? 0,
                        PurchaseNum = row.PurchaseNum,
                        PurchaseNumWait = purchaseOrder.Find(o => o.ProductId == row.ProductId).PurchaseNumWait
                    });
            }
            return View(acceptorder);
        }
        
        public ActionResult AcceptEidt(string AcceptOrderNo = "", string Msg = "")
        {
            var model = AcceptOrderManageBz.Get(o => o.AcceptOrderNo == AcceptOrderNo);
            var purchaseOrder = PurchaseOrderManageBz.Get(o => o.PurchaseOrderNo == model.PurchaseOrderNo).PurchaseOrderDetail;
            if (model == null || purchaseOrder == null)
            {
                return HttpNotFound();
            }
            if (model != null && model.Status != OrderStatus.未审批)
            {
                TempData["Msg"] = "此订单非未审批状态，无法修改！";
                return RedirectToAction("Index");
            }

            var acceptorder = new AcceptOrderView
            {
                AcceptDate = model.AcceptDate,
                Accepter = model.Accepter,
                AcceptNum = model.AcceptNum,
                AcceptOrderNo = model.AcceptOrderNo,
                Oper = model.Oper,
                ID = model.ID,
                PurchaseOrderNo = model.PurchaseOrderNo,
                Remark = model.Remark,
                Status = model.Status,
                SumPrice = model.SumPrice,
                AcceptOrderDetailView = new List<AcceptOrderDetailView>()
            };
            foreach (var row in purchaseOrder)
            {
                var acceptOrderDetail = model.AcceptOrderDetail.Find(o => o.ProductId == row.ProductId);
                acceptorder.AcceptOrderDetailView.Add(
                    new AcceptOrderDetailView
                    {
                        AcceptNum = acceptOrderDetail?.AcceptNum??0,
                        AcceptOrderId = acceptOrderDetail?.AcceptOrderId??0,
                        Price = row.Price,
                        ProductId = row.ProductId,
                        ProductName = row.ProductName,
                        Remark = row.Remark,
                        SumPrice = acceptOrderDetail?.SumPrice??0,
                        PurchaseNum = row.PurchaseNum,
                        PurchaseNumWait = purchaseOrder.Find(o => o.ProductId == row.ProductId).PurchaseNumWait
                    });
            }
            ViewBag.Msg = Msg;
            return View(acceptorder);
        }
        public ActionResult Save(AcceptOrderView model)
        {
            try
            {
                var AcceptOrderView = model.AcceptOrderNo ?? "";
                //验证
                if (string.IsNullOrEmpty(model.PurchaseOrderNo))
                {
                    ViewBag.Msg = "请选择需验收的采购订单";
                    return View("AcceptEidt", model);
                }
                if (string.IsNullOrEmpty(model.AcceptDate))
                {
                    ViewBag.Msg = "请选择验收日期";
                    return View("AcceptEidt", model);
                }

               


                var purchaseModel = PurchaseOrderManageBz.Get(o => o.PurchaseOrderNo == model.PurchaseOrderNo);
                if (string.IsNullOrWhiteSpace(AcceptOrderView))//新增
                {
                    model.AcceptOrderNo = Common.Utils.GetOrderNumber();
                    while (AcceptOrderManageBz.IsExist(o => o.AcceptOrderNo == model.AcceptOrderNo))
                    {
                        model.AcceptOrderNo = Common.Utils.GetOrderNumber();
                    }
                    
                    var acceptOrder = AcceptOrderViewToAcceptOrder(model);
                    if (model.AcceptNum <= 0)
                    {
                        ViewBag.Msg = "请输入验收数量";
                        return View("AcceptEidt", model);
                    }
                    acceptOrder.Oper = CurrentUser.UserName;
                    bool isSuccess = AcceptOrderManageBz.Save(acceptOrder);
                    ViewBag.Msg = isSuccess ? "保存成功！" : "保存失败！";

                }
                else
                {
                    var acceptOrder = AcceptOrderViewToAcceptOrder(model);

                    var oldData = AcceptOrderManageBz.Get(o => o.AcceptOrderNo == model.AcceptOrderNo);
                    var newData = Common.Tools.CopyObjectValue(oldData, acceptOrder);
                    newData.AcceptOrderDetail.ForEach(o => o.AcceptOrderId = newData.ID);
                    if (newData.AcceptNum <= 0)
                    {
                        ViewBag.Msg = "请输入验收数量";
                        return View("AcceptEidt", model);
                    }
                    if (newData.Status == OrderStatus.未审批)
                    {
                        //purchaseModel.PurchaseNumOver = purchaseModel.PurchaseNumOver + model.AcceptNum;
                        //purchaseModel.PurchaseNumWait = purchaseModel.PurchaseNumWait - model.AcceptNum;
                        //foreach (var row in acceptOrder.AcceptOrderDetail)
                        //{
                        //    var purchaseDetailModel = purchaseModel.PurchaseOrderDetail.Find(o => o.ProductId == row.ProductId);
                        //    purchaseDetailModel.PurchaseNumOver += row.AcceptNum;
                        //    purchaseDetailModel.PurchaseNumWait -= row.AcceptNum;
                        //}
                        newData.Oper = CurrentUser.UserName;
                        bool isSuccess = AcceptOrderManageBz.Update(newData);
                        ViewBag.Msg = isSuccess ? "保存成功！" : "保存失败！";
                    }
                    else
                    {
                        ViewBag.Msg = $"订单{newData.AcceptOrderNo}，修改失败！";
                    }
                }

                return RedirectToAction("AcceptEidt", new { AcceptOrderNo = model.AcceptOrderNo, Msg = ViewBag.Msg });
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
                return View("PurchaseEidt", model);
            }
        }
        private AcceptOrder AcceptOrderViewToAcceptOrder(AcceptOrderView  model)
        {
            model.Accepter = CurrentUser.UserName;
            model.Status = OrderStatus.未审批;

            float sumPriceTotal = 0;
            int sumNumTotal = 0;
            foreach (var detail in model.AcceptOrderDetailView)
            {
                float price = detail.Price;
                int num = detail.AcceptNum;
                float sumprice = price * num;
                sumPriceTotal += sumprice;
                sumNumTotal += num;
                detail.SumPrice = sumprice;
            }
            model.SumPrice = sumPriceTotal;
            model.AcceptNum = sumNumTotal;

            var acceptorder = new AcceptOrder
            {
                AcceptDate = model.AcceptDate,
                Accepter = model.Accepter,
                AcceptNum = model.AcceptNum,
                AcceptOrderNo = model.AcceptOrderNo,
                Oper = model.Oper,
                ID = model.ID,
                PurchaseOrderNo = model.PurchaseOrderNo,
                Remark = model.Remark,
                Status = model.Status,
                SumPrice = model.SumPrice,
                AcceptOrderDetail = new List<AcceptOrderDetail>()
            };
            foreach (var row in model.AcceptOrderDetailView)
            {
                acceptorder.AcceptOrderDetail.Add(
                    new AcceptOrderDetail
                    {
                        AcceptNum = row.AcceptNum,
                        AcceptOrderId = row.AcceptOrderId,
                        Price = row.Price,
                        ProductId = row.ProductId,
                        ProductName = row.ProductName,
                        Remark = row.Remark,
                        SumPrice = row.SumPrice  
                    });
            }
            return acceptorder;
        }
        public ActionResult AcceptDel(int id)
        {
            var model = AcceptOrderManageBz.Get(o => o.ID == id);
            if (model.Status != OrderStatus.未审批)
            {
                TempData["Msg"] = "此订单非未审批状态，无法删除！";
            }
            else
            {
                var isSuccess = AcceptOrderManageBz.Delete(o => o.ID == id);
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

        public ActionResult AcceptApprove(int id)
        {
            var model = AcceptOrderManageBz.Get(o => o.ID == id);
            if (model.Status != OrderStatus.未审批)
            {
                TempData["Msg"] = "此订单非未审批状态，无法审批！";
            }
            else
            {
                var uow=new UnitOfWork();
                PurchaseOrderManageBz.SetUOW(uow);
                AcceptOrderManageBz.SetUOW(uow);
                ProductManageBz.SetUOW(uow);
                ProductLogManageBz.SetUOW(uow);
                //更新采购单数量
                var purchaseOrder = PurchaseOrderManageBz.Get(o => o.PurchaseOrderNo == model.PurchaseOrderNo);
                purchaseOrder.PurchaseNumWait -= model.AcceptNum;
                purchaseOrder.PurchaseNumOver += model.AcceptNum;
                if (purchaseOrder.PurchaseNumWait < 0)
                {
                    TempData["Msg"] = "审批失败:验收数大于待采购数";
                    return RedirectToAction("Index");
                }else if (purchaseOrder.PurchaseNumWait == 0)
                {
                    purchaseOrder.Status =OrderStatus.已验收;
                }
                else
                {
                    purchaseOrder.Status = OrderStatus.部分验收;
                }
                foreach (var row in model.AcceptOrderDetail)
                {
                    var detail = purchaseOrder.PurchaseOrderDetail.Find(o => o.ProductId == row.ProductId);
                    detail.PurchaseNumOver += row.AcceptNum;
                    detail.PurchaseNumWait -= row.AcceptNum;
                }
                PurchaseOrderManageBz.Update(purchaseOrder, false);
                //新增库存

                foreach (var row in model.AcceptOrderDetail)
                {
                    var product = ProductManageBz.Get(o => o.ProductName == row.ProductName);
                    if (product == null)
                    {
                        ProductManageBz.Save(new Product {
                            From="验收单",
                            Location="",
                            LockNum=0,
                            Price=row.Price,
                            ProductName=row.ProductName,
                            SumNum=row.AcceptNum,
                            Type=ProductType.物料,
                            WaringMin=-1,
                            WaringMax= -1
                        }, false);
                    }
                    else
                    {
                        product.SumNum += row.AcceptNum;
                        ProductManageBz.Update(product, false);
                    } 
                }
                //库存记录(验收产品种类一般不多故分开结构清晰)
                foreach (var row in model.AcceptOrderDetail)
                {
                    if (row.AcceptNum > 0)
                    {
                        ProductLogManageBz.Save(new ProductLog
                        {
                            In1Out = "In",
                            Oper = CurrentUser.UserName,
                            OperTime = DateTime.Now,
                            num = row.AcceptNum,
                            ProductName = row.ProductName,
                            Remark = $"验收单号:{model.AcceptOrderNo}验收入库"
                        }, false);
                    }
                }
                 
                //审批验收单
                model.Status = OrderStatus.已审批;
                model.Oper = CurrentUser.UserName;
                bool isSuccess=AcceptOrderManageBz.Update(model);
                
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


    }
}