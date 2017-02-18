using MvcSpa.Data;
using System.Web.Mvc;

namespace MvcSpa.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var productViewModel = new ProductViewModel();
            productViewModel.HandleRequest();

            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult Index(ProductViewModel productViewModel)
        {
            productViewModel.HandleRequest();

            ModelState.Clear();

            //TODO We should redirect to an action instead of returning a view in the HttpPost :(
            // If we do so, we will avoid posting the form when we refresh the page.   
            return View(productViewModel);
        }
    }
}
