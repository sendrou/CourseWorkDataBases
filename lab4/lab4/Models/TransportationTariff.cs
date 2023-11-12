using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class TransportationTariff
{
    public int TransportationTariffId { get; set; }

    public int TariffPerTKm { get; set; }

    public int TariffPerM3Km { get; set; }

    public virtual ICollection<CargoTransportation> CargoTransportations { get; set; } = new List<CargoTransportation>();
}
