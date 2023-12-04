using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class EditDistancesViewModel
    {
        public EditDistancesViewModel() { }
        public EditDistancesViewModel(List<Settlement> settlements, int arrivalSettlement, int departuresSettlement, int distance)
        {
            ArrivalSettlements = new SelectList(settlements, "SettlementId", "SettlementName", arrivalSettlement);
            DeparturesSettlements = new SelectList(settlements, "SettlementId", "SettlementName", departuresSettlement);
            SelectedArrivalSettlementId = arrivalSettlement;
            SelectedDeparturesSettlementId = departuresSettlement;
            SelectedDistance = distance;
            
        }

        public int Id { get; set; }

  


        public IEnumerable<SelectListItem>? ArrivalSettlements { get; set; }
        public IEnumerable<SelectListItem>? DeparturesSettlements { get; set; }

        [Required]
        public int SelectedDistance { get; set; }


        [Required]
        public int SelectedArrivalSettlementId { get; set; }
        [Required]
        public int SelectedDeparturesSettlementId { get; set; }



    }
}
