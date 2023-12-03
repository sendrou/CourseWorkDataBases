using Cargo.Models;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class CreateSettlementsViewModel
    {
        [Required]
        public string SettlementName { get; set; }



    }
}

