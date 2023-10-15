using System;
using System.Collections.Generic;

namespace Lab02;

public partial class CargoTransportation
{
    public int DocumentId { get; set; }

    public DateTime? Date { get; set; }

    public int? OrganizationId { get; set; }

    public int? DistanceId { get; set; }

    public int? DriverId { get; set; }

    public int? CarId { get; set; }

    public int? LoadId { get; set; }

    public int? TransportationTariffId { get; set; }

    public virtual Car? Car { get; set; }

    public virtual Distance? Distance { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual Load? Load { get; set; }

    public virtual Organization? Organization { get; set; }

    public virtual TransportationTariff? TransportationTariff { get; set; }
}
