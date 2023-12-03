using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cargo.ViewModels
{
    public class FilterTransportationTariffsViewModel
    {
        public FilterTransportationTariffsViewModel(int startTariff, int endTariff)
        {


            SelectedStartTransportationTariff = startTariff;
            SelectedEndTransportationTariff = endTariff;


        }

        public int SelectedStartTransportationTariff { get; set; }
        public int SelectedEndTransportationTariff { get; set; }

    }
}
