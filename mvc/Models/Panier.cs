namespace mvc.Models
{
    public class Panier
    {
        public  int Id { get; set; }
        public double prix_total     { get; set; }

        public int nombre_article   { get; set; }

        public string? descritption { get; set; }

        public int clientID { get; set; }
    }
}
