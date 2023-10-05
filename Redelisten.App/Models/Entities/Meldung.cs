using Microsoft.VisualBasic.CompilerServices;

public class Meldung
{
    public int UserID { get; set; }
    public string RedelistenName { get; set; }

    public Meldung(CreateMeldungDto createMeldungDto)
    {
        UserID = createMeldungDto.Moderator.Id;
        RedelistenName = createMeldungDto.RedelistenName;
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