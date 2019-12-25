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
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Models.DataModels.UserModels.User> userManager;
        private readonly RoleManager<Models.DataModels.UserModels.UserRole> roleManager;
        private readonly IWebHostEnvironment hostingEnvironment;

        public InventoryController(
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

        [HttpGet]
        public ActionResult GetProducts(int page, int itemsPerPage, string sortBy, bool sortDesc)
        {
            // this.context.Users
        }
    }
}
