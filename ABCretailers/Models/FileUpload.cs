using System.ComponentModel.DataAnnotations;
namespace ABCretailers.Models
{
    public class FileUpload
    {
        [Required]
        [Display(Name = "Proof of Payment")]
        public IFormFile ProofOfPayment { get; set; }

        [Display(Name = "Order ID")]
        public string? OrderId { get; set; }
        [Display(Name = "Customer Name")]
        public string? CustomerName { get; set; }

    }
}
