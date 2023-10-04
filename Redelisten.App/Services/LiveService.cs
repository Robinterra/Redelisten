using Microsoft.AspNetCore.SignalR;

public class LiveService<THub> : ILiveService<THub> where THub : Hub
{

    #region vars

    private IHubContext<THub> hubContext;

    #endregion vars

    #region ctor

    public LiveService(IHubContext<THub> hubContext)
    {
        this.hubContext = hubContext;
    }

    #endregion ctor

    #region methods

    public bool Send<TSendObj>(string method, TSendObj tobj)
    {
        this.hubContext.Clients.All.SendAsync(method, tobj).Wait();

        //ThreadPool.QueueUserWorkItem(t => this.hubContext.Clients.All.SendAsync(method, tobj));

        return true;
    }

    public Task Send<TSendObj>(string method, string connectionId, TSendObj tobj)
    {
        IClientProxy clientProxy = this.hubContext.Clients.Client(connectionId);

        return clientProxy.SendAsync(method, tobj);
    }

    #endregion methods

}