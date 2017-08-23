using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionManage.Controllers
{
    public class UploadController : BaseController
    {
        [AllowAnonymous]
        // GET: BackStage/Upload
        public ActionResult UploadImg(HttpPostedFileWrapper file)
        {
            try
            {
                var localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/Img/User");
                var filePathName = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                file.SaveAs(Path.Combine(localPath, filePathName));
                return Json(new
                {
                    isSuccess = true,
                    ImgUrl = "http://" + Request.Url.Authority + "/Content/Img/User/" + filePathName
                });
            }
            catch
            {
                return Json(new
                {
                    isSuccess = false,
                    ImgUrl = ""
                });
            }
        }
        [AllowAnonymous]
        public ActionResult UploadFile(HttpPostedFileWrapper file)
        {
            try
            {
                var localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/File");
                var filePathName = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                file.SaveAs(Path.Combine(localPath, filePathName));
                return Json(new
                {
                    isSuccess = true,
                    Msg = "上传成功",
                    FileUrl = "http://" + Request.Url.Authority + "/Content/File/" + filePathName
                });
            }
            catch
            {
                return Json(new
                {
                    isSuccess = false,
                    Msg = "上传失败",
                    FileUrl = ""
                });
            }
        }
        [AllowAnonymous]
        public ActionResult UploadImg2(HttpPostedFileWrapper upload, string CKEditorFuncNum)
        {
            try
            {
                var localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/Img/User");
                var filePathName = DateTime.Now.ToString("yyyyMMddHHmmss") + upload.FileName;
                upload.SaveAs(Path.Combine(localPath, filePathName));
                var imgUrl = "http://" + Request.Url.Authority + "/Content/Img/User/" + filePathName;

                Response.Write("<script type=\"text/javascript\">window.parent.CKEDITOR.tools.callFunction(" +
                               CKEditorFuncNum + ",'" + imgUrl + "','') </script>");

                return Content("上传成功！");
            }
            catch
            {
                return Content("上传失败！");
            }
        }
    }
}