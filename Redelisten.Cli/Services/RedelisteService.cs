using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using ApiService;
using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.AspNetCore.SignalR.Client;

//using Microsoft.AspNet.SignalR.Client;
using MyApiService;

public class RedelisteService
{
    public ConnectionInfo ConnectionInfo { get; }
    private MyApiClient ApiClient { get; }

    public RedelisteService(ConnectionInfo ci)
    {
        this.ConnectionInfo = ci;
        this.ApiClient = new MyApiClient(ci.Host);
        if (ci.IgnoreCertificateErrors) this.ApiClient.CheckCertEasy += CheckCertEasy;
    }

    private bool CheckCertEasy(string hostname, X509Certificate2 x509Certificate2, X509Chain x509Chain)
    {
        return true;
    }

    public static void CriticalError(string message)
    {

        Console.Error.WriteLine(message);
        Environment.Exit(1);
    }

    private async Task StartConnection()
    {
        bool isalreadyExist = File.Exists(".cookie");
        if (isalreadyExist)
        {
            Cookie cookie = new Cookie("token", File.ReadAllText(".cookie"), null, this.ApiClient.client.BaseAddress!.Host);
            this.ApiClient.handler.CookieContainer.Add(cookie);

            ApiResponse response = (await this.ApiClient.GetAsync<ApiResponse>("User"))!;
            isalreadyExist = response.HttpCode == 200;
            if (!isalreadyExist) cookie.Expired = true;
        }

        if (!isalreadyExist)
        {
            ApiResponse response = (await this.ApiClient.PostAsync<ApiResponse, dynamic>(new{name=ConnectionInfo.User}, "User/Create"))!;
            if (response.HttpCode == 409) CriticalError("User already exists");

            Cookie? cookie = this.ApiClient.handler.CookieContainer.GetCookies(new Uri(this.ConnectionInfo.Host)).FirstOrDefault(t=>t.Name == "token");
            if (cookie is null) CriticalError("Cookie wurde nicht gefunden");

            File.WriteAllText($".cookie", cookie!.Value);
        }

        HttpResponseMessage message = await ApiClient.client.PostAsJsonAsync("Redeliste/Create", new{name=ConnectionInfo.Redeliste});
        if (message.StatusCode == HttpStatusCode.Created) Console.WriteLine("Du bist Moderator");
        else if (message.StatusCode == HttpStatusCode.OK) Console.WriteLine("Redeliste Erfolgreich beigetreten");
        else CriticalError(message.Content.ReadAsStringAsync().Result);

        await this.WebSocketStart();
    }

    private async Task WebSocketStart()
    {
        HubConnectionBuilder builder = new HubConnectionBuilder();

        builder.WithAutomaticReconnect();
        builder.WithUrl($"{this.ConnectionInfo.Host}/LiveInfos", InitSignalrConnection);

        HubConnection connection = builder.Build();

        await connection.StartAsync();

        connection.On<MeldungReport>($"NeueMeldung_{this.ConnectionInfo.Redeliste}", param => {
            Console.WriteLine($"Der User {param.User} wurde mit auf die Redeliste aufgenommen");
            Console.Write("> ");
        });

        connection.On<MeldungReport>($"CurrentMeldung_{this.ConnectionInfo.Redeliste}", param => {
            Console.WriteLine($"Der User {param.User} ist jetzt dran");
            Console.Write("> ");
        });

        connection.On<string>($"KeineMeldung_{this.ConnectionInfo.Redeliste}", param => {
            Console.WriteLine($"Es gibt keine weiteren Meldungen");
            Console.Write("> ");
        });
    }

    private void InitSignalrConnection(HttpConnectionOptions options)
    {
        if (this.ConnectionInfo.IgnoreCertificateErrors)
        {
            options.HttpMessageHandlerFactory = (x) => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            };
        }

        Cookie? token = this.ApiClient.handler.CookieContainer.GetCookies(new Uri(this.ConnectionInfo.Host)).FirstOrDefault(t=>t.Name == "token");
        if (token is null) return;
        options.Cookies.Add(token);
    }

    public async Task Run()
    {
        await this.StartConnection();
        Console.WriteLine("Mit q verlassen, mit m melden und mit n next person (nur moderator)");

        while (true)
        {
            Console.Write("> ");
            string? cmd = Console.ReadLine();
            if (cmd == "m") await Melden();
            if (cmd == "q") break;
            if (cmd == "n") await Nächster();
        }
    }

    private async Task Nächster()
    {
        HttpResponseMessage message = await ApiClient.client.PostAsync($"Redeliste/{this.ConnectionInfo.Redeliste}/Meldung/Done", null);
        if (message.StatusCode == HttpStatusCode.OK)
        {
            return;
        }
        string msg = await message.Content.ReadAsStringAsync();
        //Console.WriteLine(msg);
    }

    private async Task Melden()
    {
        HttpResponseMessage message = await ApiClient.client.PostAsync($"Redeliste/{this.ConnectionInfo.Redeliste}/Meldung", null);
        if (message.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine("Erfolgreich gemeldet");
            return;
        }
        string msg = await message.Content.ReadAsStringAsync();
        Console.WriteLine(msg);
    }
}