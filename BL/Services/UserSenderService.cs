using AutoMapper;
using BL.Contract;
using BL.Dtos;
using DAL.Contracts;
using DAL.Repositories;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserSenderService : BaseService<TbUserSender, UserSenderDto>,IUserSender
    {
        IUnitOfWork _uow;
        public UserSenderService(IGenericRepository<TbUserSender> repo,IMapper mapper,
             IUserService userService, IUnitOfWork uow) : base(uow, mapper, userService)
        {
            _uow = uow;
        }
    }
}
