using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbCarrier : BaseTable
{
    public string CarrierName { get; set; } = null!;

    public virtual ICollection<TbShipment> TbShipments { get; set; } = new List<TbShipment>();
}
