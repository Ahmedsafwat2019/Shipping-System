using BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contract.Shipment
{
    public interface IShipmentWorkflowServices
    {
        public Task ApproveShipment(ShipmentDto dto);
        public Task<bool> ReadyShipment(ShipmentDto dto);
        public Task<bool> ShippingShipment(Guid id, DateTime deliveryDate);
        public Task<bool> DeliverShipment(Guid id);
        public Task<bool> ReturnShipment(Guid id);
    }
}
