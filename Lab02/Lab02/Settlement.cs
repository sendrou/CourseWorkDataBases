using System;
using System.Collections.Generic;

namespace Lab02;

public partial class Settlement
{
    public int SettlementId { get; set; }

    public string SettlementName { get; set; } = null!;

    public virtual ICollection<Distance> DistanceArrivalSettlements { get; set; } = new List<Distance>();

    public virtual ICollection<Distance> DistanceDeparturesSettlements { get; set; } = new List<Distance>();
}
