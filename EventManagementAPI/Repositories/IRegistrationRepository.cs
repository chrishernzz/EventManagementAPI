using EventManagementAPI.Dtos;
using EventManagementAPI.Models;

namespace EventManagementAPI.Repositories
{
    //header - contains the methods that the system should have and this is repository so only reads data
    public interface IRegistrationRepository {
        IEnumerable<Registration> GetByEventId(Guid eventId);
        Registration Add(Registration registration);
        bool Exists(Guid eventId, Guid userId);
        Task<bool> ExistsAsync(Guid eventId, Guid userId);
        Task AddAsync(Registration registration);
        Task<int> SaveChangesAsync();
        Task<int> CountByEventIdAsync(Guid eventId);
        Task<PagedResult<RegistrationListItemDto>> GetPagedByEventIdAsync(Guid eventId, int page, int pageSize, string? status, string? search);
    }
}
