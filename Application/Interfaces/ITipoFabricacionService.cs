using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITipoFabricacionService
    {
        Task<IEnumerable<TipoFabricacionDto>> GetAllAsync();
        Task<TipoFabricacionDto> GetByIdAsync(int id);
        Task CreateAsync(TipoFabricacionDto dto);
        Task UpdateAsync(int id, TipoFabricacionDto dto);
        Task DeleteAsync(int id);
    }
}
