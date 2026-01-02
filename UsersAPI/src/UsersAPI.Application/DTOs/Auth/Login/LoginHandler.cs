using UsersAPI.Application.Interfaces;
using UsersAPI.Domain.ValueObjects;

namespace UsersAPI.Application.DTOs.Auth.Login
{
    public sealed class LoginHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwt;

        public LoginHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwt)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwt = jwt;
        }

        public async Task<LoginResponse> HandleAsync(LoginRequest request)
        {
            var email = new Email(request.Email);

            var user = await _userRepository.GetByEmailAsync(email)
                ?? throw new UnauthorizedAccessException("Invalid credentials");

            if (!_passwordHasher.Verify(
                new Password(request.Password),
                user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = _jwt.Generate(user);

            return new LoginResponse
            {
                AccessToken = token,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };
        }
    }
}
