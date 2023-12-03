using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cargo.ViewModels
{
    public class FilterCarsViewModel
    {
        public FilterCarsViewModel(List<CarBrand> carBrands,int carBrand, int liftingCapacity, int bodyVolume, string registrationNumber)
        {
            CarBrands = new SelectList(carBrands, "CarId", "RegistrationNumber", carBrand);
            SelectedBodyVolume = bodyVolume;
            SelectedCarBrandId = carBrand;
            SelectedLiftingCapacity = liftingCapacity;
            SelectedRegistrationNumber = registrationNumber;



        }
        public int SelectedLiftingCapacity { get; set; }
        public int SelectedBodyVolume { get; set; }
        public string SelectedRegistrationNumber { get; set; }

        public SelectList CarBrands { get; }
        public int SelectedCarBrandId { get; set; }

    }
}
