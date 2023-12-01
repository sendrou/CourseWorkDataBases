using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class CreateCargoTransportationsViewModel
    {


        public CreateCargoTransportationsViewModel() { }
        public CreateCargoTransportationsViewModel(List<Car> cars, List<Distance> distances, List<Driver> drivers, List<Load> loads,List<Organization> organizations, List<TransportationTariff> transportationTariffs)
        {
            Cars = new SelectList(cars, "CarId", "RegistrationNumber", cars[0]);

            Distances = new SelectList(distances, "DistanceId", "Distance1", distances[0]);

            Drivers = new SelectList(drivers, "DriverId", "FullName", drivers[0]);

            Loads = new SelectList(loads, "LoadId", "LoadName", loads[0]);
            Organizations = new SelectList(organizations, "OrganizationId", "OrganizationName", organizations[0]);

            TransportationTariffs = new SelectList(transportationTariffs, "TransportationTariffId", "TariffPerTKm", transportationTariffs[0]);
            Date = DateTime.Now;

        }



        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int OrganizationId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int DistanceId { get; set; }
        [Required]
        public int DriverId { get; set; }
        [Required]
        public int LoadId { get; set; }
        [Required]
        public int TransportationTariffId { get; set; }

        public IEnumerable<SelectListItem>? Cars { get; set; }

        public IEnumerable<SelectListItem>? Distances { get; set; }
        public IEnumerable<SelectListItem>? Drivers { get; set; }

        public IEnumerable<SelectListItem>? Loads { get; set; }

        public IEnumerable<SelectListItem>? Organizations { get; set; }
        public IEnumerable<SelectListItem>? TransportationTariffs { get; set; }


    }
}
