using Cargo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cargo.ViewModels
{
    public class FilterLoadsViewModel
    {
        public FilterLoadsViewModel(string loadName, int startVolume, int endVolume, int startWeight, int endWeight)
        {


            SelectedLoadName = loadName;
            SelectedStartVolume = startVolume;
            SelectedEndVolume = endVolume;
            SelectedStartWeight = startWeight;
            SelectedEndWeight = endWeight;


        }

        public string SelectedLoadName { get; set; }

        public int SelectedStartVolume { get; set; }
        public int SelectedEndVolume { get; set; }
        public int SelectedStartWeight { get; set; }
        public int SelectedEndWeight { get; set; }


    }
}
