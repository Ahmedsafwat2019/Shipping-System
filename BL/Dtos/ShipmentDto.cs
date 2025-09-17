using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.Dtos;

public partial class ShipmentDto : BaseDto
{
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime ShipingDate { get; set; }
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime DeliveryDate { get; set; }

    public Guid SenderId { get; set; }
    public UserSenderDto UserSender { get; set; }
    public Guid ReceiverId { get; set; }
    public UserReceiverDto UserReceiver { get; set; }
    public Guid? ShippingTypeId { get; set; }
    public Guid? ShipingPackgingId { get; set; }
    public Guid? CarrierId { get; set; }
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
}
