namespace FastClieckWebAppMVC.Models
{
    public class Player
    {
        // ID du joueur
        public int Id { get; set; }

        // Pseudo du joueur 
        public string username { get; set; }

        // nbr of click
        public int clickNbr { get; set; }

        // nbr of restart
        public int restartNbr { get; set; }
    }
}
