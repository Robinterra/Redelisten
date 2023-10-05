using Redelisten.App.Models.Dtos;

namespace Redelisten.App.Models.Entities;

public class MeldungHistory
{
    public string RedelisteName { get; set; }
    public int UserId { get; set; }
    public int MeldungCount { get; set; }

    public MeldungHistory(int userId, string redelisteName)
    {
        this.RedelisteName = redelisteName;
        this.UserId = userId;
        this.MeldungCount = 0;
    }
}