using Cargo.Models;
using Microsoft.AspNetCore.Identity;

namespace Cargo.ViewModels
{
    public class UsersViewModel
    {
        public IEnumerable<IdentityUser> Users { get; }

        public PageViewModel PageViewModel { get; }

        public FilterUserViewModel FilterUserViewModel { get; }

        public UsersViewModel(IEnumerable<IdentityUser> users, PageViewModel pageViewModel, FilterUserViewModel filterUserViewModel)
        {
            Users = users;
            PageViewModel = pageViewModel;
            FilterUserViewModel = filterUserViewModel;
        }
    }
}
