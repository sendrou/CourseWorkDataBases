using Cargo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Cargo.ViewModels
{
    public class SettlementsViewModel
    {
        public IEnumerable<Settlement> Settlements{ get; }
        public PageViewModel PageViewModel { get; }
        public FilterSettlementsViewModel FilterSettlementsViewModel { get; }

        //public ApplicationUser ApplicationUser { get; }
        public SettlementsViewModel(IEnumerable<Settlement> settlements, PageViewModel viewModel, FilterSettlementsViewModel filterSettlementsViewModel)
        {
            Settlements = settlements;
            PageViewModel = viewModel;
            FilterSettlementsViewModel = filterSettlementsViewModel;
        }

    }
}
