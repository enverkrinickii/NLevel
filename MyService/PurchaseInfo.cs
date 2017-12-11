namespace MyService
{
    class PurchaseInfo
    {
        public PurchaseInfo(string managerName, string saleDate, string clientName, string productName, double productCost)
        {
            ManagerName = managerName;
            SaleDate = saleDate;
            ClientName = clientName;
            ProductName = productName;
            ProductCost = productCost;
        }

        public string ManagerName { get; private set; }
        public string SaleDate { get; private set; }
        public string ClientName { get; private set; }
        public string ProductName { get; private set; }
        public double ProductCost { get; private set; }
    }
}
