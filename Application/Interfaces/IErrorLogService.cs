using Application.DTOs;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IErrorLogService
    {
        Task LogAsync(ErrorLogDTO dto);
    }
}
