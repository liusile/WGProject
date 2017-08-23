using Microsoft.VisualStudio.TestTools.UnitTesting;
using WG_BackStage.Areas.HeadStage.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.ServiceImp.HeadStage;
using System.Web.Mvc;
using Spring.Context.Support;
using Spring.Context;
using Service.IService.HeadStage;
using System.Web.Helpers;
using Domain.Models.HeadStage;

namespace WG_BackStage.Areas.HeadStage.Controllers.Tests
{
    [TestClass()]
    public class RecruitControllerTests
    {

        [TestMethod()]
        public void SaveTest()
        {
            var loginCrl = new RecruitController();
            IApplicationContext ctx = ContextRegistry.GetContext();
            loginCrl.ApplyJobBz = ctx.GetObject("Service.ApplyJob") as IApplyJob;

            var result = loginCrl.Save(new Domain.Models.HeadStage.ApplyJob
            {
                Age = "18",
                Name = "l",
                Status = 1,
                ApplyTime = "",
                Email = "",
                Talk = ""
            });
            Assert.AreEqual(new { Msg = "申请成功！" }.ToString(), result.Data.ToString());
        }
    }
}