using Microsoft.AspNetCore.Mvc;
using Redelisten.App.Interfaces;

[ApiController]
[Route("Redeliste/{redelisteName}/[controller]")]
public class MeldungController : ControllerBase
{
    private readonly ILiveService<LiveHub> hubContext;
    private readonly IMeldungRepo meldungRepo;
    private IMeldungHistoryRepo meldungHistoryRepo;
    private readonly IRedelisteRepo redelisteRepo;
    private readonly IUserRepo userRepo;

    public MeldungController(ILiveService<LiveHub> hubContext, IMeldungRepo meldungRepo, IMeldungHistoryRepo meldungHistoryRepo, IRedelisteRepo redelisteRepo, IUserRepo userRepo)
    {
        this.hubContext = hubContext;
        this.meldungRepo = meldungRepo;
        this.meldungHistoryRepo = meldungHistoryRepo;
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

    private void IncreaseHistoryCount(int userId, string redelisteName)
    {
        if (meldungHistoryRepo.Retrieve(userId, redelisteName) is null)
            meldungHistoryRepo.Create(userId, redelisteName);
        meldungHistoryRepo.IncreaseCount(userId, redelisteName);
    }
    
}