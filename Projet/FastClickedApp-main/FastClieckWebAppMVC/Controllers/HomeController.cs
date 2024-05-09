﻿using FastClieckWebAppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Docker.DotNet;
using System.Web;


namespace FastClieckWebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        public MySqlConnection connection;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(SelectPlayer());
        }

		/// <summary>
        /// Méthode pour vérifier si le pseudonyme existe
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns></returns>
		[HttpPost]
		public bool CheckPlayerExists(string username)
		{
            // Logique pour vérifier si le pseudonyme existe dans la base de données
            bool playerExists = false;/* Appelle ta méthode ou requête pour vérifier le pseudonyme dans la base de données */

            // requête sql pour insérer des valeurs dans la table `t_joueur`.
            string selectquery = $"SELECT * FROM t_joueur WHERE username = '{username}'";

            // crée la commande avec la requête d'insertion.
            MySqlCommand insertcommand = new MySqlCommand(selectquery, connection);

            // exécute la requête
            MySqlDataReader insertdatareader = insertcommand.ExecuteReader();

			if (insertdatareader.HasRows)
			{
				playerExists = true; // Le joueur existe dans la base de données
			}

			insertdatareader.Close();

			return playerExists;
		}

		/// <summary>
		/// Insérer le joueur dans la base donnée s'il n'existe pas déjà
		/// </summary>
		/// <param name="playerName"></param>
		/// <param name="playerNbrClick"></param>
		/// <param name="playerNbrRestart"></param>
		[HttpPost]
        public void InsertPlayerScore(string playerName, int playerNbrClick, int playerNbrRestart)
        {
            // Valeurs de connection de la base de donnée
            string srv_addr = "192.168.0.10";                          // Adresse du  serveur 
            string dbname = "db_clickGame";                          // nom de la base de donnée
            string uid = "root";                                     // Utilisateur
            string pass = "root";                                    // Mot de passe
            string port = "3306";                                    // Port 

            // Chaine de connexion permettant de de se connecter à la base de donnée
            string connectStr = "SERVER=" + srv_addr + ";" + "PORT=" + port + ";" + "DATABASE=" + dbname + ";" + "UID=" + uid + ";" + "PASSWORD=" + pass + ";";

            // attribue la chaine de connexion 
            connection = new MySqlConnection(connectStr);

            // Ouvre la connexion à la base de donnée
            connection.Open();

            
            if (CheckPlayerExists(playerName))
            {
				
			} else
            {
				// Requête SQL pour insérer des valeurs dans la table `t_joueur`.
				string insertQuery = $"insert into t_joueur (username, nbrRestart) values ('{playerName}', 1);";

				// Crée la commande avec la requête d'insertion.
				MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);

				// Exécute la requête
				MySqlDataReader insertDataReader = insertCommand.ExecuteReader();

				insertDataReader.Read();

				// Ferme la connexion à la base de données.
				connection.Close();
			}
			//Check if the username already exist in the database
			//if it already exist
			//get the data in the database

			//if it doesn't exist
			//create the user with this value, username = what he entered | nbrClick = 1 | nbrRestart = 1
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
            string connectStr = "SERVER=" + srv_addr + ";DATABASE=" + dbname + ";UID=" + uid + ";PASSWORD=" + pass + ";PORT=" + port;

            // attribue la chaine de connexion 
            connection = new MySqlConnection(connectStr);

            // Ouvre la connexion à la base de données.
            connection.Open();

            // Crée une liste pour stocker les joueurs.
            List<Player> playerList = new List<Player>();

            // Requête SQL pour sélectionner les 10 meilleurs joueurs par score, en ordre décroissant.
            string selectQuery = "select * from t_joueur order by nbrRestart desc limit 10;";

            MySqlCommand cmd = new MySqlCommand(selectQuery, connection);

            // Exécute la requête 
            MySqlDataReader reader = cmd.ExecuteReader();

            // Parcourt chaque ligne des résultats obtenu.
            while (reader.Read())
            {
                // Crée un nouveau joueur 
                Player player = new Player();

                // Récupère les valeurs des colonnes et les assigne au joueur.
                player.Id = Convert.ToInt32(reader["id"]);
                //player.Place = place;
                //player.NickName = reader["name"].ToString();
                //player.Score = Convert.ToInt32(reader["score"]);

                // Ajoute le joueur à la liste des joueurs.
                playerList.Add(player);

                // Incrémente la place pour le prochain joueur.
                place++;
            }

            // Ferme la connexion à la base de données.
            connection.Close();

            // Renvoie la liste des joueurs
            return playerList;
        }

        static async void getContainerIp()
        {
            var dockerClient = new DockerClientConfiguration(new Uri("http://ubuntu-docker.cloudapp.net:4243")).CreateClient();

            var containers = await dockerClient.Containers.InspectContainerAsync("1903e9c65613846836d4977d3bdd476f0d69c7bb8e657f1f3f96f6582f79e10f");


            var inspectResult = await dockerClient.Containers.InspectContainerAsync(containers.ID);
            var ipAddress = inspectResult.NetworkSettings.Networks["bridge"].IPAddress;
            Console.WriteLine($"MySQL Container IP Address: {ipAddress}");
            
            //_ipAddress = ipAddress;

        }
    }
}