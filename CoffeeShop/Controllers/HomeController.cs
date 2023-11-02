using CoffeeShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoffeeShop.Controllers
{
    public class HomeController : Controller
    {
        public CoffeeShopContext db = new CoffeeShopContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Technically the db.table syntax return an ienumerable which can just call toList on
            List<Product> products = db.Products.ToList();
            return View(products);
        }

        public IActionResult Product(int id)
        {
            Product p = db.Products.Find(id);
            return View(p);
        }

        //This action is a get that exists to display our view 
        public IActionResult CreateProduct()
        {
            return View();
        }

        //This second actions exists to add the movie to our table and redirect us to the main listing
        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            db.Products.Add(product);
            //This needs to be called after any create, delete or edit actions 
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //We will use the id to find the movie we want to update
        public IActionResult UpdateProduct(int id)
        {
            Product update = db.Products.Find(id);
            return View(update);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product p)
        {
            db.Products.Update(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //This display a view asking if you are sure you want to delete this movie
        public IActionResult DeleteProduct(int id)
        {
            Product p = db.Products.Find(id);
            return View(p);
        }

        public IActionResult IAmSureDeleteProduct(int id)
        {
            Product p = db.Products.Find(id);
            db.Products.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}