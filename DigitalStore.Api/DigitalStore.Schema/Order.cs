using DigitalStore.Base.Entity;
using DigitalStore.Base.Schema;
using DigitalStore.Data.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Schema
{
    public class OrderRequest : BaseRequest
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

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
    }


    public class OrderResponse : BaseResponse
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
