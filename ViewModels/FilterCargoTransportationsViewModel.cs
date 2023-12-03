using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cargo.ViewModels
{
    public class FilterCargoTransportationsViewModel
    {
        public FilterCargoTransportationsViewModel(List<Car> cars,  List<Driver> drivers, List<Load> loads, List<Organization> organizations,  int car, int startDistance, int endDistance, int driver, int load, int organization, int startTariff, int endTariff, DateTime startDate,DateTime endDate)
        {


            Cars = new SelectList(cars, "CarId", "RegistrationNumber", car);
            SelectedCarId = car;
            SelectedStartDistance = startDistance;
            SelectedEndDistance = endDistance;

            SelectedDriverId = driver;
            SelectedLoadId = load;
            SelectedOrganizationId = organization;
            SelectedStartTransportationTariff = startTariff;
            SelectedEndTransportationTariff = endTariff;

            SelectedStartDate = startDate;
            SelectedEndDate = endDate;
            Drivers = new SelectList(drivers, "DriverId", "FullName", driver);

            Loads = new SelectList(loads, "LoadId", "LoadName", load);
            Organizations = new SelectList(organizations, "OrganizationId", "OrganizationName", organization);

        }
        public SelectList Cars { get; }

        public SelectList Drivers { get; }
        public SelectList Loads { get; }
        public SelectList Organizations { get; }




        public DateTime SelectedStartDate { get; set; }
        public DateTime SelectedEndDate { get; set; }


        public int SelectedOrganizationId { get; set; }


        public int SelectedCarId { get; set; }


        public int SelectedStartDistance { get; set; }
        public int SelectedEndDistance { get; set; }

        public int SelectedDriverId { get; set; }

        public int SelectedLoadId { get; set; }

        public int SelectedStartTransportationTariff { get; set; }
        public int SelectedEndTransportationTariff { get; set; }

    }
}
