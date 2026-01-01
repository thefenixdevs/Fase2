using UsersAPI.Application.Common;
using UsersAPI.Application.Requests;
using UsersAPI.Application.Responses;

namespace UsersAPI.Application.UseCases.CreateUser
{
    public interface ICreateUserUseCase
    {
        Task<Result<CreateUserResponse>> ExecuteAsync(CreateUserRequest request);
    }
}