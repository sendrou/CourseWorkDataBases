namespace Cargo.ViewModels
{
    public class FilterUserViewModel
    {
        public FilterUserViewModel(string name)
        {
            SelectedName = name;
        }

        public string SelectedName { get; }
    }
}
