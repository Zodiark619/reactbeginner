namespace InventoryManagamentAPI.Models.Stripe
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // This defines what logic the product uses
        public string YieldContent { get; set; }  // "Generate12CardsPack"

    }
}
