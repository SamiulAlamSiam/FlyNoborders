using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using FLYNOBORDERS.SelfB2B.Entities;
using FLYNOBORDERS.SelfB2B.Framework.Constant;
using FLYNOBORDERS.SelfB2B.Framework.Object;
using FLYNOBORDERS.SelfB2B.Web.Framework.Attributes;
using FLYNOBORDERS.SelfB2B.Web.Framework.Bases;
using FLYNOBORDERS.SelfB2B.Web.Framework.Utils;

namespace FLYNOBORDERS.SelfB2B.Web.Controllers
{
    [SELFB2BAuthorize(EnumCollection.UserTypeEnum.Admin)]
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (HttpUtil.Current == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult UserProfile()
        {
            var result = UserInfoRepo.GetById(HttpUtil.Current.ID);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return RedirectToAction("Index", "Admin");
            }

            var userProfile = new UserInfo();

            return View(result);
        }

        [HttpPost]
        public ActionResult UserProfile(Result<UserInfo> userInfo, string pass, string cPass)
        {
            userInfo.Data.Pass = pass;
            /*if (!ModelState.IsValid)
            {
                return View(userInfo);
            }*/

            if (!pass.Equals(cPass))
            {
                return Content("Password and Confirm Password Should be Matched");
            }
            //userInfo.Data.Pass = pass;
            var result = UserInfoRepo.Save(userInfo.Data);

            if (result.HasError)
            {
                userInfo.HasError = true;
                userInfo.Message = result.Message;
                return View(userInfo);
            }

            return RedirectToAction("Index");
        }

        /*[HttpPost]
        public ActionResult UserProfile(string name)
        {
            return Content(name);
        }*/

        public ActionResult AcceptPendingAgent(int pendingAgentId)
        {
            if (MessageBox.Show("Are You Sure ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                return RedirectToAction("List", "AgentInfo");

            var result = UserInfoRepo.AcceptByAdmin(pendingAgentId);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return RedirectToAction("List", "AgentInfo");
            }
            return RedirectToAction("List", "AgentInfo");
        }


    }
}