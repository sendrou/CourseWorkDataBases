using Cargo.Models;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class CreateCarBrandsViewModel
    {
        [Required]
        public int CarBrand { get; set; }
        
    }
}
