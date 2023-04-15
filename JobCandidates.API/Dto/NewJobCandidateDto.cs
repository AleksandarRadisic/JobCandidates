using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobCandidates.API.Dto
{
    public class NewJobCandidateDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [RegularExpression(@"((\+[0-9]{1,3})|0)[0-9]{7,10}", ErrorMessage = "Invalid phone number")]
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime? DateOfBirth { get; set; }
        public IEnumerable<Guid> Skills { get; set; }
    }
}
