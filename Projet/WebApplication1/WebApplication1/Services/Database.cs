using Microsoft.Data.SqlClient;

namespace WebApplication1.Services
{
	public class Database : IDatabase
	{
		private readonly IConfiguration _configuration;

		public Database(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void InitializeDatabase()
		{
			string connectionString = _configuration.GetConnectionString("DefaultConnection");
			// Utilisez la chaîne de connexion pour initialiser la base de données
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				// Votre code pour initialiser la base de données
				connection.Open();
			}
		}

		// Implémentez d'autres méthodes selon vos besoins (par exemple, CRUD operations)
	}
}
