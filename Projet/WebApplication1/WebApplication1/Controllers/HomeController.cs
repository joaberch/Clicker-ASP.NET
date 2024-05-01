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
			// Ferme la connexion
			Connection.Close();
		}

		[HttpPost]
		public void InsertPlayerScore(string playerName, int playerScore)
		{
			// Valeurs de connection de la base de donnée
			string srv_addr = "192.168.0.10";                          // Adresse du  serveur 
			string dbname = "db_clicker";                          // nom de la base de donnée
			string uid = "root";                                     // Utilisateur
			string pass = "root";                                    // Mot de passe
			string port = "3306";                                    // Port 

			// Chaine de connexion permettant de de se connecter à la base de donnée
			string connectStr = "SERVER=" + srv_addr + ";" + "PORT=" + port + ";" + "DATABASE=" + dbname + ";" + "UID=" + uid + ";" + "PASSWORD=" + pass + ";";

			// attribue la chaine de connexion 
			Connection = new MySqlConnection(connectStr);

			// Ouvre la connexion à la base de donnée
			Connection.Open();

			// Requête SQL pour insérer des valeurs dans la table `t_joueur`.
			string insertQuery = $"insert into t_joueur (name, score) values ('{playerName}', {playerScore});";

			// Crée la commande avec la requête d'insertion.
			MySqlCommand insertCommand = new MySqlCommand(insertQuery, Connection);

			// Exécute la requête
			MySqlDataReader insertDataReader = insertCommand.ExecuteReader();

			insertDataReader.Read();

			// Ferme la connexion à la base de données.
			Connection.Close();
		}

		/// <summary>
		/// Fonction qui va chercher les premier joueur de la base donnée
		/// </summary>
		/// <returns>La liste des joueurs</returns>
		public List<Player> SelectPlayer()
		{
			//getContainerIp();

			int place = 1;

			// Valeurs de connection de la base de donnée
			string srv_addr = "192.168.0.10";                          // Adresse du  serveur 
			string dbname = "db_clickGame";                          // nom de la base de donnée
			string uid = "root";                                     // Utilisateur
			string pass = "root";                                    // Mot de passe
			string port = "3306";                                    // Port 

			// Chaine de connexion permettant de de se connecter à la base de donnée
			string connectStr = "SERVER=" + srv_addr + ";" + "DATABASE=" + dbname + ";" + "UID=" + uid + ";" + "PASSWORD=" + pass + ";" + "PORT=" + port;

			// attribue la chaine de connexion 
			Connection = new MySqlConnection(connectStr);

			// Ouvre la connexion à la base de données.
			Connection.Open();

			// Crée une liste pour stocker les joueurs.
			List<Player> playerList = new List<Player>();

			// Requête SQL pour sélectionner les 10 meilleurs joueurs par score, en ordre décroissant.
			string selectQuery = "select * from t_joueur order by score desc limit 10;";

			MySqlCommand cmd = new MySqlCommand(selectQuery, Connection);

			// Exécute la requête 
			MySqlDataReader reader = cmd.ExecuteReader();

			// Parcourt chaque ligne des résultats obtenu.
			while (reader.Read())
			{
				// Crée un nouveau joueur 
				Player player = new Player();

				// Récupère les valeurs des colonnes et les assigne au joueur.
				player.Id = Convert.ToInt32(reader["id"]);
				player.nbrRestart = 1;
				player.username = reader["name"].ToString();

				// Ajoute le joueur à la liste des joueurs.
				playerList.Add(player);

				// Incrémente la place pour le prochain joueur.
				place++;
			}

			// Ferme la connexion à la base de données.
			Connection.Close();

			// Renvoie la liste des joueurs
			return playerList;
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
