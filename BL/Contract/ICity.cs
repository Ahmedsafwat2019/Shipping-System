using BL.Dtos;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contract
{
    public interface ICity : IBaseService<TbCity,CityDto>
    {
       Task<List<CityDto>> GetAllCitites();

       Task<List<CityDto>> GetByCountry(Guid countryId);
    }
}
