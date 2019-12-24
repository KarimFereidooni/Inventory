namespace Inventory.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Inventory.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Models.DataModels.UserModels.User> userManager;
        private readonly RoleManager<Models.DataModels.UserModels.UserRole> roleManager;
        private readonly IWebHostEnvironment hostingEnvironment;

        public HomeController(
            ApplicationDbContext context,
            UserManager<Models.DataModels.UserModels.User> userManager,
            RoleManager<Models.DataModels.UserModels.UserRole> roleManager,
            IWebHostEnvironment hostingEnvironment)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Error()
        {
            return this.View();
        }

        public async Task<IActionResult> Init(bool migarte = true)
        {
            if (migarte)
            {
                await this.context.Database.MigrateAsync();
            }

            return this.Ok("OK");
        }

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
        [HttpGet]
        public ActionResult CheckConfiguration()
        {
            string webRootPath = this.hostingEnvironment.WebRootPath;
            string contentRootPath = this.hostingEnvironment.ContentRootPath;
#if DEBUG
            return this.Json(new { Configuration = "DEBUG", WebRootPath = webRootPath, ContentRootPath = contentRootPath });
#endif
#if RELEASE
            return this.Json(new { Configuration = "RELEASE", WebRootPath = webRootPath, ContentRootPath = contentRootPath });
#endif
#if ELECTRON
            return this.Json(new { Configuration = "ELECTRON", WebRootPath = webRootPath, ContentRootPath = contentRootPath });
#endif
        }
    }
}
