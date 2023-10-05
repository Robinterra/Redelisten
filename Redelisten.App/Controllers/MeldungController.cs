using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Redeliste/{redelisteName}/[controller]")]
public class MeldungController : ControllerBase
{
    private readonly ILiveService<LiveHub> hubContext;
    private readonly IMeldungRepo meldungRepo;

    private readonly IRedelisteRepo redelisteRepo;
    private readonly IUserRepo userRepo;

    public MeldungController(ILiveService<LiveHub> hubContext, IMeldungRepo meldungRepo, IRedelisteRepo redelisteRepo, IUserRepo userRepo)
    {
        this.hubContext = hubContext;
        this.meldungRepo = meldungRepo;
        this.redelisteRepo = redelisteRepo;
        this.userRepo = userRepo;
    }

    [HttpPost]
    public IActionResult NeueMeldung(string redelisteName)
    {
        if (redelisteRepo.Retrieve(redelisteName) is null) return NotFound("Redeliste nicht gefunden");
        if (!this.Request.Cookies.TryGetValue("token", out string? token)) return Unauthorized("Nicht angemeldet");
        if (!Guid.TryParse(token, out Guid tokenGuid)) return Unauthorized("Nicht angemeldet");

        User? user = userRepo.Retrieve(tokenGuid);
        if (user is null) return Unauthorized("Nicht angemeldet");

        Meldung? result = meldungRepo.Create(new(user, redelisteName));
        if (result is null) return BadRequest("Meldung konnte nicht erstellt werden");

        hubContext.Send("NeueMeldung", result);

        return Ok(result);
    }
}