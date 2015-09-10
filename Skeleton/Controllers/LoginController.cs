using Skeleton.BLL.BL;
using Skeleton.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skeleton.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(UserLoginModel model)
        {
            var result = new StatusModel<string>();
            var loginBL = new LoginBL();
            if (string.IsNullOrWhiteSpace(model.ID.ToString()) || string.IsNullOrWhiteSpace(model.Password))
            {
                result.Message = "Bad Prameteres";
                result.Success = false;
            }
            else
            {
                result = loginBL.Login(model);
            }

            return Json(result);
        }

    }
}
