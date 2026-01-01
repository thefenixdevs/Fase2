namespace UsersAPI.Application.Requests;

/// <summary>
/// Input data for the Create User use case.
/// </summary>
public class CreateUserRequest
{
    public string Name { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
}
