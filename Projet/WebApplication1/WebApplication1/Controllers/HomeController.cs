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
			// Valeurs de connection de la base de donn�e
			string srv_addr = "192.168.0.10";                          // Adresse du  serveur 
			string dbname = "db_clicker";                          // nom de la base de donn�e
			string uid = "root";                                     // Utilisateur
			string pass = "root";                                    // Mot de passe
			string port = "3306";                                    // Port 

			// Chaine de connexion permettant de de se connecter � la base de donn�e
			string connectStr = "SERVER=" + srv_addr + ";" + "PORT=" + port + ";" + "DATABASE=" + dbname + ";" + "UID=" + uid + ";" + "PASSWORD=" + pass + ";";

			// attribue la chaine de connexion 
			Connection = new MySqlConnection(connectStr);

			// Ouvre la connexion � la base de donn�e
			Connection.Open();

			// Requ�te SQL pour ins�rer des valeurs dans la table `t_joueur`.
			string insertQuery = $"insert into t_joueur (name, score) values ('{playerName}', {playerScore});";

			// Cr�e la commande avec la requ�te d'insertion.
			MySqlCommand insertCommand = new MySqlCommand(insertQuery, Connection);

			// Ex�cute la requ�te
			MySqlDataReader insertDataReader = insertCommand.ExecuteReader();

			insertDataReader.Read();

			// Ferme la connexion � la base de donn�es.
			Connection.Close();
		}

		/// <summary>
		/// Fonction qui va chercher les premier joueur de la base donn�e
		/// </summary>
		/// <returns>La liste des joueurs</returns>
		public List<Player> SelectPlayer()
		{
			//getContainerIp();

			int place = 1;

			// Valeurs de connection de la base de donn�e
			string srv_addr = "192.168.0.10";                          // Adresse du  serveur 
			string dbname = "db_clickGame";                          // nom de la base de donn�e
			string uid = "root";                                     // Utilisateur
			string pass = "root";                                    // Mot de passe
			string port = "3306";                                    // Port 

			// Chaine de connexion permettant de de se connecter � la base de donn�e
			string connectStr = "SERVER=" + srv_addr + ";" + "DATABASE=" + dbname + ";" + "UID=" + uid + ";" + "PASSWORD=" + pass + ";" + "PORT=" + port;

			// attribue la chaine de connexion 
			Connection = new MySqlConnection(connectStr);

			// Ouvre la connexion � la base de donn�es.
			Connection.Open();

			// Cr�e une liste pour stocker les joueurs.
			List<Player> playerList = new List<Player>();

			// Requ�te SQL pour s�lectionner les 10 meilleurs joueurs par score, en ordre d�croissant.
			string selectQuery = "select * from t_joueur order by score desc limit 10;";

			MySqlCommand cmd = new MySqlCommand(selectQuery, Connection);

			// Ex�cute la requ�te 
			MySqlDataReader reader = cmd.ExecuteReader();

			// Parcourt chaque ligne des r�sultats obtenu.
			while (reader.Read())
			{
				// Cr�e un nouveau joueur 
				Player player = new Player();

				// R�cup�re les valeurs des colonnes et les assigne au joueur.
				player.Id = Convert.ToInt32(reader["id"]);
				player.nbrRestart = 1;
				player.username = reader["name"].ToString();

				// Ajoute le joueur � la liste des joueurs.
				playerList.Add(player);

				// Incr�mente la place pour le prochain joueur.
				place++;
			}

			// Ferme la connexion � la base de donn�es.
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
