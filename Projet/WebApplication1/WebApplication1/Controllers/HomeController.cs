using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using WebApplication1.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		public MySqlConnection Connection;

		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public void CloseConnection()
		{
			// Close the connection
			Connection.Close();
		}

		[HttpPost]
		public void InsertPlayerScore(string playerName, int playerScore)
		{
			// Connection value for the database
			string srv_addr = "192.168.0.10";                       // Address server 
			string dbname = "db_clicker";							// Database name
			string uid = "root";                                    // User name
			string pass = "root";                                   // Password
			string port = "3306";                                   // Port 

			// The connection string to connect to the database
			string connectStr = "SERVER=" + srv_addr + ";" + "PORT=" + port + ";" + "DATABASE=" + dbname + ";" + "UID=" + uid + ";" + "PASSWORD=" + pass + ";";

            // assigns the connection string 
            Connection = new MySqlConnection(connectStr);

			// Open the connection to the database
			Connection.Open();

			//SQL request to insert data in table 't_joueur'
			//string insertQuery = $"insert into t_joueur (name, score) values ('{playerName}', {playerScore});";

			// Create the command with the insert value
			//MySqlCommand insertCommand = new MySqlCommand(insertQuery, Connection);

			// Execute the request
			//MySqlDataReader insertDataReader = insertCommand.ExecuteReader();

			//insertDataReader.Read();

			// Close the connection to the database
			Connection.Close();
		}

		//static async void getContainerIp()
		//{
		//	var dockerClient = new DockerClientConfiguration(new Uri("http://ubuntu-docker.cloudapp.net:4243")).CreateClient();

		//	var containers = await dockerClient.Containers.InspectContainerAsync("1903e9c65613846836d4977d3bdd476f0d69c7bb8e657f1f3f96f6582f79e10f");

		//	var inspectResult = await dockerClient.Containers.InspectContainerAsync(containers.ID);
		//	var ipAddress = inspectResult.NetworkSettings.Networks["bridge"].IPAddress;
		//	Console.WriteLine($"MySQL Container IP Address: {ipAddress}");

		//	//_ipAddress = ipAddress;

		//}
	}
}
