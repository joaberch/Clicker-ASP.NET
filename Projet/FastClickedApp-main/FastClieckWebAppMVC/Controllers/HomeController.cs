﻿/******************************************************************************
#** PROGRAMME  HomeController.cs                                             **
#**                                                                           **
#** Lieu      : ETML - section informatique                                   **
#** Auteur    : Joachim Berchel                                               **
#** Date      : 29.05.2024                                                    **
#**                                                                           **
#** Modifications                                                             **
#**   Auteur  :                                                               **
#**   Version :                                                               **
#**   Date    :                                                               **
#**   Raisons :                                                               **
#**                                                                           **
#**                                                                           **
#******************************************************************************/

/******************************************************************************
#** DESCRIPTION                                                               **
#** Application ASP de jeu de clicker                                         **     
#**                                                                           **
#**                                                                           **
#******************************************************************************/

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
        /// Method to check if the username exist
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns></returns>
        [HttpPost]
        public bool CheckPlayerExists(string username)
        {
            // check if the user is in the database
            bool playerExists = false;

            // sql request to insert value in t_joueur
            string selectquery = $"SELECT * FROM t_joueur WHERE username = '{username}'";

            //create the command with the insert request
            MySqlCommand insertcommand = new MySqlCommand(selectquery, connection);

            // Execute the request
            MySqlDataReader insertdatareader = insertcommand.ExecuteReader();

            //check if the user is in the database
            if (insertdatareader.HasRows)
            {
                playerExists = true;
            }

            insertdatareader.Close();

            return playerExists;
        }

        /// <summary>
        /// insert the user in the database if he doesn't exist
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="playerNbrClick"></param>
        /// <param name="playerNbrRestart"></param>
        [HttpPost]
        public void InsertPlayerScore(string playerName, int playerNbrClick, int playerNbrRestart)
        {
            // Connection value in the database
            string srv_addr = "192.168.0.10";                        // Server address 
            string dbname = "db_clickGame";                          // Database's name
            string uid = "root";                                     // User's name
            string pass = "root";                                    // Password's name
            string port = "3306";                                    // Port 

            // Connection string
            string connectStr = "SERVER=" + srv_addr + ";" + "PORT=" + port + ";" + "DATABASE=" + dbname + ";" + "UID=" + uid + ";" + "PASSWORD=" + pass + ";";

            // Assigns the connection string 
            connection = new MySqlConnection(connectStr);

            // Open the connection to the database
            connection.Open();

            //If the user exist in the database
            if (CheckPlayerExists(playerName))
            {

            }
            else //If the user doesn't exist in the database
            {
                //SQL request to insert value in table 't_joueur'
                string insertQuery = $"insert into t_joueur (username, nbrRestart) values ('{playerName}', 1);";

                //Create the command with the insertion request
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);

                //Execute the request
                MySqlDataReader insertDataReader = insertCommand.ExecuteReader();

                insertDataReader.Read();

                //Close the connection to the database
                connection.Close();
            }
            //Check if the username already exist in the database
            //if it already exist
            //get the data in the database

            //if it doesn't exist
            //create the user with this value, username = what he entered | nbrClick = 1 | nbrRestart = 1
        }
    }
}