using Cargo.Models;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class CreateOrganizationsViewModel
    {
        [Required]
        public string OrganizationName { get; set; }



    }
}

