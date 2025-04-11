using ADO.Net_App.Models;
using ADO.Net_App.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ADO.Net_App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        // GET: ProductController
        public IActionResult Index()
        {
            var products = _productRepo.GetAll();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        // GET: ProductController/Details/5
        public IActionResult Details(int id)
        {
            var product = _productRepo.GetById(id);
            return View(product);
        }

        // GET: ProductController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            try
            {
                _productRepo.Add(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _productRepo.GetById(id);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            try
            {
                _productRepo.Update(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _productRepo.GetById(id);
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _productRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
