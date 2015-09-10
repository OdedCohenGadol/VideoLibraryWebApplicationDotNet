using Skeleton.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skeleton.Controllers
{
    public class ModalController : Controller
    {
        //
        // GET: /Modal/

   
        public ActionResult Index(ModalModel model)
        {
            return View(model);
        }
    }
}
