﻿using Cargo.Models;
using System.ComponentModel.DataAnnotations;

namespace Cargo.ViewModels
{
    public class CreateCarBrandsViewModel
    {
        [Required]
        public string CarBrand { get; set; }
        
    }
}
