using AutoMapper;
using AutoMapper.QueryableExtensions;
using BL.Contract;
using BL.Contract.Shipment;
using BL.Dtos;
using DAL.Contracts;
using DAL.Models;
using DAL.Repositories;
using Domains;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ShipmentStatusService : BaseService<TbShipmentStatus, ShippmentStatusDto>, IShipmentStatus
    {
        IUnitOfWork _uow;
        IUserService _userService;
        IGenericRepository<TbShipment> _repo;
        IMapper _mapper;
        public ShipmentStatusService(IGenericRepository<TbShipment> repo, IMapper mapper,
             IUserService userService, IUnitOfWork uow) : base(uow, mapper, userService)
        {
            _uow = uow;
            _repo = repo;
            _mapper = mapper;
            _userService = userService;
        }

    }
}
