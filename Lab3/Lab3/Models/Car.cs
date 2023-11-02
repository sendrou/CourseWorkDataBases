using Lab3.Models;
using System;
using System.Collections.Generic;

namespace Lab02;

public partial class Car
{
    public int CarId { get; set; }

    public int? CarBrandId { get; set; }

    public int LiftingCapacity { get; set; }

    public int BodyVolume { get; set; }

    public string RegistrationNumber { get; set; } = null!;

    public virtual CarBrand? CarBrand { get; set; }

    public virtual ICollection<CargoTransportation> CargoTransportations { get; set; } = new List<CargoTransportation>();
}
