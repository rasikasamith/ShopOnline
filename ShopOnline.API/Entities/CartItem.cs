namespace ShopOnline.API.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int Productid { get; set; }
        public int Qty {  get; set; }
    }
}
