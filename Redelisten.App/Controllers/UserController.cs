using Microsoft.AspNetCore.Mvc;

namespace Redelisten.App.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepo userRepo;

    public UserController(IUserRepo userRepo)
    {
        this.userRepo = userRepo;
    }

    [HttpPost("create")]
    public IActionResult Create(CreateUserDto createUserDto)
    {
        User user = this.userRepo.Create(createUserDto);

        this.Response.Cookies.Append("test", user.Id.ToString());

        return this.Ok(user);
    }
}