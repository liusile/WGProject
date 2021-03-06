// <copyright file="LoginControllerTest.cs" company="Microsoft">Copyright © Microsoft 2016</copyright>
using System;
using System.Web.Mvc;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WG_BackStage.Areas.BackStage.Controllers;

namespace WG_BackStage.Areas.BackStage.Controllers.Tests
{
    /// <summary>此类包含 LoginController 的参数化单元测试</summary>
    [PexClass(typeof(LoginController))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class LoginControllerTest
    {
        ///// <summary>测试 Login(String, String) 的存根</summary>
        //[PexMethod]
        //public ActionResult LoginTest(
        //    [PexAssumeUnderTest]LoginController target,
        //    string UserName,
        //    string Pwd
        //)
        //{
        //    ActionResult result = target.Login(UserName, Pwd);
        //    return result;
        //    // TODO: 将断言添加到 方法 LoginControllerTest.LoginTest(LoginController, String, String)
        //}
    }
}
