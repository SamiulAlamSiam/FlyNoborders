using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FLYNOBORDERS.SelfB2B.Data;
using FLYNOBORDERS.SelfB2B.Repo;

namespace FLYNOBORDERS.SelfB2B.Web.Framework.Bases
{
    public class BaseController : Controller
    {
        private SelfB2BDBContext _Context;

        public SelfB2BDBContext Context
        {
            get
            {
                if (_Context == null)
                    _Context = new SelfB2BDBContext();

                return _Context;
            }
        }



        private UserInfoRepo _UserInfoRepo;

        public UserInfoRepo UserInfoRepo
        {
            get
            {
                if (_UserInfoRepo == null)
                    _UserInfoRepo = new UserInfoRepo();

                return _UserInfoRepo;
            }
        }


        private DepositeRepo _DepositeRepo;

        public DepositeRepo DepositeRepo
        {
            get
            {
                if (_DepositeRepo == null)
                    _DepositeRepo = new DepositeRepo();

                return _DepositeRepo;
            }
        }


        private PackagePictureRepo _PackagePictureRepo;

        public PackagePictureRepo PackagePictureRepo
        {
            get
            {
                if (_PackagePictureRepo == null)
                    _PackagePictureRepo = new PackagePictureRepo();

                return _PackagePictureRepo;
            }
        }


        private BookingRepo _BookingRepo;

        public BookingRepo BookingRepo
        {
            get
            {
                if (_BookingRepo == null)
                    _BookingRepo = new BookingRepo();

                return _BookingRepo;
            }
        }

    }
}
