namespace WebApplication.Controllers
{
	using System;
	using System.Threading.Tasks;
	using System.Web.Http;
	using Models;
	using WebApplication.Services;

	public class VotesController : ApiController
	{
		private static readonly VotingResult Empty = new VotingResult();

		private static readonly Lazy<VotesRepository> _repository = new Lazy<VotesRepository>(() => new VotesRepository());

		// GET api/votes
		public VotingResult[] Get()
		{
			return _repository.Value.GetResults();
		}

		// GET api/votes/5
		public async Task<VotingResult> Get(int id)
		{
			// HACK:
			id = 1 + new Random().Next(8);

			await _repository.Value.AddVote(id);

			return Empty;
		}
	}
}
