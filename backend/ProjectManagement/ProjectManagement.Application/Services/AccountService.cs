using Microsoft.Extensions.Logging;
using ProjectManagement.Application.Dtos.Request;
using ProjectManagement.Application.Dtos.Response;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Application.Interfaces.Repository;
using ProjectManagement.Application.Interfaces.Services;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Enums;
using System.Security.Cryptography;
using System.Text;
namespace ProjectManagement.Application.Services
{
    public class AccountService : IAccountService

    {
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IUserRepository userRepository, IOrganizationRepository organizationRepository, ILogger<AccountService> logger)
        {
            _userRepository = userRepository;
            _organizationRepository = organizationRepository;
            _logger = logger;
        }

        public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest)
        {
            _logger.LogInformation("User registration process has started ...");
            try
            {

                var existingUser = await _userRepository.GetUserByEmailAsync(registerUserRequest.Email);
                if (existingUser != null)
                {
                    _logger.LogWarning("User Registration process has failed: {Email} already exists", registerUserRequest.Email);

                    return new RegisterUserResponse
                    {
                        IsSuccess = false,
                        Message = "Email Already Registered "
                    };
                }


                Organization organization;

                if (registerUserRequest.Role == Role.Manager)
                {
                    _logger.LogInformation("Creating new organization {@OrgName}", registerUserRequest.OrganizationName);
                    organization = await _organizationRepository.AddOrganizationAsync(new Organization { Name = registerUserRequest.OrganizationName, Domain = registerUserRequest.Domain });
                }
                else
                {
                    organization = await _organizationRepository.GetOrganizationByNameAsync(registerUserRequest.OrganizationName);
                    if (organization is null)
                    {
                        _logger.LogWarning("Organization not found for registration: {OrgName}", registerUserRequest.OrganizationName);
                        return new RegisterUserResponse
                        {
                            IsSuccess = false,
                            Message = "Organization not found."
                        };
                    }
                    _logger.LogInformation("User registered under existing organization {@OrgName}", registerUserRequest.OrganizationName);
                }

                var verificationToken = Guid.NewGuid().ToString();

                var user = new User
                {
                    FirstName = registerUserRequest.FirstName,
                    LastName = registerUserRequest.LastName,
                    PasswordHash = HashPassword(registerUserRequest.Password),
                    Role = registerUserRequest.Role,
                    OrganizationId = organization.OrganizationId,
                    VerificationToken = verificationToken,
                    Email = registerUserRequest.Email
                };

                await _userRepository.AddUserAsync(user);
                _logger.LogInformation("User with email {Email} registered successfully under organization {OrganizationName}.", registerUserRequest.Email, registerUserRequest.OrganizationName);


                return new RegisterUserResponse
                {
                    IsSuccess = true,
                    Message = "Registration successful. Please check your email to verify your account."
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User registration process failed for {Email} in {Organization}. Error: {ErrorMessage}", registerUserRequest.Email, registerUserRequest.OrganizationName, ex.Message);
                throw;
            }
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
