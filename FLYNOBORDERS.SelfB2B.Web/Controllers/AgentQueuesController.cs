using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FLYNOBORDERS.SelfB2B.Entities;
using FLYNOBORDERS.SelfB2B.Framework.Constant;
using FLYNOBORDERS.SelfB2B.Framework.Object;
using FLYNOBORDERS.SelfB2B.Web.Framework.Attributes;
using FLYNOBORDERS.SelfB2B.Web.Framework.Bases;
using FLYNOBORDERS.SelfB2B.Web.Framework.Utils;

namespace FLYNOBORDERS.SelfB2B.Web.Controllers
{
    [SELFB2BAuthorize(EnumCollection.UserTypeEnum.Agent)]
    public class AgentQueuesController : BaseController
    {
        // GET: Admin

        public ActionResult OnHold()
        {
            return Content("On Hold");
        }

        public ActionResult Pending()
        {
            return Content("Pending");
        }

        public ActionResult Ticketed()
        {
            return Content("Ticketed");
        }

        public ActionResult Cancelled()
        {
            return Content("Cancelled");
        }

        public ActionResult RefundManagement()
        {
            return Content("Refund Management");
        }

        public ActionResult VoidManagement()
        {
            return Content("Void Management");
        }


    }
}