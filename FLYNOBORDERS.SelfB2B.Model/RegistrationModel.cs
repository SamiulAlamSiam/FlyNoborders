using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLYNOBORDERS.SelfB2B.Model
{
    public class RegistrationModel
    {
        [Required]
        [StringLength(250)]
        [DisplayName("Company Name")]
        public string CName { get; set; }

        [Required]
        [Display(Name = "Trade License No")]
        public string TradeLicenseNo { get; set; }

        [Required]
        [StringLength(150)]
        public string Website { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Company Email")]
        public string CEmail { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhnNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(150)]
        public string City { get; set; }

        [DisplayName("Zip / Postal Code")]
        public int Zip { get; set; }


        //  ====================== User ==================================== 

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Personal Email")]
        public string UEmail { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Pass { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Pass")]
        public string CPass { get; set; }

        [Required]
        public int TimeZone { get; set; }

        [DisplayName("Preferred ROE")]
        public int PreferredRoe { get; set; }

    }
}
