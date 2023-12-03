using Cargo.Models;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class CreateDriversViewModel
    {
        [Required]
        public string DriverName { get; set; }
        [Required]
        public string PassportDetails { get; set; }



    }
}

