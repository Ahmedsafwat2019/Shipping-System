using BL.Contract.Shipment;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Shipment.ManageStates
{
    public class ShipmentStatusHandlerFactory:IShipmentStatusHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public ShipmentStatusHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IShipmentStatesHandler GetHandler(ShipmentStatusEnum shipmentStatus)
        {
            return shipmentStatus switch
            {
                ShipmentStatusEnum.Approved => _serviceProvider.GetRequiredService<ApproveShipment>(),
                ShipmentStatusEnum.Reject =>_serviceProvider.GetRequiredService<RejectShipment>(),
                ShipmentStatusEnum.ReadyForShip => _serviceProvider.GetRequiredService<RejectShipment>(),
                ShipmentStatusEnum.Shipped=> _serviceProvider.GetRequiredService<ShippShipment>(),
                ShipmentStatusEnum.Delivered => _serviceProvider.GetRequiredService<DeliverShipment>(),
                ShipmentStatusEnum.Returned => _serviceProvider.GetRequiredService<ReturnShipment>()
                
            };
        }
    }
}
