using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cargo.Models;

public partial class Settlement
{
    public int SettlementId { get; set; }

    public string SettlementName { get; set; } = null!;

    [InverseProperty("ArrivalSettlement")]
    public virtual ICollection<Distance> DistanceArrivalSettlements { get; set; } = new List<Distance>();

    [InverseProperty("DeparturesSettlement")]
    public virtual ICollection<Distance> DistanceDeparturesSettlements { get; set; } = new List<Distance>();
}
