using BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contract.Shipment
{
    public interface IShipmentStatusHandlerFactory
    {
       public IShipmentStatesHandler GetHandler(ShipmentStatusEnum currentState);
    }
}
