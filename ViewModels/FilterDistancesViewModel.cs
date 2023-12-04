using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cargo.ViewModels
{
    public class FilterDistancesViewModel
    {
        public FilterDistancesViewModel(List<Settlement>settlements, int settlement,int startDistance, int endDistance, string departureSettlementName, string arrivalSettlementName)
        {
            DepartureSettlementName = departureSettlementName;
            SelectedStartDistance = startDistance;
            SelectedEndDistance = endDistance;
            ArrivalSettlementName = arrivalSettlementName;
            //ArrivalSettlements = new SelectList(settlements, "CarBrandId", "BrandName", settlement);
            //SelectedArrivalSettlementId = settlement;
            //DeparturesSettlements = new SelectList(settlements, "CarBrandId", "BrandName", settlement);
            //SelectedDeparturesSettlementId = settlement;



        }
     
        //public SelectList ArrivalSettlements { get; }
        //public int SelectedArrivalSettlementId { get; set; }
        //public SelectList DeparturesSettlements { get; }
        //public int SelectedDeparturesSettlementId { get; set; }


        public int SelectedStartDistance { get; set; }
        public int SelectedEndDistance { get; set; }
        public string ArrivalSettlementName { get; set; }
        public string DepartureSettlementName { get; set; }

    }
}
