using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicStore.ViewComponents
{
    public class CartItemCountViewComponent : ViewComponent
    {
        private readonly Contexts _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CartItemCountViewComponent(Contexts context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null) return Content("0");

            var cart = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.UserId == user.Id && o.Status == "Cart");

            return View(cart?.OrderItems.Sum(oi => oi.Quantity) ?? 0);
        }
    }
}