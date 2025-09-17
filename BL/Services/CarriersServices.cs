using AutoMapper;
using BL.Contract;
using BL.Dtos;
using DAL.Contracts;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class CarriersServices :BaseService<TbCarrier,CarrierDto>,ICarriersServices
    {
        public CarriersServices(IGenericRepository<TbCarrier> repo, IMapper mapper,
            IUserService userService) : base(repo, mapper, userService)
        { 
        }
    }
}
