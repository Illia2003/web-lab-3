using LR3.DbConnection;
using LR3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LR3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var records = await _context.Products
                .AsNoTracking()
                .ToListAsync();

            return View(records);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetModifyProduct(Guid id )
        {
            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            return View("ModifyProduct", product);
        }

        [HttpPost]
        public async Task<IActionResult> ModifyProduct(Product modProduct)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(t => t.Id == modProduct.Id);

            product.Title = modProduct.Title;
            product.Description = modProduct.Description;
            product.Price = modProduct.Price;
            product.ProductType = modProduct.ProductType;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct([FromRoute]Guid id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            return View(product);
        }

        public async Task<IActionResult> DeleteProduct([FromRoute]Guid id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(t => t.Id == id);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}