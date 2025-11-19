using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractClaimSystem.Models
{
    public class Claim
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lecturer name is required")]
        [Display(Name = "Lecturer Name")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string LecturerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email Address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string LecturerEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Claim month is required")]
        [Display(Name = "Claim Month")]
        [DataType(DataType.Date)]
        public DateTime ClaimMonth { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        [Display(Name = "Claim Amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Document Path")]
        public string? DocumentPath { get; set; }

        [Display(Name = "Status")]
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;

        [Display(Name = "Submitted Date")]
        public DateTime SubmittedDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Reviewed Date")]
        public DateTime? ReviewedDate { get; set; }

        [Display(Name = "Approved Date")]
        public DateTime? ApprovedDate { get; set; }

        [Display(Name = "Reviewer Notes")]
        [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
        public string? ReviewerNotes { get; set; }
    }

    public enum ClaimStatus
    {
        [Display(Name = "Pending")]
        Pending,

        [Display(Name = "Under Review")]
        UnderReview,

        [Display(Name = "Approved")]
        Approved,

        [Display(Name = "Rejected")]
        Rejected
    }
}