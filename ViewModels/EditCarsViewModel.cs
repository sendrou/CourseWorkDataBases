using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class EditCarsViewModel
    {
        public EditCarsViewModel() { }
        public EditCarsViewModel(List<CarBrand> carBrands, int carBrand, int liftingCapacity, int bodyVolume, string registrationNumber)
        {
            BodyVolume = bodyVolume;
            LiftingCapacity = liftingCapacity;
            RegistrationNumber = registrationNumber;
            CarBrands = new SelectList(carBrands, "CarBrandId", "BrandName", carBrand);
            SelectedCarBrandId = carBrand;
        }

        public int Id { get; set; }


        [Required]
        public int SelectedCarBrandId { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }
        [Required]
        public int LiftingCapacity { get; set; }
        [Required]
        public int BodyVolume { get; set; }


        public IEnumerable<SelectListItem>? CarBrands { get; set; }



    }
}
