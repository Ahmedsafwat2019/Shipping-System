using BL.Contract;
using BL.Contract.Shipment;
using BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Shipment.ManageStates
{
    public class ShippShipment : IShipmentStatesHandler
    {
        IShipment _shipment;
        IShipmentStatus _status;
        public ShippShipment(IShipment shipment, IShipmentStatus status)
        {
            _shipment = shipment;
            _status = status;
        }
        public ShipmentStatusEnum TargetState { get => ShipmentStatusEnum.Shipped; }
        public async Task HandleState(ShipmentDto shipmentDto)
        {
            await _shipment.UpdateFields(shipmentDto.Id, a =>
            {
                a.DelivryDate = shipmentDto.DeliveryDate;
                a.CurrentState = (int)TargetState;
            });

        }
    }
}
