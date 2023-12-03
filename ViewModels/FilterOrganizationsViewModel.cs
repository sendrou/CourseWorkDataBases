using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cargo.ViewModels
{
    public class FilterOrganizationsViewModel
    {
        public FilterOrganizationsViewModel(string organizationName)
        {


            SelectedOrganizationName = organizationName;



        }

        public string SelectedOrganizationName { get; set; }


    }
}
