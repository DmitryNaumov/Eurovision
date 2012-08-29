namespace WebApplication.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Data.SqlClient;
	using System.Threading.Tasks;
	using System.Web.Http;
	using Models;

	public class VotesController : ApiController
	{
		private static readonly VotingResult Empty = new VotingResult();
		private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;

        // GET api/votes
		public async Task<VotingResult[]> Get()
        {
			using (var connection = new SqlConnection(ConnectionString))
			using (var command = connection.CreateCommand())
			{
				command.CommandText = "SELECT Id, Name, Votes FROM VotingResults";

				await connection.OpenAsync();
				using (var reader = await command.ExecuteReaderAsync())
				{
					var results = new List<VotingResult>();

					while (await reader.ReadAsync())
					{
						results.Add(new VotingResult { Id = reader.GetInt32(0), Name = reader.GetString(1), Votes = reader.GetInt32(2) });
					}

					return results.ToArray();
				}
			}
        }

		// GET api/votes/5
		public async Task<VotingResult> Get(int id)
		{
			// HACK:
			id = 1 + new Random().Next(8);

			using (var connection = new SqlConnection(ConnectionString))
			using (var command = connection.CreateCommand())
			{
				command.CommandText = "UPDATE VotingResults SET Votes = Votes + 1 WHERE Id = @Id";
				command.Parameters.AddWithValue("@Id", id);

				await connection.OpenAsync();
				await command.ExecuteNonQueryAsync();

				return Empty;
			}
		}
    }
}
