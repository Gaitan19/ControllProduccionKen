using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Models;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TipoFabricacionService : ITipoFabricacionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; // Si usas AutoMapper

        public TipoFabricacionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoFabricacionDto>> GetAllAsync()
        {
            var tipos = await _unitOfWork.TipoFabricacionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TipoFabricacionDto>>(tipos);
        }

        public async Task<TipoFabricacionDto> GetByIdAsync(int id)
        {
            var tipo = await _unitOfWork.TipoFabricacionRepository.GetByIdAsync(id);
            return _mapper.Map<TipoFabricacionDto>(tipo);
        }

        public async Task CreateAsync(TipoFabricacionDto dto)
        {
            var entity = _mapper.Map<TipoFabricacion>(dto);
            await _unitOfWork.TipoFabricacionRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, TipoFabricacionDto dto)
        {
            var entity = await _unitOfWork.TipoFabricacionRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Tipo de fabricación no encontrado");
            }
            // Actualizar propiedades
            entity.Descripcion = dto.Descripcion;
            entity.Activo = dto.Activo;

            _unitOfWork.TipoFabricacionRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _unitOfWork.TipoFabricacionRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Tipo de fabricación no encontrado");
            }

            _unitOfWork.TipoFabricacionRepository.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
