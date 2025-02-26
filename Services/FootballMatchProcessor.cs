using System;
using System.Threading.Tasks;
using Questao2.Services;

namespace Questao2.Services
{
    public class FootballMatchProcessor
    {
        private readonly IFootballApiService _footballApiService;

        public FootballMatchProcessor(IFootballApiService footballApiService)
        {
            _footballApiService = footballApiService;
        }

        public async Task Process()
        {
            var teamsAndYears = new (string team, int year)[]
            {
                ("Paris Saint-Germain", 2013),
                ("Chelsea", 2014)
            };

            foreach (var (team, year) in teamsAndYears)
            {
                int totalGoals = await _footballApiService.GetTotalScoredGoals(team, year);
                Console.WriteLine($"Team {team} scored {totalGoals} goals in {year}");
            }
        }
    }
}
