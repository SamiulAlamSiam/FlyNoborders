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
    public partial class HotelPicture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HID { get; set; }

        [Required]
        [DisplayName("Hotel Name")]
        [StringLength(50)]
        public string HName { get; set; }

        [DisplayName("Hotel Description")]
        public string HDesc { get; set; }

        [Required]
        public string Image { get; set; }

    }
}
