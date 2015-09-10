using Skeleton.BLL.BL;
using Skeleton.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skeleton.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /Entity/

        public ActionResult Index(int id)
        {
            var userBL = new UserBL();
            var result = userBL.GetRentUser(id);
            if (result.Data.UserID == 0)
            {
                Redirect("/Login");
            }
            return View(result.Data);
        }

        public ActionResult RentVideo(RentVideoModel model)
        {
            var result = new StatusModel();

            var rentBL = new RentBL();
            result = rentBL.RentVideo(model);

            return Json(result);
        }

        public ActionResult ReturnUserVideo(int userId)
        {
            var result = new StatusModel();

            var rentBL = new RentBL();
            result = rentBL.ReturnUserVideo(userId);

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReturnVideo(RentVideoModel model)
        {
            var result = new StatusModel();

            var rentBL = new RentBL();
            result = rentBL.ReturnVideo(model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
