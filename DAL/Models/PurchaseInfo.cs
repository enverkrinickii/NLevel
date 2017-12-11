namespace DAL.Models
{
    public class PurchaseInfo
    {
        public int Id { get; set; }
        public string PurchaseDate { get; set; }
        public int ManagerId { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }

        public override string ToString()
        {
            return PurchaseDate;
        }
    }
}
