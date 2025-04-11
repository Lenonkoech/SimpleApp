using ADO.Net_App.Models;
using System.Collections.Generic;

namespace ADO.Net_App.Repository
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        void PatchProduct(Product product);
        void Initialize();
    }
}
