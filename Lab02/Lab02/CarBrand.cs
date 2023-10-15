using System;
using System.Collections.Generic;

namespace Lab02;

public partial class CarBrand
{
    public int CarBrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
