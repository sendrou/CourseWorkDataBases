using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class EditLoadsViewModel
    {
        public EditLoadsViewModel() { }
        public EditLoadsViewModel(string loadName, int volume, int weight)
        {
           LoadName = loadName;
            Volume = volume;
            Weight = weight;

        }
        public int Id { get; set; }

        [Required]
        public string LoadName { get; set; }
        [Required]
        public int Volume { get; set; }

        [Required]
        public int Weight { get; set; }
    }
}
