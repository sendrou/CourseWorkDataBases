using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class EditOrganizationsViewModel
    {
        public EditOrganizationsViewModel() { }
        public EditOrganizationsViewModel(string organizationName)
        {
            OrganizationName = organizationName;


        }
        public int Id { get; set; }

        [Required]
        public string OrganizationName { get; set; }


    }
}
