namespace OnlineShoopingApp.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BuyPrice { get; set; }
        public int SellPrice { get; set; }
        public decimal Quantity { get; set; }
        public string ImageLink { get; set; }
        public string CategoryName { get; set; }
        public string SuplierName { get; set; }
    }
}