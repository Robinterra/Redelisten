namespace Redelisten.App.Services;

public class RemoveOldHostedService : IHostedService
{
    private IUserRepo userRepo;

    private Timer? timer;

    public RemoveOldHostedService(IUserRepo userRepo)
    {
        this.userRepo = userRepo;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        this.timer = new Timer(RemoveOldEntries, state: null, dueTime: TimeSpan.Zero, period: TimeSpan.FromDays(1));

        return Task.CompletedTask;
    }

    private void RemoveOldEntries(object? state)
    {
        userRepo.LifetimeDelete();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        this.timer?.Dispose();
        
        return Task.CompletedTask;
    }
}