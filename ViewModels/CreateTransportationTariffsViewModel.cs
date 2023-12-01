using Cargo.Models;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class CreateTransportationTariffsViewModel
    {
        [Required]
        public int TariffPerTKm { get; set; }
        [Required]
        public int TariffPerM3Km { get; set; }


    }
}
