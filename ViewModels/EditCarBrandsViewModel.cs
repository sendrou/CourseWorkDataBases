using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class EditCarBrandsViewModel
    {
        public EditCarBrandsViewModel() { }
        public EditCarBrandsViewModel(string carBrand)
        {
            CarBrand = carBrand;


        }
        public int Id { get; set; }

        [Required]
        public string CarBrand { get; set; }


    }
}
