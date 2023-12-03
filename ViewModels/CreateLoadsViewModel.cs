using Cargo.Models;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class CreateLoadsViewModel
    {
        [Required]
        public string LoadName { get; set; }
        [Required]
        public int Volume { get; set; }
        [Required]
        public int Weight { get; set; }

    }
}

