
public class RedelisteNode: ICommandLine, ICommandParent
{

    // -----------------------------------------------

    #region get/set

    // -----------------------------------------------

    public string Key
    {
        get
        {
            return "redeliste";
        }
    }

    // -----------------------------------------------

    public bool HasValue
    {
        get
        {
            return true;
        }
    }

    // -----------------------------------------------

    public string HelpLine
    {
        get
        {
            return string.Format (HelpManager.HilfePattern, this.Key, "<name>", "Der Name zur Redeliste" );
        }
    }

    // -----------------------------------------------

    public string Value
    {
        get;
        set;
    } = string.Empty;
    public List<ICommandLine> Childs { get; set; } = new List<ICommandLine>();

    // -----------------------------------------------

    #endregion get/set

    // -----------------------------------------------

    #region methods

    // -----------------------------------------------

    public ICommandLine? Check ( string command )
    {
        if ($"{this.Key}" == command) return this;
        if ($"--{this.Key}" == command) return this;
        if ($"-l" == command) return this;
        if ($"-r" == command) return this;

        return null;
    }

    public bool Execute(RequestExecuteArgs request)
    {
        return true;
    }

    // -----------------------------------------------

    #endregion methods

    // -----------------------------------------------
}