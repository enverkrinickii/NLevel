using DAL.Repositories;

namespace MyWindowsService
{
    class PurchaseBook
    {

        private IRepository<DAL.Models.Manager, NLevel.Manager> _managerRepository;
        private IRepository<DAL.Models.Client, NLevel.Client> _clientRepository;
        private IRepository<DAL.Models.Product, NLevel.Product> _productRepository;
        private IRepository<DAL.Models.PurchaseInfo, NLevel.PurchaseInfo> _purchaseInfoRepository;

        public PurchaseBook()
        {
            _managerRepository = new ManagerRepository();
            _clientRepository = new ClientRepository();
            _productRepository = new ProductRepository();
            _purchaseInfoRepository = new PurchaseInfoRepository();
        }

        public void SaveReports(string path)
        {
            var parser = new Parser();
            var reports = parser.GetPurchasesInfoFromFile(path);
            foreach (var report in reports)
            {
                AddInformation(report);
            }
        }

        public void AddInformation(PurchaseInfo purchaseInfo)
        {
            lock (this)
            {
                var newManager = new DAL.Models.Manager
                {
                    Surname = purchaseInfo.ManagerName
                };
                _managerRepository.Add(newManager);
                var manager = _managerRepository.GetEntity(newManager);
                
                var newClient = new DAL.Models.Client
                {
                    Surname = purchaseInfo.ClientName
                };
                _clientRepository.Add(newClient);
                var client = _clientRepository.GetEntity(newClient);

                var newProduct = new DAL.Models.Product
                {
                    ProductName = purchaseInfo.ProductName,
                    ProductCost = purchaseInfo.ProductCost
                };
                _productRepository.Add(newProduct);
                var product = _productRepository.GetEntity(newProduct);

                var purchaseInformation = new DAL.Models.PurchaseInfo
                {
                    PurchaseDate = purchaseInfo.SaleDate,
                    ManagerId = manager.Id,
                    ClientId = client.Id,
                    ProductId = product.Id
                };

                _purchaseInfoRepository.Add(purchaseInformation);
                _purchaseInfoRepository.SaveChanges();
            }
        }
    }
}
