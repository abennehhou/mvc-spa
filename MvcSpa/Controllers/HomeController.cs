using MvcSpa.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSpa.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //TODO use dependency injection.
            var productRepository = new ProductRepository();
            var products = productRepository.Get();

            return View(products);
        }
    }
}