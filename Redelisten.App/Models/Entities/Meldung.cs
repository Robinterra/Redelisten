using Microsoft.VisualBasic.CompilerServices;

public class Meldung
{
    public int UserID { get; set; }
    public string RedelistenName { get; set; }

    public int Order { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Meldung(CreateMeldungDto createMeldungDto, int newOrder)
    {
        UserID = createMeldungDto.Moderator.Id;
        RedelistenName = createMeldungDto.RedelistenName;
        Order = newOrder;
    }

    public static bool operator ==(Meldung lhs, Meldung rhs)
    {
        return lhs.UserID == rhs.UserID && lhs.RedelistenName == rhs.RedelistenName;
    }

    public static bool operator !=(Meldung lhs, Meldung rhs)
    {
        return !(lhs == rhs);
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}