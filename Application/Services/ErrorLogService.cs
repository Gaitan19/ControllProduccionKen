using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Models;
using Infrastructure.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ErrorLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task LogAsync(ErrorLogDTO dto)
        {
            var entity = new ErrorLog
            {
                TimeUtc = DateTime.Now, // local (no UTC)
                Level = dto.Level,
                Message = dto.Message,
                Exception = dto.Exception,
                StackTrace = dto.StackTrace,
                RequestPath = dto.RequestPath,
                RequestMethod = dto.RequestMethod,
                QueryString = dto.QueryString,
                RequestBody = dto.RequestBody,
                UserId = dto.UserId,
                Ipaddress = dto.IPAddress,
                UserAgent = dto.UserAgent,
                CorrelationId = dto.CorrelationId
            };

            await _unitOfWork.ErrorLogRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
