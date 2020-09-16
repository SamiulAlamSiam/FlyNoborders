using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLYNOBORDERS.SelfB2B.Framework.Object
{
    public class UserProfile
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UserTypeID { get; set; }

        public string UEmail { get; set; }

        public string CEmail { get; set; }

        public int PreferredRoe { get; set; }

        public string CName { get; set; }

        public string TradeLicenseNo { get; set; }

        public string Website { get; set; }

        public string PhnNumber { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int Zip { get; set; }
    }
}
