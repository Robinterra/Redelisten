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
        User? user = this.userRepo.Create(createUserDto);
        if (user is null) return this.Conflict(new { status = "User already exists" });

        this.Response.Cookies.Append("token", user.Token.ToString());
        this.Response.Cookies.Append("user-id", user.Id.ToString());

        return this.Ok(user);
    }

    [HttpGet("{id}/retrieve")]
    public IActionResult Retrieve(int id)
    {
        User? user = this.userRepo.Retrieve(id);

        if (user is null)
            return NotFound();
        return Ok(new RetrieveUserDto(user));
    }
}
