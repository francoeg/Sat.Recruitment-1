
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Application.EntitesVM
{
    public class UserRequestVM
    {
        [Required(ErrorMessage = "The Name is required")]
        public string Name { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "The Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The Adress is required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "The phone is required.")]
        public string Phone { get; set; }
        public string UserType { get; set; }
        public string Money { get; set; }
    }
}
