public interface ILivestreamSubscribeRepo
{
    bool Add(LivestreamSubscribe dbelem);
    IEnumerable<LivestreamSubscribe> GetFromUserId(string userid);
    bool Remove(string connectionId);
}