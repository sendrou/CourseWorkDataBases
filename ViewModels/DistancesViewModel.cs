using Cargo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Cargo.ViewModels
{
    public class DistancesViewModel
    {
        public IEnumerable<Distance> Distances{ get; }
        public PageViewModel PageViewModel { get; }
        public FilterDistancesViewModel FilterDistancesViewModel { get; }

        //public ApplicationUser ApplicationUser { get; }
        public DistancesViewModel(IEnumerable<Distance> distances, PageViewModel viewModel, FilterDistancesViewModel filterDistancesViewModel)
        {
            Distances = distances;
            PageViewModel = viewModel;
            FilterDistancesViewModel = filterDistancesViewModel;
        }

    }
}
