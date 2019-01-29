using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TeamHGSTalentContest.Models;

namespace TeamHGSTalentContest.ViewModels
{
    public class SubmissionViewModel : BaseEntity
    {
        [Required]
        [StringLength(100)]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Name => $"{FirstName} {LastName}";

        [Required]
        [StringLength(200)]
        [Display(Name = "Your Manager's Full Name")]
        public string ManagerName { get; set; }

        [Required]
        [Display(Name = "Choose your location")]
        [Range(typeof(int), "1", "999", ErrorMessage = "Please choose a location.")]
        public int LocationId { get; set; }

        [Display(Name="Location")]
        public string LocationName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(320, MinimumLength = 5)]
        [Display(Name = "Email Address (work email preferred)")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Describe your talent")]
        [StringLength(10000)]
        public string Talent { get; set; }

        [Required]
        [Display(Name = "Choose a video file")]
        public IFormFile FormFile { get; set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Display(Name = "Error Message")]
        public string ErrorMessage { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must consent to the image release...")]
        [Display(Name="Image Release Consent")]
        public bool ImageConsent { get; set; }

        public bool Archive { get; set; }
    }
}
