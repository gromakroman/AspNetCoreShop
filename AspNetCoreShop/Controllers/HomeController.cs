using AspNetCoreShop.Data;
using AspNetCoreShop.Model;
using AspNetCoreShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, SignInManager<IdentityUser> SignInManager, UserManager<IdentityUser> UserManager)
        {
            _logger = logger;
            _db = db;
            _signInManager = SignInManager;
            _userManager = UserManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Category(int id)
        {
            var category = _db.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
                NotFound();

            var result = category.Products;

            ViewBag.Title = category.Name;
            ViewBag.CurentCategoryId = id;

            return View(result);
        }

        public IActionResult Details(int id, bool isBasket = false)
        {
            var product = _db.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            ViewBag.Title = product.Name;
            ViewBag.CurentCategoryId = product.CategoryId;

            if (isBasket)
                ViewBag.Popup = $"Товар \"{product.Name}\" добавлен в корзину";

            return View(product);
        }

        public async Task<IActionResult> BasketAdd(int id)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Redirect($"/Identity/Account/Login?ReturnUrl=/Home/BasketAdd/{id}");
            }

            var product = _db.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            var basketitem = await _db.Baskets.FirstOrDefaultAsync(b => b.UserId == user.Id && b.ProductId == product.ProductId);

            if (basketitem == null)
            {
                basketitem = new Basket { ProductId = product.ProductId, UserId = user.Id, Count = 1 };
                await _db.Baskets.AddAsync(basketitem);
            }
            else
            {
                basketitem.Count++;
            }
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id, isBasket = true });
        }

        public async Task<IActionResult> BasketInc(int id)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Redirect("/Identity/Account/Login");
            }
            var basket = await _db.Baskets.FirstOrDefaultAsync(b => b.BasketId == id);
            if (basket == null)
                return NotFound();

            basket.Count++;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Basket));
        }

        public async Task<IActionResult> BasketDec(int id)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Redirect("/Identity/Account/Login");
            }
            var basket = await _db.Baskets.FirstOrDefaultAsync(b => b.BasketId == id);
            if (basket == null)
                return NotFound();

            if (basket.Count > 1)
                basket.Count--;
            else
                _db.Baskets.Remove(basket);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Basket));
        }

        public async Task<IActionResult> BasketDelete(int id)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Redirect("/Identity/Account/Login");
            }
            var basket = await _db.Baskets.FirstOrDefaultAsync(b => b.BasketId == id);
            if (basket == null)
                return NotFound();

            _db.Baskets.Remove(basket);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Basket));
        }

        public async Task<IActionResult> Basket()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return Redirect("/Identity/Account/Login");
            }

            var id = (await _userManager.GetUserAsync(User)).Id;
            var result = await _db.Baskets.Where(b => b.UserId == id).Select(b => new BasketViewModel
            {
                BasketId = b.BasketId,
                Name = b.Product.Name,
                Count = b.Count,
                Price = b.Product.Price,
                Amount = b.Product.Price * b.Count,
                Image = b.Product.Image
            }).ToListAsync();

            ViewBag.Title = "Корзина";

            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
