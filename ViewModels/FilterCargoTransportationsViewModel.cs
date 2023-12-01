using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cargo.ViewModels
{
    public class FilterCargoTransportationsViewModel
    {
        public FilterCargoTransportationsViewModel(List<Car> cars, List<Distance> distances, List<Driver> drivers, List<Load> loads, List<Organization> organizations, List<TransportationTariff> transportationTariffs, int car, int distance, int driver, int load, int organization, int tariff, DateTime date)
        {


            Cars = new SelectList(cars, "CarId", "RegistrationNumber", car);
            SelectedCarId = car;
            SelectedDistanceId = distance;
            SelectedDriverId = driver;
            SelectedLoadId = load;
            SelectedOrganizationId = organization;
            SelectedTransportationTariffId = tariff;
            SelectedDate = date;
            Distances = new SelectList(distances, "DistanceId", "Distance1", distance);
            Drivers = new SelectList(drivers, "DriverId", "FullName", driver);

            Loads = new SelectList(loads, "LoadId", "LoadName", load);
            Organizations = new SelectList(organizations, "OrganizationId", "OrganizationName", organization);

            TransportationTariffs = new SelectList(transportationTariffs, "TransportationTariffId", "TariffPerTKm", tariff);
        }
        public SelectList Cars { get; }
        public SelectList Distances { get; }
        public SelectList Drivers { get; }
        public SelectList Loads { get; }
        public SelectList Organizations { get; }
        public SelectList TransportationTariffs { get; }



        public DateTime SelectedDate { get; set; }

        public int SelectedOrganizationId { get; set; }


        public int SelectedCarId { get; set; }


        public int SelectedDistanceId { get; set; }

        public int SelectedDriverId { get; set; }

        public int SelectedLoadId { get; set; }

        public int SelectedTransportationTariffId { get; set; }
    }
}
