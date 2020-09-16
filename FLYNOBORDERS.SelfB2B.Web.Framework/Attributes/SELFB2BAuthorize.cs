using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FLYNOBORDERS.SelfB2B.Framework.Constant;
using FLYNOBORDERS.SelfB2B.Web.Framework.Utils;

namespace FLYNOBORDERS.SelfB2B.Web.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class SELFB2BAuthorize : FilterAttribute, IAuthorizationFilter
    {
        private EnumCollection.UserTypeEnum CurrentType { get; set; }

        public SELFB2BAuthorize(EnumCollection.UserTypeEnum type)
        {
            CurrentType = type;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated || HttpUtil.Current == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            if (HttpUtil.Current.UserTypeID != (int) CurrentType)
            {
                if (HttpUtil.Current.UserTypeID == (int) EnumCollection.UserTypeEnum.Admin)
                {
                    filterContext.Result=new RedirectToRouteResult(new RouteValueDictionary(
                    new
                    {
                        Controller = "Admin",
                        Action = "Index"
                    }));
                    return;
                }

                if (HttpUtil.Current.UserTypeID == (int) EnumCollection.UserTypeEnum.Agent)
                {
                    filterContext.Result=new RedirectToRouteResult(new RouteValueDictionary(
                        new
                        {
                            Controller = "Agent",
                            Action = "Index"
                        }));
                }
            }
        }

        
    }
}
