public class MeldungReport
{
    public string User
    {
        get;
        set;
    }

    public MeldungReport(string name)
    {
        this.User = name;
    }
}