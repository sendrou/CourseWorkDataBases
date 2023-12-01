namespace Cargo.Models;

public partial class CarBrand
{
    public int CarBrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
