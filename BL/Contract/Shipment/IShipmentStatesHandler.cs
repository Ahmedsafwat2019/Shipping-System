using BL.Dtos;
using BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contract.Shipment
{
    public interface IShipmentStatesHandler
    {
        public ShipmentStatusEnum TargetState { get; }
        public Task HandleState(ShipmentDto shipment);
    }
}
