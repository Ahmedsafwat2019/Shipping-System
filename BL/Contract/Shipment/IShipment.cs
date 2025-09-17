using BL.Dtos;
using DAL.Models;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contract
{
    public interface IShipment : IBaseService<TbShipment,ShipmentDto>
    {
        public Task Create(ShipmentDto dto);
        public Task Edit(ShipmentDto dto);
        public Task<List<ShipmentDto>> GetShipments();

        public Task<PagedResult<ShipmentDto>> GetShipments(int pageNumber, int pageSize, bool isUserRole, int? status);
        public Task<PagedResult<ShipmentDto>> GetAllShipments(int pageNumber, int pageSize, bool isUserRole, int? status);

        public Task<ShipmentDto> GetShipment(Guid id);
       
    }
}
