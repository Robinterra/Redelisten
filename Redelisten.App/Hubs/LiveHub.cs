using Microsoft.AspNetCore.SignalR;

public class LiveHub : Hub
{

    #region vars

    private ILivestreamSubscribeRepo livestreamSubscribeRepo;

    //private IUserAuthService userAuthService;

    #endregion vars

    #region ctor

    public LiveHub(ILivestreamSubscribeRepo livestreamSubscribeRepo/*, IUserAuthService userAuthService*/)
    {
        this.livestreamSubscribeRepo = livestreamSubscribeRepo;
    }

    #endregion ctor

    #region methods

    public override Task OnConnectedAsync()
    {
        //string username = userAuthService.GetUsername();
        string connectionId = this.Context.ConnectionId;

        this.Context.GetHttpContext()!.Request.Cookies.TryGetValue("redeliste", out string? redeliste);

        LivestreamSubscribe subscribe = new LivestreamSubscribe(connectionId, redeliste!);
        this.livestreamSubscribeRepo.Add(subscribe);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        string connectionId = this.Context.ConnectionId;

        this.livestreamSubscribeRepo.Remove(connectionId);

        return base.OnDisconnectedAsync(exception);
    }

    #endregion methods

}