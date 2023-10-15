using System;
using System.Collections.Generic;

namespace Lab02;

public partial class Organization
{
    public int OrganizationId { get; set; }

    public string OrganizationName { get; set; } = null!;

    public virtual ICollection<CargoTransportation> CargoTransportations { get; set; } = new List<CargoTransportation>();
}
