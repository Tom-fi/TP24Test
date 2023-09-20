using System;
using System.ComponentModel.DataAnnotations;

namespace TP24.Api.Invoices.DTO
{
    public class AddInvoiceRequest : IValidatableObject
    {
        [Required]
        [StringLength(50)]
        public string Reference { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string CurrencyCode { get; set; }

        [Required]
        [StringLength(10)]
        public string IssueDate { get; set; }

        [Required]
        [Range(0, 999999999999999.999)]
        public decimal OpeningValue { get; set; }

        [Required]
        [Range(0, 999999999999999.999)]
        public decimal PaidValue { get; set; }

        [Required]
        [StringLength(10)]
        public string DueDate { get; set; }

        [StringLength(10)]
        public string ClosedDate { get; set; }

        public bool Cancelled { get; set; }

        [Required]
        [StringLength(100)]
        public string DebtorName { get; set; }

        [Required]
        [StringLength(50)]
        public string DebtorReference { get; set; }

        [StringLength(100)]
        public string DebtorAddress1 { get; set; }

        [StringLength(100)]
        public string DebtorAddress2 { get; set; }

        [StringLength(100)]
        public string DebtorTown { get; set; }

        [StringLength(100)]
        public string DebtorState { get; set; }

        [StringLength(20)]
        public string DebtorZip { get; set; }

        [Required]
        [StringLength(2)]
        public string DebtorCountryCode { get; set; }

        [StringLength(50)]
        public string DebtorRegistrationNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (OpeningValue < PaidValue)
            {
                yield return new ValidationResult("PaidValue must be less than or equal to OpeningValue", new[] { "PaidValue" });
            }
        }
    }
}
