using System;
using System.Net.Http;
using System.Threading.Tasks;
using Questao2.Services;

public class Program
{
    public static async Task Main()
    {
        var httpClient = new HttpClient();
        IFootballApiService apiService = new FootballApiService(httpClient);
        var matchProcessor = new FootballMatchProcessor(apiService);

        await matchProcessor.Process();
    }
}