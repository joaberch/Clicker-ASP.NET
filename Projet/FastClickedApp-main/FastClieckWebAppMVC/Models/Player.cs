/******************************************************************************
#** PROGRAMME  Player.cs                                             **
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

namespace FastClieckWebAppMVC.Models
{
    public class Player
    {
        // Player's id
        public int Id { get; set; }

        // Player's pseudo
        public string username { get; set; }

        // nbr of click
        public int clickNbr { get; set; }

        // nbr of restart
        public int restartNbr { get; set; }
    }
}
