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

        public ProductRepository(IRepository<ProductEqpPlan> plans, IRepository<Part> partRepo, IRepository<ProductPartRelation> productRepo)
        {
            var products = new List<Product>();
            foreach (var productPlan in plans.ReadAll())
            {
                var product = new Product(productPlan, partRepo, productRepo);
                products.Add(product);
            }
            this.Products = products;
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
