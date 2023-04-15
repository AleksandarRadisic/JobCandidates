using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidates.API.Dto
{
    public class NewSkillDto
    {
        [Required(ErrorMessage = "Skill name is required")]
        public string Name { get; set; }
    }
}
