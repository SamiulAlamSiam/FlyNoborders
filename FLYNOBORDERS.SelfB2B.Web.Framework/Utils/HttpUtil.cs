using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FLYNOBORDERS.SelfB2B.Framework.Object;
using Newtonsoft.Json;

namespace FLYNOBORDERS.SelfB2B.Web.Framework.Utils
{
    public static class HttpUtil
    {
        public static UserProfile Current
        {
            get
            {
                try
                {
                    var userProfile = JsonConvert.DeserializeObject<UserProfile>(HttpContext.Current.User.Identity.Name);

                    return userProfile;
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }
    }
}
