using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cargo.ViewModels
{
    public class FilterDriversViewModel
    {
        public FilterDriversViewModel(string driverName, string passportDetails, DateTime startDate, DateTime endDate)
        {


            SelectedDriverName = driverName;
            SelectedPassportDetails = passportDetails;
            SelectedStartDate = startDate;
            SelectedEndDate = endDate;
        }

        public string SelectedDriverName { get; set; }
        public string SelectedPassportDetails { get; set; }

        public DateTime SelectedStartDate { get; set; }
        public DateTime SelectedEndDate { get; set; }

    }
}
