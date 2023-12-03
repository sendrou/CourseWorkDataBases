using Cargo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Cargo.ViewModels
{
    public class OrganizationsViewModel
    {
        public IEnumerable<Organization> Organizations{ get; }
        public PageViewModel PageViewModel { get; }
        public FilterOrganizationsViewModel FilterOrganizationsViewModel { get; }

        //public ApplicationUser ApplicationUser { get; }
        public OrganizationsViewModel(IEnumerable<Organization> organizations, PageViewModel viewModel, FilterOrganizationsViewModel filterOrganizationsViewModel)
        {
            Organizations = organizations;
            PageViewModel = viewModel;
            FilterOrganizationsViewModel = filterOrganizationsViewModel;
        }

    }
}
