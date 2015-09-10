using Skeleton.BLL.BL;
using Skeleton.BLL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skeleton.Controllers
{
    public class VideoController : Controller
    {
        //
        // GET: /Video/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetVideos(VideoSearchModel searchModel)
        {
            var result = new StatusModel<List<VideoDataTableModel>>();
            var videoBL = new VideoBL();
            result = videoBL.GetVideos(searchModel);

            if (result.Success)
            {
                //return Json(result.Data);
                return Json(new { data = result.Data }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new List<VideoModel>());
            }
        }

        //public ActionResult AddVideo(VideoModel model)
        //{
        //    var result = new StatusModel(true);

        //    //validation
        //    if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Director) || model.Year == 0 || string.IsNullOrWhiteSpace(model.Genre))
        //    {
        //        result.Success = false; result.Message = "Bad Parameters";
        //        return Json(result);
        //    }

        //    var videoBL = new VideoBL();
        //    result = videoBL.Add(model);

        //    return Json(result);
        //}

        public ActionResult EditVideo(VideoModel model)
        {
            var result = new StatusModel(true);

            //validation
            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Director) || model.Year == 0 || string.IsNullOrWhiteSpace(model.Genre))
            {
                result.Success = false; result.Message = "Bad Parameters";
                return Json(result);
            }

            var videoBL = new VideoBL();
            result = videoBL.Edit(model);

            return Json(result);
        }

        public ActionResult GetVideo(VideoModel model)
        {
            var result = new StatusModel<VideoModel>(true);

            if (model.ID != 0)
            {

                var videoBL = new VideoBL();
                result = videoBL.Get(model);
            }
            else
            {
                result.Data = new VideoModel();
            }

            return View("_Get", result.Data);
        }

        public ActionResult GetDeleteVideo(VideoModel model)
        {
            var result = new StatusModel<VideoModel>(true);

            if (model.ID != 0)
            {

                var videoBL = new VideoBL();
                result = videoBL.Get(model);
            }
            else
            {
                result.Data = new VideoModel();
            }

            return View("_GetDelete", result.Data);
        }

        public ActionResult RemoveVideo(VideoModel model)
        {
            var result = new StatusModel(true);

            var videoBL = new VideoBL();
            result = videoBL.Remove(model);

            return Redirect("/admin");
        }

        public ActionResult UploadThumb(int videoId)
        {
            var result = new StatusModel(true,"Upload Success");
                string fName = "";
            try
            {
                //saving file section
                HttpPostedFileBase file = Request.Files[0];
                //Save file content goes here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0 && videoId > 0)
                {

                    var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Thumbs", Server.MapPath(@"\")));

                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), videoId.ToString());

                    //var fileName1 = Path.GetFileName(file.FileName);

                    bool isExists = System.IO.Directory.Exists(pathString);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);

                    var path = string.Format("{0}\\{1}", pathString, file.FileName);
                    file.SaveAs(path);

                }
            }
            catch (Exception ex)
            {
                result.Success = false; result.Message = "Error Occured " + ex.Message;
            }
            if (result.Success )//image upload success - continue to save image name in DB
            {
                var videoBL = new VideoBL();
                result = videoBL.SaveThumbName(videoId,fName);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
