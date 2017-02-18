using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcSpa.Data
{
    public class ProductRepository
    {
        private static List<Product> _mockedData = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Pen",
                IntroductionDate = new DateTime(2015, 01, 02),
                Url = "http://my-domain/pen",
                Price = 1.02m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Pinapple",
                IntroductionDate = new DateTime(2015, 01, 22),
                Url = "http://my-domain/pinapple",
                Price = 2.05m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Apple",
                IntroductionDate = new DateTime(2015, 01, 13),
                Url = "http://my-domain/apple",
                Price = 1.42m
            }
        };

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

        public List<KeyValuePair<string, string>> Insert(Product entity)
        {
            var validationErrors = ValidateNewProduct(entity);
            if (!validationErrors.Any())
            {
                entity.Id = Guid.NewGuid();
                var databaseEntity = new Product
                {
                    Id = entity.Id,
                    IntroductionDate = entity.IntroductionDate,
                    Name = entity.Name,
                    Price = entity.Price,
                    Url = entity.Url
                };
                _mockedData.Add(databaseEntity);
            }

            return validationErrors;
        }

        private List<KeyValuePair<string, string>> ValidateNewProduct(Product entity)
        {
            var validationErrors = new List<KeyValuePair<string, string>>();
            var products = GetProductsQueryableData();
            if (products.Any(product => string.Equals(product.Name, entity.Name, StringComparison.OrdinalIgnoreCase)))
            {
                validationErrors.Add(new KeyValuePair<string, string>("Name", "Product name already exists."));
            }

            return validationErrors;
        }

        private IQueryable<Product> GetProductsQueryableData()
        {
            return _mockedData.AsQueryable();
        }
    }
}
