using System;
using System.ComponentModel.DataAnnotations;
using JobCandidates.API.ValidationAttributes;

namespace JobCandidates.API.Dto
{
    public class DeleteEntityIdDto
    {
        [Required(ErrorMessage = "Id is required")]
        [Guid(ErrorMessage = "Invalid id")]
        public Guid? Id { get; set; }
    }
}
