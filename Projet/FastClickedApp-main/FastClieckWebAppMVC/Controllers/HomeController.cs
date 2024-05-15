using FastClieckWebAppMVC.Models;
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
            return View();
        }

        /// <summary>
        /// Check if the username exist in the database
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns></returns>
        [HttpPost]
        public bool CheckPlayerExists(string username)
        {
            //First we consider the username doesn't exist
            bool playerExists = false;

            //SQL request to insert the username in the database in the table 't_joueur'
            string selectquery = $"SELECT * FROM t_joueur WHERE username = '{username}'";

            //Create the command with the insert request
            MySqlCommand insertcommand = new MySqlCommand(selectquery, connection);

            //Execute the request
            MySqlDataReader insertdatareader = insertcommand.ExecuteReader();

            //If there's already a username in the database
            if (insertdatareader.HasRows)
            {
                playerExists = true; //The player's username already exist in the database
            }

            insertdatareader.Close();

            return playerExists;
        }

        /// <summary>
        /// Insert the player in the database
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="playerNbrClick"></param>
        /// <param name="playerNbrRestart"></param>
        [HttpPost]
        public void InsertPlayerScore(string playerName, int playerNbrClick, int playerNbrRestart)
        {
            //Connection value
            string srv_addr = "192.168.0.10";                        // Server's address
            string dbname = "db_clickGame";                          // Database's name
            string uid = "root";                                     // Username
            string pass = "root";                                    // Password
            string port = "3306";                                    // Port 

            //Connection string 
            string connectStr = "SERVER=" + srv_addr + ";" + "PORT=" + port + ";" + "DATABASE=" + dbname + ";" + "UID=" + uid + ";" + "PASSWORD=" + pass + ";";

            // Assign the connection string
            connection = new MySqlConnection(connectStr);

            // Open the connection
            connection.Open();

            //if the player exist in the database
            if (CheckPlayerExists(playerName))
            {
                //TODO - get his value
            }
            else //if the username doesn't already exist in the database
            {
                //SQL request to insert the player's value
                string insertQuery = $"insert into t_joueur (username, nbrRestart) values ('{playerName}', 1);";

                //Create the command with the insert request
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);

                //Execute the request
                MySqlDataReader insertDataReader = insertCommand.ExecuteReader();

                insertDataReader.Read();

                //Close the connection to the database
                connection.Close();
            }
        }
    }
}