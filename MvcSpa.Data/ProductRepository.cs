using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcSpa.Data
{
    public class ProductRepository
    {
        public List<Product> Get(SearchProductFilter filter)
        {
            //TODO Use non mocked data
            var products = GetProductsQueryableData();
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                    products = products
                        .Where(product => product.Name != null
                                            && product.Name.StartsWith(filter.Name, StringComparison.InvariantCultureIgnoreCase));
            }

            return products.ToList();
        }

        private IQueryable<Product> GetProductsQueryableData()
        {
            var mockedData = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Pen",
                    IntroductionDate = new DateTime(2015, 01, 02),
                    Url = "http://my-domain/pen",
                    Price = 1.02m
                },
                new Product
                {
                    Id = 2,
                    Name = "Pinapple",
                    IntroductionDate = new DateTime(2015, 01, 22),
                    Url = "http://my-domain/pinapple",
                    Price = 2.05m
                },
                new Product
                {
                    Id = 3,
                    Name = "Apple",
                    IntroductionDate = new DateTime(2015, 01, 13),
                    Url = "http://my-domain/apple",
                    Price = 1.42m
                }
            };

            return mockedData.AsQueryable();
        }
    }
}
