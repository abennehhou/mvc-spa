using System.Collections.Generic;

namespace MvcSpa.Data
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }
        public EventCommandType EventCommand { get; set; }
        public SearchProductFilter SearchEntity { get; set; }

        private ProductRepository _productRepository;

        public ProductViewModel()
        {
            //TODO use dependency injection.
            _productRepository = new ProductRepository();
            Products = new List<Product>();
            SearchEntity = new SearchProductFilter();

            // Use List as the default value for event command
            EventCommand = EventCommandType.List;
        }

        public void HandleRequest()
        {
            switch (EventCommand)
            {
                case EventCommandType.List:
                case EventCommandType.Search:
                    Get();
                    break;
                case EventCommandType.ResetSearch:
                    ResetSearch();
                    Get();
                    break;
                default:
                    break;
            }
        }

        private void Get()
        {
            var productRepository = new ProductRepository();
            Products = productRepository.Get(SearchEntity);
        }

        private void ResetSearch()
        {
            SearchEntity = new SearchProductFilter();
        }
    }
}
