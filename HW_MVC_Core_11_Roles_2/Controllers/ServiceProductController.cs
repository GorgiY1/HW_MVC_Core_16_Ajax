using HW_MVC_Core_11_Roles_2.DbContexts;
using HW_MVC_Core_11_Roles_2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_app.Models;

namespace HW_MVC_Core_11_Roles_2.Controllers
{
    public class ServiceProductController : Controller
    {
        private readonly IServiceProduct _serviceProduct;
        public ServiceProductController(IServiceProduct serviceProduct)
        {
            _serviceProduct = serviceProduct;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _serviceProduct.ReadAsync();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            var product = await _serviceProduct.GetByIdAsync(id);
            return View(product);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ViewResult Create() => View();
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                _ = await _serviceProduct.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Update(Guid? id) {
            //Product ? product = await _serviceProduct.GetByIdAsync(id);
            return View(await _serviceProduct.GetByIdAsync(id));
        } 
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid?id, [Bind("Id,Name,Price,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                _ = await _serviceProduct.UpdateAsync(id, product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id) => View(await _serviceProduct.GetByIdAsync(id));

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Product product)
        {
            var result = await _serviceProduct.DeleteAsync(product.Id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
