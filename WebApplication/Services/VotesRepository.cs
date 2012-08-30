namespace WebApplication.Services
{
	using System.Configuration;
	using System.Data.SqlClient;
	using System.Threading.Tasks;
	using WebApplication.Models;

	internal sealed class VotesRepository
	{
		private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;

		private readonly VotingResult[] _votes =
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

		public VotesRepository()
		{
			// HACK:	

			using (var connection = new SqlConnection(ConnectionString))
			using (var command = connection.CreateCommand())
			{
				command.CommandText = "SELECT Id FROM Votes";

				connection.Open();
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var id = reader.GetInt32(0);
						IncrementVote(id);
					}
				}
			}
		}

		public VotingResult[] GetResults()
		{
			return _votes;
		}

		public async Task<int> AddVote(int id)
		{
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

				IncrementVote(id);

				return 42;
			}
		}

		private void IncrementVote(int id)
		{
			var votingResult = _votes[id - 1];
			lock (votingResult)
			{
				votingResult.Votes++;
			}
		}
	}
}