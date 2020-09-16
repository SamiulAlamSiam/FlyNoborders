using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLYNOBORDERS.SelfB2B.Entities
{
    public partial class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("User Email")]
        public string UEmail { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Pass { get; set; }

        [Required]
        public int TimeZone { get; set; }

        [DisplayName("PREFERRED ROE")]
        public int PreferredRoe { get; set; }

        public double Balance { get; set; }

        public int UserTypeID { get; set; }

        public int AdminVerifyID { get; set; }

        public int CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        public virtual CompanyInfo CompanyInfo { get; set; }
    }
}
