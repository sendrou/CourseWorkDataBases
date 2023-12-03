using Cargo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Cargo.ViewModels
{
    public class LoadsViewModel
    {
        public IEnumerable<Load> Loads{ get; }
        public PageViewModel PageViewModel { get; }
        public FilterLoadsViewModel FilterLoadsViewModel { get; }

        //public ApplicationUser ApplicationUser { get; }
        public LoadsViewModel(IEnumerable<Load> loads, PageViewModel viewModel, FilterLoadsViewModel filterLoadsViewModel)
        {
            Loads = loads;
            PageViewModel = viewModel;
            FilterLoadsViewModel = filterLoadsViewModel;
        }

    }
}
