using System;
using System.Collections.Generic;

namespace Lab02;

public partial class Driver
{
    public int DriverId { get; set; }

    public string FullName { get; set; } = null!;

    public string PassportDetails { get; set; } = null!;

    public virtual ICollection<CargoTransportation> CargoTransportations { get; set; } = new List<CargoTransportation>();
}
