namespace UsersAPI.Application.Responses;

/// <summary>
/// Output data for the Create User use case.
/// </summary>
public class CreateUserResponse
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = default!;
    public string Email { get; init; } = default!;
}
