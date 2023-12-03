using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class EditDriversViewModel
    {
        public EditDriversViewModel() { }
        public EditDriversViewModel(string driverName, string passportDetails)
        {
            DriverName =driverName;
            PassportDetails = passportDetails;

        }
        public int Id { get; set; }

        [Required]
        public string DriverName { get; set; }

        [Required]
        public string PassportDetails { get; set; }
    }
}
