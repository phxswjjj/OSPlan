using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSPlan
{
    class ProductRepository : IRepository<Product>
    {
        List<Product> Products;

        public ProductRepository(IRepository<ProductPartEntity> repo)
        {
            var dic = new Dictionary<string, Product>();
            var list = repo.ReadAll();
            var gProducts = list.GroupBy(p => p.ProductName);
            foreach (var gProduct in gProducts)
            {
                var product = new Product(gProduct.Key, gProduct.ToList());
                dic.Add(gProduct.Key, product);
            }
            this.Products = dic.Select(d => d.Value).ToList();
        }

        public Product Read(Predicate<Product> find)
        {
            return Products.Find(find);
        }

        public IEnumerable<Product> Reads(Predicate<Product> find)
        {
            return Products.FindAll(find);
        }

        public IEnumerable<Product> ReadAll()
        {
            return Products;
        }
    }
}
