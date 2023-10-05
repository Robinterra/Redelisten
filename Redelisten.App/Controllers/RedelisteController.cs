using Microsoft.AspNetCore.Mvc;

namespace Redelisten.App.Controllers;

[ApiController]
[Route("[controller]")]
public class RedelisteController : ControllerBase
{

    private readonly IUserRepo userRepo;
    private readonly IRedelisteRepo redelisteRepo;
    private readonly ILiveService<LiveHub> hubContext;
    private readonly IMeldungRepo meldungRepo;

    public RedelisteController(IUserRepo userRepo, IRedelisteRepo redelisteRepo, IMeldungRepo meldungRepo, ILiveService<LiveHub> hubContext)
    {
        this.userRepo = userRepo;
        this.redelisteRepo = redelisteRepo;
        this.meldungRepo = meldungRepo;
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

        List<User> meldungen = LoadMeldungen(redeliste);
        
        return Ok(new RetrieveRedelisteDto(redeliste, meldungen));
    }

    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        this.hubContext.Send("RedelisteDeleted", new { name });

        return redelisteRepo.Delete(name) is not null ? Ok() : NotFound();
    }
    private List<User> LoadMeldungen(Redeliste redeliste)
    {
        var meldungen = meldungRepo.Retrieve(redeliste.Name);
        IEnumerable<User> users = meldungen.Select(meldung => userRepo.Retrieve(meldung.UserID)).OfType<User>();
        return users.ToList();
    }
}