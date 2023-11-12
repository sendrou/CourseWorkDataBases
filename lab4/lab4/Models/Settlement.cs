using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class Settlement
{
    public int SettlementId { get; set; }

    public string SettlementName { get; set; } = null!;

    public virtual ICollection<Distance> DistanceArrivalSettlements { get; set; } = new List<Distance>();

    public virtual ICollection<Distance> DistanceDeparturesSettlements { get; set; } = new List<Distance>();
}
