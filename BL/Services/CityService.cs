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
    public class CityService : BaseService<TbCity, CityDto>, ICity
    {
        IGenericRepository<TbCity> _Repo;
        IMapper _mapper;
        public CityService(IGenericRepository<TbCity> repo,IMapper mapper,
            IUserService userService) : base(repo, mapper, userService)
        {
            _Repo = repo;
            _mapper = mapper;
        }

        public async Task<List<CityDto>> GetAllCitites()
        {
            var cities =  await _Repo.GetList(a => a.CurrentState == 1);
            return _mapper.Map<List<TbCity>,List<CityDto>>(cities);
        }


        public async Task<List<CityDto>> GetByCountry(Guid countryId)
        {
            var cities = await _Repo.GetList(a => a.CurrentState == 1 &&
            a.CountryId == countryId);
            return _mapper.Map<List<TbCity>, List<CityDto>>(cities);
        }
    }
}
