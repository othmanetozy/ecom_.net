namespace mvc.Models
{
    public class lignepanier
    {
        public int ID { get; set; }

        public int panierID { get; set; }

        public Panier? panier { get; set; }

        public int quantite { get; set; }

        public int prix { get; set; }
    }
}
