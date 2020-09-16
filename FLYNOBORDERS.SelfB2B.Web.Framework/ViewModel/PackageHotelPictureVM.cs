using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLYNOBORDERS.SelfB2B.Entities;

namespace FLYNOBORDERS.SelfB2B.Web.Framework.ViewModel
{
    public class PackageHotelPictureVM
    {
        public List<PackagePicture> PackagePictures { get; set; }

        public List<HotelPicture> HotelPictures { get; set; }
    }
}
