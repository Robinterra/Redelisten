public class LivestreamSubscribeRepo : ILivestreamSubscribeRepo
{

    #region vars

    private List<LivestreamSubscribe> db;

    #endregion vars

    #region ctor

    public LivestreamSubscribeRepo()
    {
        this.db = new List<LivestreamSubscribe>();
    }

    public LivestreamSubscribeRepo(List<LivestreamSubscribe> db)
    {
        this.db = db;
    }

    #endregion ctor

    #region methods

    public bool Add(LivestreamSubscribe dbelem)
    {
        this.db.Add(dbelem);

        return true;
    }

    public IEnumerable<LivestreamSubscribe> GetFromUserId(string userid)
    {
        IEnumerable<LivestreamSubscribe> result = this.db.Where(t=>t.RaumId == userid);

        return result;
    }

    public bool Remove(string connectionId)
    {
        this.db.RemoveAll(t=>t.ConnectionId == connectionId);

        return true;
    }

    #endregion methods

}
