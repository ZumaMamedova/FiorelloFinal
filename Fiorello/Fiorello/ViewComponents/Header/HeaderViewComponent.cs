using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.ViewComponents.Header
{

    public class HeaderViewComponent:ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public HeaderViewComponent(AppDbContext appDbContext, UserManager<AppUser> _userManager, UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        private readonly UserManager<AppUser> _userManager;
        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            ViewBag.UserFullname = "";
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.UserFullname = user.FullName;
            }
            
            return View(take);
        }
    }
}
