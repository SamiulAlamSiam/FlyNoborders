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
    public partial class CompanyInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CID { get; set; }

        [Required]
        [StringLength(250)]
        [DisplayName("Company Name")]
        public string CName { get; set; }

        [Required]
        public string TradeLicenseNo { get; set; }

        [Required]
        [StringLength(150)]
        public string Website { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Company Email")]
        public string CEmail { get; set; }

        [Required]
        [DisplayName("Company Phone Number")]
        public string PhnNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(150)]
        public string City { get; set; }

        public int Zip { get; set; }
    }
}
