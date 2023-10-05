using Redelisten.App.Models.Dtos;
using Redelisten.App.Models.Entities;

namespace Redelisten.App.Repos;

public class MeldungHistoryRepo
{
    private List<MeldungHistory> MeldungHistories = new List<MeldungHistory>();

    public MeldungHistory? Create(int userId, string redelisteName)
    {
        MeldungHistory meldungHistory = new MeldungHistory(userId, redelisteName);

        if (MeldungHistories.Contains(meldungHistory))
            return null;
        
        MeldungHistories.Add(meldungHistory);

        return meldungHistory;
    }

    public MeldungHistory? Retrieve(int userId, string redelisteName)
    {
        return MeldungHistories.Find(t => t.UserId == userId && t.RedelisteName == redelisteName);
    }

    public int IncreaseCount(int userId, string redelisteName)
    {
        MeldungHistory? meldungsHistory = MeldungHistories.Find(t => 
            t.UserId == userId && t.RedelisteName == redelisteName);

        if (meldungsHistory == null)
            return 0;
        
        meldungsHistory.MeldungCount++;

        return meldungsHistory.MeldungCount;
    }
}