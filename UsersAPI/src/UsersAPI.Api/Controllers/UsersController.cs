using Microsoft.AspNetCore.Mvc;
using UsersAPI.Application.Requests;
using UsersAPI.Application.Responses;

namespace UsersAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public ActionResult<CreateUserResponse> Create(CreateUserRequest request)
    {
        // Call application use case (later)
        return Ok();
    }
}
