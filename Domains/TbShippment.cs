using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains;

public partial class TbShipment : BaseTable
{
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime ShipingDate { get; set; }
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime DelivryDate { get; set; }
    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public Guid? ShippingTypeId { get; set; }
    public Guid? ShipingPackgingId { get; set; }
    [Required]
    public double Width { get; set; }
    [Required]
    public double Height { get; set; }
    [Required]
    public double Weight { get; set; }
    [Required]
    public double Length { get; set; }
    [Required]
    public decimal PackageValue { get; set; }
    [Required]
    public decimal ShippingRate { get; set; }

    public Guid? PaymentMethodId { get; set; }

    public Guid? UserSubscriptionId { get; set; }

    public double? TrackingNumber { get; set; }

    public Guid? ReferenceId { get; set; }

    public Guid? CarrierId { get; set; }

    public virtual TbCarrier Carrier { get; set; } = null!;
    public virtual TbPaymentMethod? PaymentMethod { get; set; }

    public virtual TbUserReceiver Receiver { get; set; } = null!;

    public virtual TbUserSender Sender { get; set; } = null!;

    public virtual TbShippingType ShippingType { get; set; } = null!;
    public virtual TbShipingPackging ShipingPackging { get; set; } = null!;
    public virtual ICollection<TbShipmentStatus> TbShipmentStatus { get; set; } = new List<TbShipmentStatus>();
}
