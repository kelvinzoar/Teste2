using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Questao2.Models;

namespace Questao2.Services
{
    public class FootballApiService : IFootballApiService
    {
        private readonly HttpClient _httpClient;

        public FootballApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetTotalScoredGoals(string team, int year)
        {
            int totalGoals = 0;
            totalGoals += await GetGoalsForTeam(team, year, "team1");
            totalGoals += await GetGoalsForTeam(team, year, "team2");
            return totalGoals;
        }

        private async Task<int> GetGoalsForTeam(string team, int year, string teamKey)
        {
            int totalGoals = 0;
            int currentPage = 1;
            int totalPages = 1;

            while (currentPage <= totalPages)
            {
                var responseData = await FetchMatchDataFromApi(team, year, teamKey, currentPage);

                if (responseData != null && responseData.data != null)
                {
                    totalPages = responseData.total_pages;

                    totalGoals += CalculateGoals(responseData.data, teamKey);
                }

                currentPage++;
            }

            return totalGoals;
        }

        private async Task<ApiResponse?> FetchMatchDataFromApi(string team, int year, string teamKey, int page)
        {
            string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&{teamKey}={Uri.EscapeDataString(team)}&page={page}";
            string responseBody = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<ApiResponse>(responseBody);
        }

        private int CalculateGoals(List<MatchData> matches, string teamKey)
        {
            int totalGoals = 0;

            foreach (var match in matches)
            {
                int goals = 0;

                if (teamKey == "team1" && int.TryParse(match.team1goals, out goals))
                {
                    totalGoals += goals;
                }
                else if (teamKey == "team2" && int.TryParse(match.team2goals, out goals))
                {
                    totalGoals += goals;
                }
            }

            return totalGoals;
        }
    }
}
