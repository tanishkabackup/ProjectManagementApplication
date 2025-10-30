using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Interfaces.Repository
{
    public interface IOrganizationRepository
    {
        Task<Organization?> GetOrganizationByNameAsync(string name);
        Task<Organization> AddOrganizationAsync(Organization org);
    }
}
