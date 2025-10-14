namespace VisiblePT.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DiscountPercent { get; set; }
        public string ImageURL { get; set; }
        public DateTime DateAdd { get; set; }

        public decimal DiscountPrice { get; private set; }
    }
}
