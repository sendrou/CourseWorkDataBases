using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cargo.ViewModels
{
    public class CreateDistancesViewModel
    {
        public CreateDistancesViewModel() { }
        public CreateDistancesViewModel(List<Settlement> settlements)
        {
            ArrivalSettlements = new SelectList(settlements, "SettlementId", "SettlementName", settlements[0]);
            DeparturesSettlements = new SelectList(settlements, "SettlementId", "SettlementName", settlements[0]);

        }


        public IEnumerable<SelectListItem>? ArrivalSettlements { get; set; }
        public IEnumerable<SelectListItem>? DeparturesSettlements { get; set; }

        [Required]
        public int Distance { get; set; }


        [Required]
        public int ArrivalSettlementId { get; set; }
        [Required]
        public int DeparturesSettlementId { get; set; }

    }
}

