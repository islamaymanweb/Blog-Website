using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} needs to be at least {2} and no more than {1} characters long", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string? Name { get; set; }
       
        [NotMapped]
        public string Role { get; set; }
    }
}
