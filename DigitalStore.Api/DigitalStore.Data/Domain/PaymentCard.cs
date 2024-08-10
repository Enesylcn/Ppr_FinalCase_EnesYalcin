using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Data.Domain
{
    public class PaymentCard
    {
        [Required(ErrorMessage = "Card name is required.")]
        [StringLength(50, ErrorMessage = "Card name cannot exceed 50 characters.")]
        public string CardOwnerName { get; set; }

        [Required(ErrorMessage = "Card number is required.")]
        [CreditCard(ErrorMessage = "Invalid card number.")]
        [StringLength(16, MinimumLength = 13, ErrorMessage = "Card number must be between 13 and 16 digits.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Expiration month is required.")]
        [Range(1, 12, ErrorMessage = "Expiration month must be between 01 and 12.")]
        public string ExpirationMonth { get; set; }

        [Required(ErrorMessage = "Expiration year is required.")]
        [Range(2024, 2100, ErrorMessage = "Expiration year must be between the current year and 2100.")]
        public string ExpirationYear { get; set; }

        [Required(ErrorMessage = "CVC is required.")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "CVC must be 3 or 4 digits.")]
        public string Cvc { get; set; }
    }
}
