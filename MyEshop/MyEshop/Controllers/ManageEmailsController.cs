using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshop.Controllers
{
    public class ManageEmailsController : Controller
    {
        public ActionResult ActiviationEmail()
        {
            return PartialView();
        }

        public ActionResult RecoveryPassword()
        {
            return PartialView();
        }
    }
}