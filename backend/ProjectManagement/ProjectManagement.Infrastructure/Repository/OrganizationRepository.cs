using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagement.Application.Interfaces.Repository;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Infrastructure.Data;

namespace ProjectManagement.Infrastructure.Repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrganizationRepository> _logger;
        public OrganizationRepository(AppDbContext context, ILogger<OrganizationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Organization?> GetOrganizationByNameAsync(string name)
        {
            try
            {
                return await _context.Organizations.FirstOrDefaultAsync(o => o.Name == name);
            }
            catch (Exception ex) 
            { 
                _logger.LogError(ex,"Database error while fecthing Organization details By name - {OrganizationName}",name);
                throw;
            }
        }

        public async Task<Organization> AddOrganizationAsync(Organization organziation)
        {
            try
            {
                _context.Organizations.Add(organziation);
                await _context.SaveChangesAsync();
                return organziation;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Database error while adding organization - {OrganizationName}",organziation.Name);
                throw;
            }
        }
    }
}
