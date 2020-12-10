using AspNetCoreShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreShop.ViewComponents
{
    public class BasketLinkViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public BasketLinkViewComponent(ApplicationDbContext db, UserManager<IdentityUser> UserManager)
        {
            _db = db;
            _userManager = UserManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);
            if (user == null)
                return Content("");

            var result = _db.Baskets.Where(b => b.UserId == user.Id).Sum(b => b.Count);
            
            return View("BasketLink", result);
        }
    }

}
