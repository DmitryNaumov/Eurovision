namespace WebApplication.Controllers
{
	using System.Threading.Tasks;
	using System.Web.Http;
	using Models;

	public class VotesController : ApiController
	{
		private static readonly VotingResult[] _votes =
			{
				new VotingResult {Id = 1, Name = "Great Britain", Votes = 0},
				new VotingResult {Id = 1, Name = "Germany", Votes = 0},
				new VotingResult {Id = 1, Name = "Italy", Votes = 0},
				new VotingResult {Id = 1, Name = "Canada", Votes = 0},
				new VotingResult {Id = 1, Name = "Russia", Votes = 0},
				new VotingResult {Id = 1, Name = "USA", Votes = 0},
				new VotingResult {Id = 1, Name = "France", Votes = 0},
				new VotingResult {Id = 1, Name = "Japan", Votes = 0},
			};

        // GET api/votes
        public async Task<VotingResult[]> Get()
        {
	        await Task.Delay(2000);

	        foreach (var votingResult in _votes)
	        {
		        votingResult.Votes += 1;
	        }

	        return _votes;
        }
    }
}
