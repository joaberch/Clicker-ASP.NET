namespace FastClieckWebAppMVC.Models
{
    public class Player
    {
        // Player's id
        public int Id { get; set; }

        // Player's username
        public string username { get; set; }

        // Player's number of click
        public int clickNbr { get; set; }

        // Player's number of restart
        public int restartNbr { get; set; }
    }
}
