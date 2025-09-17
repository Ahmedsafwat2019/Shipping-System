﻿using BL.Contract;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using AutoMapper;
using DAL.Repositories;
namespace BL.Services
{
    public class BaseService<T, DTO> : IBaseService<T,DTO> where T : BaseTable
    {
        readonly IGenericRepository<T> _repo;
        readonly IMapper _mapper;
        readonly IUserService _userService;
        readonly IUnitOfWork _UnitOfWork;
        public BaseService(IGenericRepository<T> repo, IMapper mapper,
            IUserService userService) 
        {
            _repo=repo;
            _mapper=mapper;
            _userService=userService;
        }
        public BaseService(IUnitOfWork unitofwork, IMapper mapper,
            IUserService userService)
        {
            _UnitOfWork = unitofwork;
            _repo = unitofwork.Repository<T>();
            _mapper = mapper;
            _userService = userService;
        }
        public  async Task<List<DTO>> GetAll()
        {
            var list= await _repo.GetAll();
            return _mapper.Map<List<T>, List<DTO>>(list);
        }

        public async Task<DTO> GetById(Guid id)
        {
            var obj=  await _repo.GetById(id);
            return _mapper.Map<T,DTO>(obj);
        }

        public async Task<bool> Add(DTO entity)
        {
            var dbObject= _mapper.Map<DTO,T>(entity);
            dbObject.CreatedBy = _userService.GetLoggedInUser();
            dbObject.CurrentState = 1;
            return await _repo.Add(dbObject);
        }

        public bool Add(DTO entity, out Guid id)
        {
            var dbObject = _mapper.Map<DTO, T>(entity);
            dbObject.CreatedBy = _userService.GetLoggedInUser();
            dbObject.CurrentState = 1;
            return _repo.Add(dbObject,out id);
        }

        public  async Task<bool> Update(DTO entity)
        {
           
            var dbObject = _mapper.Map<DTO,T>(entity);
            dbObject.UpdatedBy = _userService.GetLoggedInUser();
            dbObject.CurrentState = 4;
            return await _repo.Update(dbObject);
        }

        public async Task<bool> ChangeStatus(Guid id, int status = 1)
        {
            return  await _repo.ChangeStatus(id, _userService.GetLoggedInUser(), status);
        }

        public async Task<bool> UpdateFields(Guid id, Action<T> Fields)
        {
            return await _repo.UpdateFields(id, entity =>
            {
                // Apply the passed-in field updates
                Fields(entity);

                // Optional: Add domain-specific logic here (مثلاً تعديل بيانات المستخدم اللي عمل التعديل)
                entity.UpdatedBy = _userService.GetLoggedInUser();
                entity.UpdatedDate = DateTime.UtcNow;
            });
        }
    }
}
