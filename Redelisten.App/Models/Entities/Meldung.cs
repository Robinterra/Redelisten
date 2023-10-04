using Microsoft.VisualBasic.CompilerServices;

public class Meldung
{
    public Guid UserID { get; set; }
    public string RedelistenName { get; set; }

    public Meldung(CreateMeldungDto createMeldungDto)
    {
        UserID = createMeldungDto.UserID;
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
}