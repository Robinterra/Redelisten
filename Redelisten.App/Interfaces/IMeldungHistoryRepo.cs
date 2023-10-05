using Redelisten.App.Models.Dtos;
using Redelisten.App.Models.Entities;

namespace Redelisten.App.Interfaces;

public interface IMeldungHistoryRepo
{
    MeldungHistory? Create(int userId, string redelisteName);
    MeldungHistory? Retrieve(int userId, string redelisteName);
    int IncreaseCount(int userId, string redelisteName);
}