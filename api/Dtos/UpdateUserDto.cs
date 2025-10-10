using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class UpdateUserDto
    {
        [StringLength(20, ErrorMessage = "First name cannot exceed 20 characters.")]
        public string? FirstName { get; set; }

        [StringLength(20, ErrorMessage = "Last name cannot exceed 20 characters.")]
        public string? LastName { get; set; }

        public string? Picture { get; set; }
    }
}