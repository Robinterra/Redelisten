public class LivestreamSubscribe
{
    #region get/set

        public string ConnectionId
        {
            get;
        }

        public string RaumId
        {
            get;
        }

        #endregion get/set

        #region ctor

        public LivestreamSubscribe(string connectionId, string raumId)
        {
            this.ConnectionId = connectionId;
            this.RaumId = raumId;
        }

        #endregion ctor
}