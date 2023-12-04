using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cargo.ViewModels
{
    public class FilterCarsViewModel
    {
        public FilterCarsViewModel(List<CarBrand> carBrands,int carBrand,  int startBodyVolume, int endBodyVolume, int startLiftingCapacity, int endLiftingCapacity, string registrationNumber, DateTime startDate, DateTime endDate)
        {
            CarBrands = new SelectList(carBrands, "CarBrandId", "BrandName", carBrand);
            SelectedStartBodyVolume = startBodyVolume;
            SelectedEndBodyVolume = endBodyVolume;
            SelectedCarBrandId = carBrand;
            SelectedStartLiftingCapacity = startLiftingCapacity;
            SelectedEndLiftingCapacity = endLiftingCapacity;
            SelectedRegistrationNumber = registrationNumber;
            SelectedStartDate = startDate;
            SelectedEndDate = endDate;


        }
        public int SelectedStartLiftingCapacity { get; set; }
        public int SelectedEndLiftingCapacity { get; set; }
        public int SelectedStartBodyVolume { get; set; }
        public int SelectedEndBodyVolume { get; set; }
        public string SelectedRegistrationNumber { get; set; }

        public SelectList CarBrands { get; }
        public int SelectedCarBrandId { get; set; }
        public DateTime SelectedStartDate { get; set; }
        public DateTime SelectedEndDate { get; set; }
    }
}
