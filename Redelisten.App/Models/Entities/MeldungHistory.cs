using Redelisten.App.Models.Dtos;

namespace Redelisten.App.Models.Entities;

public class MeldungHistory
{
    public string RedelisteName { get; set; }
    public int UserId { get; set; }
    public int MeldungCount { get; set; }
    public DateTime LetzterBeitrag { get; set; }

    public MeldungHistory(int userId, string redelisteName)
    {
        RedelisteName = redelisteName;
        UserId = userId;
        MeldungCount = 0;
        LetzterBeitrag = DateTime.MinValue;
    }
}