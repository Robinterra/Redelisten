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
    private new User? User;
    private Redeliste? Redeliste;

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
        //if (!this.Request.Cookies.TryGetValue("token", out string? token)) return Unauthorized("Nicht angemeldet");
        //if (!Guid.TryParse(token, out Guid tokenGuid)) return Unauthorized("Nicht angemeldet");

        //User? user = userRepo.Retrieve(tokenGuid);
        //if (user is null) return Unauthorized("Nicht angemeldet");

        List<Meldung> meldungen = meldungRepo.Retrieve(redelisteName);
        meldungen = meldungen.OrderBy(meldung => meldung.Order).ToList();

        return Ok(meldungen);
    }

    [HttpPost]
    public IActionResult NeueMeldung(string redelisteName, CreateMeldungDto createMeldungDto)
    {
        createMeldungDto.RedelistenName = redelisteName;
        Redeliste = redelisteRepo.Retrieve(createMeldungDto.RedelistenName); 
        if ( Redeliste is null) return NotFound("Redeliste nicht gefunden");
        if (!this.Request.Cookies.TryGetValue("token", out string? token)) return Unauthorized("Nicht angemeldet");
        if (!Guid.TryParse(token, out Guid tokenGuid)) return Unauthorized("Nicht angemeldet");

        User = userRepo.Retrieve(tokenGuid);
        if (User is null) return Unauthorized("Nicht angemeldet");

        Meldung? result = meldungRepo.Create(createMeldungDto, User);
        if (result is null) return BadRequest("Du hast dich bereits gemeldet");
        
        UpdateOrder();

        List<Meldung> meldungen = meldungRepo.Retrieve(createMeldungDto.RedelistenName);
        bool isok = hubContext.Send($"NeueMeldung_{redelisteName}", new MeldungReport(User.Name));

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
        meldungen = meldungen.OrderBy(meldung => meldung.Order).ToList();
        Meldung? currentMeldung = meldungen.FirstOrDefault();
        if (currentMeldung is null)
        {
            hubContext.Send($"KeineMeldung_{redelisteName}", "null");
            return NotFound("Keine Meldungen gefunden");
        }
        
        UpdateHistory(currentMeldung);
        meldungen.Remove(currentMeldung);
        meldungRepo.Delete(currentMeldung);

        User meldeUser = userRepo.Retrieve(currentMeldung.UserID)!;
        hubContext.Send($"CurrentMeldung_{currentMeldung.RedelistenName}", new { User = meldeUser.Name});

        /*Meldung? nextMeldung = meldungen.FirstOrDefault();
        if (nextMeldung is null)
        {
            hubContext.Send($"KeineMeldung_{currentMeldung.RedelistenName}", "null");
            return Ok(null);
        }

        return Ok(nextMeldung);*/

        return Ok(currentMeldung);
    }

    private void UpdateHistory(Meldung meldung)
    {
        MeldungHistory? history = meldungHistoryRepo.Retrieve(meldung.UserID, meldung.RedelistenName);
        if (history is null)
            history = meldungHistoryRepo.Create(meldung.UserID, meldung.RedelistenName);
        meldungHistoryRepo.IncreaseCount(history!);
    }

    private void UpdateOrder()
    {
        List<Meldung> meldungen = meldungRepo.Retrieve(Redeliste!.Name);
        IComparer<(Meldung, MeldungHistory)> comparer;

        comparer = (Redeliste.Algorithm) switch
        {
            Redeliste.PriorityAlgorithm.Queue => new TimeStampComparer(),
            Redeliste.PriorityAlgorithm.Balanced => new BalancedComparer(),
            _ => new TimeStampComparer(),
        };
        
        AssignOrder(meldungen.OrderBy(WithHistory, comparer));
    }

    private void AssignOrder(IEnumerable<Meldung> meldungen)
    {
        int i = 0;
        foreach (var meldung in meldungen)
        {
            meldung.Order = i;
            i++;
        }
    }

    private (Meldung, MeldungHistory) WithHistory(Meldung meldung)
    {
        return (meldung, meldungHistoryRepo.Retrieve(meldung)!);
    }

    
    private class TimeStampComparer : IComparer<(Meldung, MeldungHistory)>
    {
        public int Compare((Meldung, MeldungHistory) x, (Meldung, MeldungHistory) y)
        {
            return DateTime.Compare(x.Item1.CreatedAt, y.Item1.CreatedAt);
        }
    }
    private class BalancedComparer : IComparer<(Meldung, MeldungHistory)>
    {
        private bool HasFirstPriority((Meldung, MeldungHistory) first, (Meldung, MeldungHistory) second)
        {
            return first.Item2.MeldungCount < second.Item2.MeldungCount
                   && first.Item2.LetzterBeitrag < second.Item1.CreatedAt;
        }
        public int Compare((Meldung, MeldungHistory) x, (Meldung, MeldungHistory) y)
        {
            if (HasFirstPriority(x, y))
                return -1;
            
            if (HasFirstPriority(y, x))
                return 1;
            
            return DateTime.Compare(x.Item1.CreatedAt, y.Item1.CreatedAt);

        }
    }
}