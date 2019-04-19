using System.ComponentModel.DataAnnotations;

namespace SqlInjectionDemo.Web.Public.Models
{
    public class UserInputModel
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsSafe { get; set; }
    }
}
