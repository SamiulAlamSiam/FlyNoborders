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
    public class DepositeModel
    {
        [Required]
        [DisplayName("Deposite Type")]
        public string DType { get; set; }

        [Required]
        [DisplayName("Cheque No")]
        public string ChequeNo { get; set; }

        [Required]
        [DisplayName("Reference Number")]
        public string RefNum { get; set; }

        [Required]
        [DisplayName("Cheque Bank")]
        public string ChequeBank { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [DisplayName("Bank Name")]
        public int BankId { get; set; }

        [Required]
        public byte[] ReceiptImage { get; set; }

    }
}
