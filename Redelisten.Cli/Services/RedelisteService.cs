using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using ApiService;
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
        ApiResponse response = (await this.ApiClient.PostAsync<ApiResponse, dynamic>(new{name=ConnectionInfo.User}, "User/Create"))!;
        if (response.HttpCode == 409) CriticalError("User already exists");

        Console.WriteLine(response.Status);
        Console.WriteLine(response.HttpCode);

        HttpResponseMessage message = await ApiClient.client.PostAsJsonAsync("Redeliste/Create", new{name=ConnectionInfo.Redeliste});
        //response = (await this.ApiClient.PostAsync<ApiResponse, dynamic>(new{name=ConnectionInfo.Redeliste}, "Redeliste/Create"))!;
        Console.WriteLine(message.StatusCode);
        //Console.WriteLine(response.HttpCode);
        Console.WriteLine(await message.Content.ReadAsStringAsync());
        
    }

    public async Task Run()
    {
        await this.StartConnection();

        while (true)
        {

        }
    }

}