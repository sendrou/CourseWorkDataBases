using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cargo.ViewModels
{
    public class CreateCarsViewModel
    {
        public CreateCarsViewModel() { }
        public CreateCarsViewModel(List<CarBrand> carBrands)
        {
            CarBrands = new SelectList(carBrands, "CarBrandId", "BrandName", carBrands[0]);


        }


        public IEnumerable<SelectListItem>? CarBrands { get; set; }
        [Required]
        public string RegistrationNumber { get; set; }
        [Required]
        public int LiftingCapacity { get; set; }
        [Required]
        public int BodyVolume { get; set; }

        [Required]
        public int CarBrandId { get; set; }

    }
}

