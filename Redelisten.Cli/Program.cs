public static class Program
{
    public static List<ICommandLine> EnabledCommandLines
    {
        get;
    } = new List<ICommandLine> (  );

    public static int Main(string[] args)
    {
        Program.Init();

        return NormalStart(args);
    }

    private static int NormalStart ( string[] args )
    {
        if ( !ParseCommandLine.CheckArgs ( args ) ) return HelpPrinten (  );

        ParseCommandLine pcl = new ParseCommandLine { CommandLines = EnabledCommandLines, Default = new HelpNode() /*new FileExpressionNode()*/ };

        foreach ( string arg in args )
        {
            pcl.ArgumentAuswerten ( arg );
        }

        if (!Program.ExecuteArguments ( pcl.Result )) return 1;

        return 0;
    }

    private static bool ExecuteArguments ( List<ICommandLine> commands )
    {
        if (commands.Count == 0) return Program.HelpPrinten() == 1;

        string? host = null;
        string? user = null;
        string? redeliste = null;
        //@todo der nutzer soll es einstellen können
        bool ignoreCertificateErrors = true;

        foreach (ICommandLine command in commands)
        {
            if (command is HelpNode) return Program.HelpPrinten() == 1;
            if (command is HostNode h) host = h.Value;
            if (command is UserNode u) user = u.Value;
            if (command is RedelisteNode r) redeliste = r.Value;
        }

        if (string.IsNullOrEmpty(host))
        {
            Console.Error.WriteLine ( "please enter a host" );
            return false;
        }
        if (string.IsNullOrEmpty(user))
        {
            Console.Error.WriteLine ( "please enter a user" );
            return false;
        }
        if (string.IsNullOrEmpty(redeliste))
        {
            Console.Error.WriteLine ( "please enter a redeliste" );
            return false;
        }

        ConnectionInfo connection = new ConnectionInfo ( user, redeliste, host, ignoreCertificateErrors );
        RedelisteService redelisteService = new RedelisteService ( connection );
        redelisteService.Run (  ).Wait (  );

        return true;
    }

    /*private static Task<bool> Create(List<ICommandLine> commands)
    {
        RequestCreate request = new RequestCreate();

        foreach (var command in commands)
        {
            if (command is NameNode) request.Name = command.Value;
            if (command is FileExpressionNode) request.Image = new FileInfo(command.Value);
            if (command is DescriptionFileNode) request.Description = new FileInfo(command.Value);
            if (command is TagNode) request.Tags.Add(command.Value);
            if (command is TypeNode t) request.SerieType = t.SerieType;
        }

        UploaderRepo uploader = new UploaderRepo();

        return uploader.Execute(request);
    }*/

    // -----------------------------------------------


    private static int HelpPrinten (  )
    {
        HelpManager hilfe = new HelpManager { CommandLines = Program.EnabledCommandLines };

        hilfe.Print (  );

        return 1;
    }

    // -----------------------------------------------

    public static bool Init()
    {
        Program.InitCommandLines (  );
        
        return true;
    }

    private static bool InitCommandLines()
    {
        /*List<ICommandLine> contentArgs = new List<ICommandLine>();
        contentArgs.Add( new NameNode());
        contentArgs.Add( new FileExpressionNode () );
        contentArgs.Add( new MiniNode() );
        contentArgs.Add( new AllNode() );
        contentArgs.Add( new NamePatternNode() );
        contentArgs.Add( new NameMiniPatternNode() );
        contentArgs.Add( new ZerosNode() );
        contentArgs.Add( new IterateNode() );
        contentArgs.Add( new IdNode() );
        contentArgs.Add( new StartNode() );

        List<ICommandLine> createArgs = new List<ICommandLine>();
        createArgs.Add(new NameNode());
        createArgs.Add( new FileExpressionNode () );
        createArgs.Add(new DescriptionFileNode());
        createArgs.Add(new TagNode());
        createArgs.Add(new TypeNode());

        List<ICommandLine> uploadArgs = new List<ICommandLine>();
        uploadArgs.Add(new FileExpressionNode());
        uploadArgs.Add(new IdNode());*/

        //Program.EnabledCommandLines.Add ( new ContentNode ( contentArgs ) );
        //Program.EnabledCommandLines.Add ( new UploadNode ( uploadArgs ) );
        //Program.EnabledCommandLines.Add ( new CreateNode ( createArgs ) );
        Program.EnabledCommandLines.Add ( new HelpNode() );
        Program.EnabledCommandLines.Add ( new HostNode() );
        Program.EnabledCommandLines.Add ( new RedelisteNode() );
        Program.EnabledCommandLines.Add ( new UserNode() );
        /*Program.EnabledCommandLines.Add ( new TypeNode() );
        Program.EnabledCommandLines.AddRange(contentArgs);
        Program.EnabledCommandLines.AddRange(createArgs);
        Program.EnabledCommandLines.AddRange(uploadArgs);
        Program.EnabledCommandLines.Add ( new FileExpressionNode() );*/

        return true;
    }

}