public class ConnectionInfo
{
    public string User
    {
        get;
    }

    public string Redeliste
    {
        get;
    }

    public string Host
    {
        get;
    }

    public bool IgnoreCertificateErrors
    {
        get;
    }

    public ConnectionInfo ( string user, string redeliste, string host, bool ignoreCertificateErrors )
    {
        this.User = user;
        this.Redeliste = redeliste;
        this.Host = host;
        this.IgnoreCertificateErrors = ignoreCertificateErrors;
    }
}