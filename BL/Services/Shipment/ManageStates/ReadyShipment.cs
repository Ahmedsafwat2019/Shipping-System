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
    public class ReadyShipment : IShipmentStatesHandler
    {
        IShipment _shipment;
        IShipmentStatus _status;
        IUserService _userService;
        public ReadyShipment(IUserService userService,IShipment shipment, IShipmentStatus status)
        {
            _shipment = shipment;
            _status = status;
            _userService = userService;
        }
        public ShipmentStatusEnum TargetState { get => ShipmentStatusEnum.ReadyForShip; }
        public async Task HandleState(ShipmentDto shipmentDto)
        {
            var userId = _userService.GetLoggedInUser();
            await _shipment.UpdateFields(shipmentDto.Id, a =>
            {
                a.CarrierId = shipmentDto.CarrierId;
                a.CurrentState = (int)TargetState;
               });
            await _shipment.ChangeStatus(shipmentDto.Id, (int)TargetState);

        }
    }
}
