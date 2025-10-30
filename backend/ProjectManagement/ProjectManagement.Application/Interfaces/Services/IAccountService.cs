using ProjectManagement.Application.Dtos.Request;
using ProjectManagement.Application.Dtos.Response;

namespace ProjectManagement.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerRequest);

    }
}
