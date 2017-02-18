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
        /// Display mode, if any
        /// </summary>
        public DisplayMode? Mode { get; set; }

        /// <summary>
        /// Argument passed with the command.
        /// </summary>
        public string EventArgument { get; set; }

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
            EventArgument = null;
            SetListVisibility();
        }

        public void HandleRequest()
        {
            switch (EventCommand)
            {
                case EventCommandType.List:
                case EventCommandType.Search:
                case EventCommandType.Cancel:
                    SetListVisibility();
                    Get();
                    break;
                case EventCommandType.ResetSearch:
                    SetListVisibility();
                    ResetSearch();
                    Get();
                    break;
                case EventCommandType.Add:
                    Add();
                    break;
                case EventCommandType.Save:
                    Save();
                    break;
                case EventCommandType.Edit:
                    Edit();
                    break;
                case EventCommandType.Delete:
                    Delete();
                    break;
                default:
                    break;
            }
        }

        private void SetListVisibility()
        {
            IsValid = true;
            IsSearchAreaVisible = true;
            IsListAreaVisible = true;
            IsDetailAreaVisible = false;
        }

        private void SetDetailsVisibility()
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

            SetDetailsVisibility();
            Mode = DisplayMode.Add;
        }

        private void Edit()
        {
            IsValid = true;
            Guid productId;
            Guid.TryParse(EventArgument, out productId);
            Entity = _productRepository.Get(productId);
            if (Entity == null)
                throw new Exception($"Product {EventArgument} not found.");

            SetDetailsVisibility();
            Mode = DisplayMode.Edit;
        }

        private void Delete()
        {
            IsValid = true;
            Guid productId;
            Guid.TryParse(EventArgument, out productId);
            var isDeleted = _productRepository.Delete(productId);
            SetListVisibility();
            Mode = null;
            Get();
        }

        private void Save()
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

                IsValid = !ValidationErrors.Any();
            }

            if (IsValid)
            {
                SetListVisibility();
                Get();
            }
            else
            {
                SetDetailsVisibility();
            }
        }

        private void Get()
        {
            Products = _productRepository.Search(SearchEntity);
        }

        private void ResetSearch()
        {
            SearchEntity = new SearchProductFilter();
        }
    }
}
