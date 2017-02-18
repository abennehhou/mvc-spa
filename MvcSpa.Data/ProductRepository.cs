using System;
using System.Collections.Generic;

namespace MvcSpa.Data
{
    public class ProductRepository
    {
        public List<Product> Get()
        {
            //TODO Use non mocked data
            return CreateMockData();
        }

        private List<Product> CreateMockData()
        {
            return new List<Product>
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
        }
    }
}
