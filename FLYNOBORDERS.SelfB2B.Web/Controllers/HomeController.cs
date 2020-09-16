using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FLYNOBORDERS.SelfB2B.Web.Framework.Bases;
using FLYNOBORDERS.SelfB2B.Web.Framework.ViewModel;

namespace FLYNOBORDERS.SelfB2B.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Check()
        {
            var pp = Context.PackagePictures.OrderByDescending(p => p.PID).ToList();

            var hp = Context.HotelPictures.OrderByDescending(h => h.HID).ToList();

            var hpVM = new PackageHotelPictureVM()
            {
                PackagePictures = pp,
                HotelPictures = hp
            };

            return View(hpVM);
        }

        public ActionResult Index(int packagePageNo = 1, int hotelPageNo = 1)
        {
            if (packagePageNo < 1)
                packagePageNo = 1;
            if (hotelPageNo < 1)
                hotelPageNo = 1;

            var pp = Context.PackagePictures.OrderByDescending(p => p.PID).Skip((packagePageNo - 1)*4).Take(4).ToList();

            var hp = Context.HotelPictures.OrderByDescending(h => h.HID).Skip((hotelPageNo - 1)*4).Take(4).ToList();



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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Package()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Visa()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}