namespace WebApplication.Controllers
{
	using System;
	using System.Configuration;
	using System.Data.SqlClient;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Web;
	using System.Web.Http;
	using Models;

	public class VotesController : ApiController
	{
		private static readonly VotingResult Empty = new VotingResult();
		private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;

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
		public VotingResult[] Get()
		{
			return _votes;
		}

		// GET api/votes/5
		public async Task<VotingResult> Get(int id)
		{
			// HACK:
			id = 1 + new Random().Next(8);

			using (var connection = new SqlConnection(ConnectionString))
			using (var command = connection.CreateCommand())
			{
				command.CommandText = "INSERT INTO Votes (Id) VALUES (@Id)";
				command.Parameters.AddWithValue("@Id", id);

				await connection.OpenAsync();

				using (var transaction = connection.BeginTransaction())
				{
					command.Transaction = transaction;

					await command.ExecuteNonQueryAsync();
					
					// simulate db activity & roundtrip
					await Task.Delay(10);

					transaction.Commit();
				}

				AddVote(id - 1);

				return Empty;
			}
		}

		private void AddVote(int id)
		{
			var votingResult = _votes[id];
			lock (votingResult)
			{
				votingResult.Votes++;
			}
		}
	}
}
