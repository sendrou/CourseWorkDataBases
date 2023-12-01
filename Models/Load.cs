using System;
using System.Collections.Generic;

namespace Cargo.Models;

public partial class Load
{
    public int LoadId { get; set; }

    public string LoadName { get; set; } = null!;

    public int Volume { get; set; }

    public int Weight { get; set; }

    public virtual ICollection<CargoTransportation> CargoTransportations { get; set; } = new List<CargoTransportation>();
}
