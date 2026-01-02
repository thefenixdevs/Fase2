using Moq;
using UsersAPI.Application.DTOs.CreateUser;
using UsersAPI.Application.Interfaces;
using UsersAPI.Application.UseCases.CreateUser;
using UsersAPI.Domain.ValueObjects;
using Xunit;

namespace UsersAPI.Application.Tests.UseCases.CreateUser;

public class CreateUserUseCaseTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly CreateUserUseCase _useCase;

    public CreateUserUseCaseTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();

        _useCase = new CreateUserUseCase(
            _userRepositoryMock.Object,
            _passwordHasherMock.Object
        );
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCreateUser_WhenDataIsValid()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            Name = "Adriano",
            Email = "adriano@email.com",
            Password = "StrongPass123"
        };

        _userRepositoryMock
            .Setup(r => r.ExistsByEmailAsync(It.IsAny<Email>()))
            .ReturnsAsync(false);

        _passwordHasherMock
            .Setup(h => h.Hash(It.IsAny<Password>()))
            .Returns("hashed-password");

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(request.Name, result.Value!.Name);
        Assert.Equal(request.Email.ToLower(), result.Value.Email);

        _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Domain.Entities.User>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldFail_WhenEmailAlreadyExists()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            Name = "Adriano",
            Email = "adriano@email.com",
            Password = "StrongPass123"
        };

        _userRepositoryMock
            .Setup(r => r.ExistsByEmailAsync(It.IsAny<Email>()))
            .ReturnsAsync(true);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("email_already_exists", result.Error!.Code);

        _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldFail_WhenRequestIsInvalid()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            Name = "",
            Email = "",
            Password = ""
        };

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("validation_error", result.Error!.Code);

        _userRepositoryMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ExecuteAsync_ShouldFail_WhenEmailIsInvalid()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            Name = "Adriano",
            Email = "invalid-email",
            Password = "StrongPass123"
        };

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("domain_validation_error", result.Error!.Code);
    }
}