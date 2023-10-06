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

    [HttpGet]
    public IActionResult Get()
    {
        if (!this.Request.Cookies.TryGetValue("token", out string? token)) return Unauthorized("Nicht angemeldet");
        if (!Guid.TryParse(token, out Guid tokenGuid)) return Unauthorized("Nicht angemeldet");

        User? user = userRepo.Retrieve(tokenGuid);
        if (user is null) return Unauthorized("Nicht angemeldet");

        return this.Ok(user);
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
