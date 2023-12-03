using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cargo.ViewModels
{
    public class FilterDriversViewModel
    {
        public FilterDriversViewModel(string driverName, string passportDetails)
        {


            SelectedDriverName = driverName;
            SelectedPassportDetails = passportDetails;


        }

        public string SelectedDriverName { get; set; }
        public string SelectedPassportDetails { get; set; }

    }
}
