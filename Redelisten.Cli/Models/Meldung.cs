public class Meldung
{
    public int UserID { get; set; }
    public string RedelistenName { get; set; } = "";

    public int Order { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Meldung()
    {

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
        if (obj is Meldung m) return this == m;

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}