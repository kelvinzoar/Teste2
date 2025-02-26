using System.Collections.Generic;

namespace Questao2.Models
{
	public class ApiResponse
	{
		public int total { get; set; }
		public int total_pages { get; set; }
		public List<MatchData> data { get; set; } = new List<MatchData>();
	}
}
