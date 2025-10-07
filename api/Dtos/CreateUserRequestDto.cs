using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class CreateUserRequestDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(20, ErrorMessage = "First name cannot exceed 20 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(20, ErrorMessage = "Last name cannot exceed 20 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; } = string.Empty;

        public string Picture { get; set; } = string.Empty;

        public List<int> FavoriteCityIds { get; set; } = new();

    }
}