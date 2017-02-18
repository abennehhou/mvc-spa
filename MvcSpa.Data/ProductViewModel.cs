using System;
using System.Collections.Generic;

namespace MvcSpa.Data
{
    public class ProductViewModel : ViewModelBase
    {
        /// <summary>
        /// List of products to display.
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// Contains search filters on product.
        /// </summary>
        public SearchProductFilter SearchEntity { get; set; }

        /// <summary>
        /// Contains the product that we are adding or editing.
        /// </summary>
        public Product Entity { get; set; }

        private ProductRepository _productRepository;

        public ProductViewModel()
            : base()
        {
            //TODO use dependency injection.
            _productRepository = new ProductRepository();

            Products = new List<Product>();
            SearchEntity = new SearchProductFilter();
            Entity = new Product();
        }

        protected override void Add()
        {
            IsValid = true;
            Entity = new Product
            {
                IntroductionDate = DateTime.UtcNow,
                Url = "http://",
                Price = 0m
            };

            base.Add();
        }

        protected override void Edit()
        {
            IsValid = true;
            Guid productId;
            Guid.TryParse(EventArgument, out productId);
            Entity = _productRepository.Get(productId);
            if (Entity == null)
                throw new Exception($"Product {EventArgument} not found.");

            base.Edit();
        }

        protected override void Delete()
        {
            IsValid = true;
            Guid productId;
            Guid.TryParse(EventArgument, out productId);
            var isDeleted = _productRepository.Delete(productId);

            Get();
            base.Delete();
        }

        protected override void Save()
        {
            if (IsValid)
            {
                if (Mode == DisplayMode.Add)
                {
                    ValidationErrors = _productRepository.Insert(Entity);
                }
                else if (Mode == DisplayMode.Edit)
                {
                    ValidationErrors = _productRepository.Update(Entity);
                }
                else
                    throw new Exception("Unknown mode during save");
            }

            base.Save();
        }

        protected override void Get()
        {
            Products = _productRepository.Search(SearchEntity);
        }

        protected override void ResetSearch()
        {
            SearchEntity = new SearchProductFilter();

            base.ResetSearch();
        }
    }
}
