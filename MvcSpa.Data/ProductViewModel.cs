using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcSpa.Data
{
    public class ProductViewModel
    {
        /// <summary>
        /// List of products to display.
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// Type of event command
        /// </summary>
        public EventCommandType EventCommand { get; set; }

        /// <summary>
        /// Contains search filters on product.
        /// </summary>
        public SearchProductFilter SearchEntity { get; set; }

        /// <summary>
        /// Contains the product that we are adding or editing.
        /// </summary>
        public Product Entity { get; set; }

        /// <summary>
        /// Indicates if product details area is visible.
        /// </summary>
        public bool IsDetailAreaVisible { get; set; }

        /// <summary>
        /// Indicates if the list of products area is visible.
        /// </summary>
        public bool IsListAreaVisible { get; set; }

        /// <summary>
        /// Indicates if search product area is visible.
        /// </summary>
        public bool IsSearchAreaVisible { get; set; }

        /// <summary>
        /// Indicates if the model is valid.
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// List of additional validation errors.
        /// </summary>
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }

        private ProductRepository _productRepository;

        public ProductViewModel()
        {
            //TODO use dependency injection.
            _productRepository = new ProductRepository();

            Products = new List<Product>();
            SearchEntity = new SearchProductFilter();
            Entity = new Product();
            ValidationErrors = new List<KeyValuePair<string, string>>();

            EventCommand = EventCommandType.List;
            SetListMode();
        }

        public void HandleRequest()
        {
            switch (EventCommand)
            {
                case EventCommandType.List:
                case EventCommandType.Search:
                case EventCommandType.Cancel:
                    SetListMode();
                    Get();
                    break;
                case EventCommandType.ResetSearch:
                    SetListMode();
                    ResetSearch();
                    Get();
                    break;
                case EventCommandType.Add:
                    Add();
                    break;
                case EventCommandType.Save:
                    Save();
                    break;
                default:
                    break;
            }
        }

        private void SetListMode()
        {
            IsValid = true;
            IsSearchAreaVisible = true;
            IsListAreaVisible = true;
            IsDetailAreaVisible = false;
        }

        private void SetAddMode()
        {
            IsSearchAreaVisible = false;
            IsListAreaVisible = false;
            IsDetailAreaVisible = true;
        }

        private void Add()
        {
            IsValid = true;
            Entity = new Product
            {
                IntroductionDate = DateTime.UtcNow,
                Url = "http://",
                Price = 0m
            };

            SetAddMode();
        }

        private void Save()
        {
            if (IsValid)
            {
                ValidationErrors = _productRepository.Insert(Entity);
                IsValid = !ValidationErrors.Any();
            }

            if (IsValid)
            {
                SetListMode();
                Get();
            }
            else
            {
                SetAddMode();
            }
        }

        private void Get()
        {
            Products = _productRepository.Get(SearchEntity);
        }

        private void ResetSearch()
        {
            SearchEntity = new SearchProductFilter();
        }
    }
}
