using AutoMapper;
using BL.Contract;
using BL.Contract.Shipment;
using BL.Dtos;
using DAL.Contracts;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Shipment
{
    public class ShipmentWorkflowServices :IShipmentWorkflowServices
    {
        IShipment _shipmentService;
        IUserReceiver _userReceiver;
        IUserSender _userSender;
        ITrackingNumberCreator _trackingCreator;
        IRateCalculator _rateCalculator;
        IUnitOfWork _uow;
        IUserService _userService;
        IGenericRepository<TbShipment> _repo;
        IMapper _mapper;
        IShipmentStatus _shipmentStatus;
        public ShipmentWorkflowServices( IShipment shipment, IGenericRepository<TbShipment> repo, IMapper mapper,
             IUserService userService ,IUserReceiver userReceiver,
             IUserSender userSender, ITrackingNumberCreator trackingCreator
            , IRateCalculator rateCalculator, IShipmentStatus shipmentStatus, IUnitOfWork uow) 
        {
           _shipmentService= shipment;
            _uow = uow;
            _repo = repo;
            _mapper = mapper;
            _userReceiver = userReceiver;
            _userSender = userSender;
            _trackingCreator = trackingCreator;
            _rateCalculator = rateCalculator;
            _userService = userService;
            _shipmentStatus = shipmentStatus;
        }
        public async Task ApproveShipment(ShipmentDto dto)
        {
            try
            {
                await _uow.BeginTransactionAsync();

                // save sender
                dto.UserSender.Id = dto.SenderId;
               await _userSender.Update(dto.UserSender);
                // save receiver
                dto.UserReceiver.Id = dto.ReceiverId;
               await  _userReceiver.Update(dto.UserReceiver);
                // save shipment
               await _shipmentService.Update(dto);
                await _uow.CommitAsync();
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();

                throw new Exception();
            }
        }
        public async Task<bool> ReadyShipment(ShipmentDto dto)
        {
            var result = _repo.GetById(dto.Id);
            if (result == null)
            {
                return false;
            }
            dto.CurrentState = 3;
             await _shipmentService.Update(dto);
            return true;
        }

        public async Task<bool> ShippingShipment(Guid id, DateTime deliveryDate)
        {
            var result = await _repo.GetById(id);
            if (result == null)
            {
                return false;
            }
            int status = (int)ShipmentStatusEnum.Shipped;
            var userId = _userService.GetLoggedInUser();
           await  _repo.UpdateFields(id, a =>
            {
                a.CurrentState = status;
                a.DelivryDate = deliveryDate;
                a.UpdatedDate = DateTime.Now;
                a.UpdatedBy = userId;
            });
            return true;
        }

        public async Task<bool> DeliverShipment(Guid id)
        {
            var result = await _repo.GetById(id);
            if (result == null)
            {
                return false;
            }
            await  _shipmentService.ChangeStatus(id, 5);
            return true;
        }

        public async Task<bool> ReturnShipment(Guid id)
        {
            var result = await _shipmentService.GetShipment(id);
            if (result == null)
            {
                return false;
            }
          await  _shipmentService.ChangeStatus(id, 6);
            return true;
        }
    }
}
