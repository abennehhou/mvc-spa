using System;

namespace MvcSpa.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime IntroductionDate { get; set; }
        public string Url { get; set; }
        public decimal Price { get; set; }
    }
}
