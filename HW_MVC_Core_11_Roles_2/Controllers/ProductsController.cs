using HW_MVC_Core_11_Roles_2.DbContexts;
using HW_MVC_Core_11_Roles_2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_app.Models;

namespace Shop_app.Controllers
{

    public class ProductsController : Controller
    {
        private readonly UserContext? _userContext;
        private readonly ProductDbContext _productDbContext;
        public ProductsController(UserContext ?userContext, ProductDbContext productDbContext)
        {
            _userContext = userContext ?? throw new ArgumentNullException("Error, Context is reqired  ...");
            _productDbContext = productDbContext ?? throw new ArgumentNullException("Error Product context is not found");
        }
        //================================================================================
        [HttpGet]
        public IActionResult Index()
        {
            return View(_productDbContext.Products.ToList());
            // Ваш код
            //return View(ProductCollection.Products);
        }
        //================================================================================
        [HttpGet]
        public IActionResult Details(Guid? id)
        {
            
            if (id == null)
            {
                id = new Guid("19BC4394-0A11-488E-AA56-19197159760E");
            }

            var product = _productDbContext.Products.FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return BadRequest("Product not found");
            }

            return View(product);
        }
        //================================================================================
        [HttpPost]
        public IActionResult Details(Product? product)
        {
            //id = new Guid("19BC4394-0A11-488E-AA56-19197159760E");
            if (product?.Id == null)
            {
                if (product == null)
                {
                    return NotFound();
                }
                return NotFound();
            }

            //var product = _productDbContext.Products.FirstOrDefault(m => m.Id == id);
            
            return RedirectToAction("Update",product);
        }
        //================================================================================
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ViewResult Create() => View();
        //================================================================================
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Price,Description")] Product product)
        {
            _productDbContext.Products.Add(product);
            _productDbContext.SaveChanges();

            return RedirectToAction("Details", product);
        }
        //================================================================================
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Update(Guid? id)
        {
            if (id == null)
            {
                id = new Guid("19BC4394-0A11-488E-AA56-19197159760E");
            }
            if (_productDbContext.Products.FirstOrDefault(p => p.Id == id) == null)
            {
                return BadRequest("Id is not exist");
            }
            return View(_productDbContext.Products.FirstOrDefault(p => p.Id == id));
        }
        //================================================================================
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid?id,[Bind("Id,Name,Price,Description")] Product? product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var existingProduct = await _productDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (existingProduct == null)
            {
                return BadRequest("Product not found");
            }
            if (product == null)
            {
                return BadRequest("Product data is required.");
            }

            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.Name = product.Name;

            _productDbContext.SaveChanges();
            return RedirectToAction("Details",new {id = existingProduct.Id});
        }
        //================================================================================
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            // Перевіряємо, чи передано коректний id
            if (id == null)
            {
                id = new Guid("19BC4394-0A11-488E-AA56-19197159760E");
            }

            // Знаходимо продукт у базі даних
            var product = await _productDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            // Якщо продукт не знайдено
            if (product == null)
            {
                return NotFound();
            }

            return View(product); // Показуємо сторінку з підтвердженням видалення
        }
        //================================================================================
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Знаходимо продукт у базі даних
            var product = await _productDbContext.Products.FindAsync(id);

            // Якщо продукт не знайдено
            if (product == null)
            {
                return NotFound();
            }

            // Видаляємо продукт з бази даних
            _productDbContext.Products.Remove(product);

            // Зберігаємо зміни
            await _productDbContext.SaveChangesAsync();

            // Повертаємось до списку продуктів після видалення
            return RedirectToAction("Index");
        }
    }
}
