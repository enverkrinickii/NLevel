namespace DAL.Models
{
    public class PurchaseInfoDTO
    {
        public int Id { get; set; }
        public string SaleDate { get; set; }
        public int ManagerId { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }

        public override string ToString()
        {
            return SaleDate;
        }
    }
}
