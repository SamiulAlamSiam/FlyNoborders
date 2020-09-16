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
    public class LoginModel
    {
        //  ====================== Company ====================================
        [Required]
        [EmailAddress]
        [DisplayName("Company Email")]
        public string CEmail { get; set; }


        //  ====================== User ==================================== 

        [Required]
        [DisplayName("Password")]
        public string Pass { get; set; }

    }
}
