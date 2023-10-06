public interface ILiveService<THub>
{
    #region methods

    bool Send<TSendObj>(string method, TSendObj tobj);

    Task Send<TSendObj>(string method, string connectionId, TSendObj tobj);

    #endregion methods

}