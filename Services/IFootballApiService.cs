using System.Threading.Tasks;

namespace Questao2.Services
{
	public interface IFootballApiService
	{
		Task<int> GetTotalScoredGoals(string team, int year);
	}
}