using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class EditCargoTransportationsViewModel
    {
        public EditCargoTransportationsViewModel() { }
        public EditCargoTransportationsViewModel(List<Car> cars, List<Distance> distances, List<Driver> drivers, List<Load> loads, List<Organization> organizations, List<TransportationTariff> transportationTariffs, int car, int distance, int driver, int load, int organization, int tariff, DateTime date)
        {
            Cars = new SelectList(cars, "CarId", "RegistrationNumber", car);
            SelectedCarId = car;
            SelectedDistanceId = distance;
            SelectedDriverId = driver;
            SelectedLoadId = load;
            SelectedOrganizationId = organization;
            SelectedTransportationTariffId = tariff;
            SelectedDate=date;
            Distances = new SelectList(distances, "DistanceId", "Distance1", distance);

            Drivers = new SelectList(drivers, "DriverId", "FullName", driver);

            Loads = new SelectList(loads, "LoadId", "LoadName", load);
            Organizations = new SelectList(organizations, "OrganizationId", "OrganizationName", organization);

            TransportationTariffs = new SelectList(transportationTariffs, "TransportationTariffId", "TariffPerTKm", tariff);

        }
        public int Id { get; set; }
        [Required]
        public DateTime SelectedDate { get; set; }
        [Required]
        public int SelectedOrganizationId { get; set; }

        [Required]
        public int SelectedCarId { get; set; }

        [Required]
        public int SelectedDistanceId { get; set; }
        [Required]
        public int SelectedDriverId { get; set; }
        [Required]
        public int SelectedLoadId { get; set; }
        [Required]
        public int SelectedTransportationTariffId { get; set; }

        public IEnumerable<SelectListItem>? Cars { get; set; }

        public IEnumerable<SelectListItem>? Distances { get; set; }
        public IEnumerable<SelectListItem>? Drivers { get; set; }

        public IEnumerable<SelectListItem>? Loads { get; set; }

        public IEnumerable<SelectListItem>? Organizations { get; set; }
        public IEnumerable<SelectListItem>? TransportationTariffs { get; set; }
    }
}
