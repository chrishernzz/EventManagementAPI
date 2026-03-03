using EventManagementAPI.Controllers;
using EventManagementAPI.Data;
using EventManagementAPI.Dtos;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

//this will be responsible for talking directly to the Db
namespace EventManagementAPI.Repositories
{
    public class RegistrationRepository : IRegistrationRepository {
        private readonly ApplicationDbContext _context;

        //constructor
        public RegistrationRepository(ApplicationDbContext context) {
            _context = context;
        }
        //precondition: go to the Db
        //postcondition: returns the Registrations table in a list by EventId
        public IEnumerable<Registration> GetByEventId(Guid eventId) {
            return _context.Registrations.AsNoTracking().Where(r => r.EventId == eventId).ToList();
        }

        //precondition: go to the Db
        //postcondition: adds a Registration to the table (Db)
        public Registration Add(Registration registration) {
            _context.Registrations.Add(registration);
            _context.SaveChanges();
            return registration;
        }

        //precondition: go to the Db
        //postcondition: this will check the Db and check if event id exists 
        public bool Exists(Guid eventId, Guid userId) {
            return _context.Registrations.Any(r => r.EventId == eventId && r.UserId == userId);
        }

        //precondition: go to the Db
        //postcondition: this check if it exists but using the asynchronous method
        public Task<bool> ExistsAsync(Guid eventId, Guid userId) {
            return _context.Registrations.AnyAsync(r => r.EventId == eventId && r.UserId == userId);
        }

        //precondition: go to the Db
        //postcondition: this will add a registration using the asynchronous method
        public async Task AddAsync(Registration registration) {
            await _context.Registrations.AddAsync(registration);
        }

        //precondition: go to the Db
        //postcondition: this will now save all added registrations using the asynchronous method
        public Task<int> SaveChangesAsync() {
            return _context.SaveChangesAsync();
        }

        //precondition: go to the Db
        //postcondition: getting the total events by id and sending it back to EventService
        public Task<int> CountByEventIdAsync(Guid eventId) {
            return _context.Registrations.AsNoTracking().Where(r => r.EventId == eventId).CountAsync();
        }

        //precondition:
        //postcondition:
        public async Task<PagedResult<RegistrationListItemDto>> GetPagedByEventIdAsync(Guid eventId, int page, int pageSize, string? status,string? search) {
            IQueryable<Registration> query = _context.Registrations.AsNoTracking().Where(r => r.EventId == eventId);
           
            if (!string.IsNullOrWhiteSpace(status)) {
                query = query.Where(r => r.Status == status);
            }

            if (!string.IsNullOrWhiteSpace(search)) {
                string pattern = "%" + search.Trim() + "%";
                query = query.Where(r =>
                    r.User != null &&
                    (
                        EF.Functions.Like(r.User.FirstName, pattern) ||
                        EF.Functions.Like(r.User.LastName, pattern) ||
                        EF.Functions.Like(r.User.Email, pattern)
                    ));
            }

            int totalCount = await query.CountAsync();

            List<RegistrationListItemDto> items = await query
                .OrderByDescending(r => r.RegisteredAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new RegistrationListItemDto
                 {
                    UserId = r.UserId,
                    FirstName = r.User!.FirstName,
                    LastName = r.User!.LastName,
                    Email = r.User!.Email,
                    RegisteredAt = r.RegisteredAt,
                    Status = r.Status
                })
                .ToListAsync();

            PagedResult<RegistrationListItemDto> result = new PagedResult<RegistrationListItemDto>();
            result.Items = items;
            result.TotalCount = totalCount;
            result.Page = page;
            result.PageSize = pageSize;

            return result;

        }

    }
}
