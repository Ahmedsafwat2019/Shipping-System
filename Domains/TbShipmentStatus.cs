using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbShipmentStatus : BaseTable
{
    public Guid? ShipmentId { get; set; }

    public string? Notes { get; set; }



    public virtual TbShipment? Shipment { get; set; }
}
