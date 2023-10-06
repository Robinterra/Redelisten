using Microsoft.AspNetCore.Mvc;
using Redelisten.App.Interfaces;
using Redelisten.App.Models.Entities;

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

    [HttpGet]
    public IActionResult GetAll(string redelisteName)
    {
        Redeliste? redeliste = redelisteRepo.Retrieve(redelisteName);
        if (redeliste is null) return NotFound("Redeliste nicht gefunden");
        if (!this.Request.Cookies.TryGetValue("token", out string? token)) return Unauthorized("Nicht angemeldet");
        if (!Guid.TryParse(token, out Guid tokenGuid)) return Unauthorized("Nicht angemeldet");

        User? user = userRepo.Retrieve(tokenGuid);
        if (user is null) return Unauthorized("Nicht angemeldet");

        List<Meldung> meldungen = meldungRepo.Retrieve(redelisteName);
        meldungen = meldungen.OrderByDescending(meldung => meldung.Order).ToList();

        return Ok(meldungen);
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

        List<Meldung> meldungen = meldungRepo.Retrieve(redelisteName);
        bool isok = meldungen.Count == 1 ? hubContext.Send("CurrentMeldung", new MeldungReport(user.Name)) : hubContext.Send("NeueMeldung", new MeldungReport(user.Name));

        return Ok(result);
    }

    [HttpPost("Done")]
    public IActionResult Done(string redelisteName)
    {
        Redeliste? redeliste = redelisteRepo.Retrieve(redelisteName);
        if (redeliste is null) return NotFound("Redeliste nicht gefunden");
        if (!this.Request.Cookies.TryGetValue("token", out string? token)) return Unauthorized("Nicht angemeldet");
        if (!Guid.TryParse(token, out Guid tokenGuid)) return Unauthorized("Nicht angemeldet");

        User? user = userRepo.Retrieve(tokenGuid);
        if (user is null) return Unauthorized("Nicht angemeldet");
        if (redeliste.Moderator != user) return Unauthorized("Nicht der Moderator");

        List<Meldung> meldungen = meldungRepo.Retrieve(redelisteName);
        meldungen = meldungen.OrderByDescending(meldung => meldung.Order).ToList();
        Meldung? currentMeldung = meldungen.FirstOrDefault();
        if (currentMeldung is null) return NotFound("Keine Meldungen gefunden");
        
        UpdateHistory(currentMeldung);
        meldungen.Remove(currentMeldung);
        meldungRepo.Delete(currentMeldung);
        hubContext.Send("DoneMeldung", currentMeldung);

        Meldung? nextMeldung = meldungen.FirstOrDefault();
        if (nextMeldung is null)
        {
            hubContext.Send("KeineMeldung", "null");
            return Ok(null);
        }

        hubContext.Send("CurrentMeldung", new MeldungReport(user.Name));
        return Ok(nextMeldung);
    }

    private void UpdateHistory(Meldung meldung)
    {
        MeldungHistory? history = meldungHistoryRepo.Retrieve(meldung.UserID, meldung.RedelistenName);
        if (history is null)
            history = meldungHistoryRepo.Create(meldung.UserID, meldung.RedelistenName);
        meldungHistoryRepo.IncreaseCount(history!);
    }

}