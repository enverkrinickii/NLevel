namespace DAL.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double ProductCost { get; set; }

        public override string ToString()
        {
            return ProductName;
        }
    }
}
