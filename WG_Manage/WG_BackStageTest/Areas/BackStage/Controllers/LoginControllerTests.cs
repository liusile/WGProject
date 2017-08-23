using Microsoft.VisualStudio.TestTools.UnitTesting;
using WG_BackStage.Areas.BackStage.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.ServiceImp.BackStage;
using Domain.Models.BackStage;
using System.Web.Mvc;
using Moq;
using Service.IService.BackStage;
using System.Diagnostics;

namespace WG_BackStage.Areas.BackStage.Controllers.Tests
{
    [TestClass()]
    public class LoginControllerTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            var loginCrl = new LoginController();
            var mock = new Mock<IUserInfoManage>();
            mock.Setup(o => o.UserLogin("123", "123")).Returns(new UserInfo { UserName = "123" });
            loginCrl.UserInfoBz = mock.Object;
            var ro = loginCrl.Login("123", "123",false);
            var result = loginCrl.Login("123", "123", false) as RedirectToRouteResult;
            Assert.IsNotNull(result);
        }
    }
}