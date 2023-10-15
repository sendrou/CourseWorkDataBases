using System;
using System.Collections.Generic;

namespace Lab02;

public partial class Distance
{
    public int DistanceId { get; set; }

    public int? DeparturesSettlementId { get; set; }

    public int? ArrivalSettlementId { get; set; }

    public int? Distance1 { get; set; }

    public virtual Settlement? ArrivalSettlement { get; set; }

    public virtual ICollection<CargoTransportation> CargoTransportations { get; set; } = new List<CargoTransportation>();

    public virtual Settlement? DeparturesSettlement { get; set; }
}
