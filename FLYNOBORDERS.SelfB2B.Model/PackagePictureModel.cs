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
    public class PackagePictureModel
    {
        [Required]
        [DisplayName("Package Name")]
        [StringLength(50)]
        public string PName { get; set; }

        [DisplayName("Package Price")]
        public double Price { get; set; }

        [Required]
        public string Image { get; set; }

    }
}
