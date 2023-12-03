using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class EditTransportationTariffsViewModel
    {
        public EditTransportationTariffsViewModel() { }
        public EditTransportationTariffsViewModel(int tariffPerTKm, int tariffPerM3Km)
        {
            TariffPerTKm = tariffPerTKm;
            TariffPerM3Km = tariffPerM3Km;

        }
        public int Id { get; set; }

        [Required]
        public int TariffPerTKm { get; set; }
        [Required]
        public int TariffPerM3Km { get; set; }

    }
}
