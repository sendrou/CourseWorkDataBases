using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cargo.ViewModels
{
    public class FilterSettlementsViewModel
    {
        public FilterSettlementsViewModel(string settlementName)
        {


            SelectedSettlementName = settlementName;



        }

        public string SelectedSettlementName { get; set; }


    }
}
