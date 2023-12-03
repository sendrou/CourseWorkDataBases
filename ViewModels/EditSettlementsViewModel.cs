using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class EditSettlementsViewModel
    {
        public EditSettlementsViewModel() { }
        public EditSettlementsViewModel(string settlementName)
        {
            SettlementName = settlementName;


        }
        public int Id { get; set; }

        [Required]
        public string SettlementName { get; set; }


    }
}
