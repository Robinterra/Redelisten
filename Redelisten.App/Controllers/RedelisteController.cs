using Microsoft.AspNetCore.Mvc;

namespace Redelisten.App.Controllers;

[ApiController]
[Route("[controller]")]
public class RedelisteController : ControllerBase
{

    private readonly IUserRepo userRepo;
    private readonly IRedelisteRepo redelisteRepo;
    private readonly ILiveService<LiveHub> hubContext;

    public RedelisteController(IUserRepo userRepo, IRedelisteRepo redelisteRepo, ILiveService<LiveHub> hubContext)
    {
        this.userRepo = userRepo;
        this.redelisteRepo = redelisteRepo;
        this.hubContext = hubContext;
    }

    [HttpPost("create")]
    public IActionResult Create(CreateRedelisteDto createRedelisteDto)
    {
        Redeliste? redeliste = redelisteRepo.Create(createRedelisteDto);

        if (redeliste is null) return Redirect($"/redeliste/{createRedelisteDto.Name}");
        
        return Ok(new { redeliste.Name });
    }

    [HttpGet("{name}")]
    public IActionResult Retrieve(string name)
    {
        Redeliste? redeliste = redelisteRepo.Retrieve(name);
        if (redeliste is null)
            return NotFound();
        return Ok(new { redeliste.Name });
    }

    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        this.hubContext.Send("RedelisteDeleted", new { name });

        return redelisteRepo.Delete(name) ? Ok() : NotFound();
    }
}