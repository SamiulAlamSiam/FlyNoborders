using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FLYNOBORDERS.SelfB2B.Framework.Constant;
using FLYNOBORDERS.SelfB2B.Web.Framework.Bases;
using FLYNOBORDERS.SelfB2B.Web.Framework.Utils;

namespace FLYNOBORDERS.SelfB2B.Web.Controllers
{
    public class PackageDetailsController : BaseController
    {
        // GET: PackageDetails
        public ActionResult List(int pId)
        {
            if (HttpUtil.Current == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var pd = Context.PackagePictures.FirstOrDefault(p => p.PID == pId);
            return View(pd);
        }

        public ActionResult Book(int pId)
        {
            if (HttpUtil.Current == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var pd = Context.PackagePictures.FirstOrDefault(p => p.PID == pId);
            if (HttpUtil.Current.UserTypeID == (int) EnumCollection.UserTypeEnum.Admin)
            {
                return RedirectToAction("BookNow", "Admin", new { packageId = pId });
            }

            if (HttpUtil.Current.UserTypeID == (int)EnumCollection.UserTypeEnum.Agent)
            {
                return RedirectToAction("BookNow", "Agent", new {packageId = pId});
            }
            return View(pd);
        }
    }
}