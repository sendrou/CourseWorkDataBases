using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cargo.Models;

public partial class Distance
{
    public int DistanceId { get; set; }

    [ForeignKey("DeparturesSettlement")]
    public int? DeparturesSettlementId { get; set; }

    [ForeignKey("ArrivalSettlement")]
    public int? ArrivalSettlementId { get; set; }

    public int Distance1 { get; set; }
    
    public virtual Settlement? ArrivalSettlement { get; set; }

    public virtual ICollection<CargoTransportation> CargoTransportations { get; set; } = new List<CargoTransportation>();
    public virtual Settlement? DeparturesSettlement { get; set; }
}
