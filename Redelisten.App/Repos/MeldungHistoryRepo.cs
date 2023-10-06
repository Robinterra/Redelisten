using Redelisten.App.Interfaces;
using Redelisten.App.Models.Dtos;
using Redelisten.App.Models.Entities;

namespace Redelisten.App.Repos;

public class MeldungHistoryRepo : IMeldungHistoryRepo
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

    public MeldungHistory? Retrieve(Meldung meldung)
    {
        return Retrieve(meldung.UserID, meldung.RedelistenName);
    }

    public int IncreaseCount(MeldungHistory  meldungsHistory)
    {
        meldungsHistory.MeldungCount++;
        meldungsHistory.LetzterBeitrag = DateTime.Now;

        return meldungsHistory.MeldungCount;
    }
}