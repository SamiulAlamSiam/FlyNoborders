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
using FLYNOBORDERS.SelfB2B.Web.Framework.ViewModel;

namespace FLYNOBORDERS.SelfB2B.Web.Controllers
{
    [SELFB2BAuthorize(EnumCollection.UserTypeEnum.Agent)]
    public class AgentController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            var userInfo = Context.UserInfos.FirstOrDefault(u => u.UID == HttpUtil.Current.ID);
            return View(userInfo);
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

        public ActionResult Reports()
        {
            return Content("Reports");
        }

        public ActionResult SearchPad(int packagePageNo = 1, int hotelPageNo = 1)
        {
            if (packagePageNo < 1)
                packagePageNo = 1;
            if (hotelPageNo < 1)
                hotelPageNo = 1;

            var pp = Context.PackagePictures.OrderByDescending(p => p.PID).Skip((packagePageNo - 1) * 4).Take(4).ToList();

            var hp = Context.HotelPictures.OrderByDescending(h => h.HID).Skip((hotelPageNo - 1) * 4).Take(4).ToList();



            var hpVM = new PackageHotelPictureVM()
            {
                PackagePictures = pp,
                HotelPictures = hp
            };

            ViewBag.PackageNext = packagePageNo + 1;
            ViewBag.PackagePrev = packagePageNo - 1;

            ViewBag.HotelNext = hotelPageNo + 1;
            ViewBag.HotelPrev = hotelPageNo - 1;

            return View(hpVM);
        }

        public ActionResult BookNow(int packageId)
        {
            var pd = Context.PackagePictures.FirstOrDefault(p => p.PID == packageId);
            //var ad = Context.UserInfos.FirstOrDefault(u => u.UID == HttpUtil.Current.ID);

            var puVM = new PackageUserInfoVM()
            {
                PackagePicture = pd,
                //UserInfo = ad
            };
            return View(pd);
        }

        [HttpPost]
        public ActionResult BookNow(PackagePicture packagePicture)
        {
            var ad = Context.UserInfos.FirstOrDefault(u => u.UID == HttpUtil.Current.ID);

            var result = BookingRepo.MinusBalance(HttpUtil.Current.ID, packagePicture.Price);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return Content(ViewBag.Error);
            }

            return Content("Successfully Booking Done");
        }


    }
}