namespace Redelisten.App.Services;

public class RemoveOldHostedService : IHostedService
{
    private IUserRepo userRepo;

    private IRedelisteRepo redelisteRepo;

    private IMeldungRepo meldungRepo;

    private Timer? timer;

    public RemoveOldHostedService(IUserRepo userRepo, IRedelisteRepo redelisteRepo, IMeldungRepo meldungRepo)
    {
        this.userRepo = userRepo;
        this.redelisteRepo = redelisteRepo;
        this.meldungRepo = meldungRepo;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        this.timer = new Timer(RemoveOldEntries, state: null, dueTime: TimeSpan.Zero, period: TimeSpan.FromDays(1));

        return Task.CompletedTask;
    }

    private void RemoveOldEntries(object? state)
    {
        List<User> toDelete = userRepo.UsersToDelete();
        meldungRepo.Delete(toDelete);
        List<Redeliste> deletedRedelisten = redelisteRepo.Delete(toDelete);
        foreach (Redeliste redeliste in deletedRedelisten)
        {
            meldungRepo.DeleteFromRedeliste(redeliste.Name);
        }
        userRepo.Delete(toDelete);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        this.timer?.Dispose();
        
        return Task.CompletedTask;
    }
}