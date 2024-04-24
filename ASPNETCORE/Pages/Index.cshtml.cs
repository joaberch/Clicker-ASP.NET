using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data.SqlClient;

namespace ASPNETCORE.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
		{
			string myConnectionString = "server=127.0.0.1;uid=root;pwd=root;database=db_clicker";

			try
			{
				var myConnection = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
				myConnection.Open();

				// Exemple d'insertion d'un nouvel utilisateur
				string query = $"INSERT INTO t_users (username, nbrRestart) VALUES ('john_doe', 0)";

				MySqlCommand cmd = new MySqlCommand(query, myConnection);
			} catch (Exception) { }
		}
	}
}
