public class HelpNode : ICommandLine
{

    // -----------------------------------------------

    #region get/set

    // -----------------------------------------------

    public string Key
    {
        get
        {
            return "help";
        }
    }

    // -----------------------------------------------

    public bool HasValue
    {
        get
        {
            return false;
        }
    }

    // -----------------------------------------------

    public string HelpLine
    {
        get
        {
            return string.Format (HelpManager.HilfePattern, this.Key, string.Empty, "Print the help in the console" );
        }
    }

    // -----------------------------------------------

    public string Value
    {
        get;
        set;
    } = string.Empty;

    // -----------------------------------------------

    #endregion get/set

    // -----------------------------------------------

    #region methods

    // -----------------------------------------------

    public ICommandLine? Check ( string command )
    {
        if ($"{this.Key}" == command) return this;
        if ($"--{this.Key}" == command) return this;
        if ("-h" == command) return this;
        if ("/?" == command) return this;

        return null;
    }

    // -----------------------------------------------

    #endregion methods

    // -----------------------------------------------

}