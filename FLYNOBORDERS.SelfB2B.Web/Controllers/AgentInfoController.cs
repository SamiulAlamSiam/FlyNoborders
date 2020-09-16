using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using FLYNOBORDERS.SelfB2B.Entities;
using FLYNOBORDERS.SelfB2B.Framework.Constant;
using FLYNOBORDERS.SelfB2B.Web.Framework.Attributes;
using FLYNOBORDERS.SelfB2B.Web.Framework.Bases;
using FLYNOBORDERS.SelfB2B.Web.Framework.Utils;

namespace FLYNOBORDERS.SelfB2B.Web.Controllers
{
    [SELFB2BAuthorize(EnumCollection.UserTypeEnum.Admin)]
    public class AgentInfoController : BaseController
    {
        // GET: Admin
        public ActionResult List(string key = "")
        {
            /*if (MessageBox.Show("Are You Sure ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                return RedirectToAction("Index", "Admin");*/
            var result = UserInfoRepo.PendingAgent(key);
            return View(result);
        }

        public ActionResult PendingDeposite(string key = "")
        {
            /*if (MessageBox.Show("Are You Sure ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                return RedirectToAction("Index", "Admin");*/
            var result = DepositeRepo.GetAllPendingDeposite(key);
            return View(result);
        }

        public ActionResult AcceptPendingAgentDeposite(int pendingAgentId)
        {
            if (MessageBox.Show("Are You Sure ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                return RedirectToAction("PendingDeposite");

            var result = DepositeRepo.AcceptByAdmin(pendingAgentId);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return RedirectToAction("PendingDeposite");
            }
            return RedirectToAction("PendingDeposite");
        }

    }
}