namespace Inventory.Controllers
{
    using Inventory.Data;
    using Inventory.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using OfficeOpenXml;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment env;

        public InventoryController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            this.env = env;
            this.context = context;
        }

        /// <summary>
        /// Get paginated list of products.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetProducts(int page, int itemsPerPage, string sortBy, bool sortDesc)
        {
            var query = this.context.Products.Select(x => new
            {
                x.Id,
                x.Name,
                Count = x.Count.ToString() + " " + x.EnumerationUnit,
            });
            var data = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortDesc)
                {
                    data = data.DynamicOrderByDescending(sortBy);
                }
                else
                {
                    data = data.DynamicOrderBy(sortBy);
                }
            }

            return this.Json(new { Items = await data.ToArrayAsync(), TotalCount = await query.CountAsync() });
        }

        /// <summary>
        /// Add a new product.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody]Models.ViewModels.ProductViewModel model)
        {
            Models.DataModels.Product product = new Models.DataModels.Product(model);
            this.context.Products.Add(product);
            await this.context.SaveChangesAsync();
            return this.Ok();
        }

        /// <summary>
        /// Export to Excel.
        /// </summary>
        [HttpGet]
        public IActionResult ExportExcel(string sortBy, bool sortDesc)
        {
            using (MemoryStream ms = new MemoryStream(Properties.Resources.ExportProducts))
            {
                using (ExcelPackage pck = new ExcelPackage(ms))
                {
                    using (ExcelWorksheet ws = pck.Workbook.Worksheets[0])
                    {
                        var query = this.context.Products.Select(x => new
                        {
                            x.Id,
                            x.Name,
                            x.Count,
                            x.EnumerationUnit
                        });
                        if (!string.IsNullOrEmpty(sortBy))
                        {
                            if (sortDesc)
                            {
                                query = query.DynamicOrderByDescending(sortBy);
                            }
                            else
                            {
                                query = query.DynamicOrderBy(sortBy);
                            }
                        }

                        ws.Cells["B2"].Value = query.Count().ToString();
                        ws.Cells["B3"].Value = query.Sum(x => x.Count).ToString();
                        int row = 5;
                        foreach (var p in query)
                        {
                            ws.Cells["A" + row.ToString()].Value = p.Id;
                            ws.Cells["B" + row.ToString()].Value = p.Name;
                            ws.Cells["C" + row.ToString()].Value = p.Count.ToString() + " " + p.EnumerationUnit;
                            row++;
                        }

                        using (MemoryStream saveMs = new MemoryStream())
                        {
                            pck.SaveAs(saveMs);
                            return this.File(saveMs.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        }
                    }
                }
            }
        }
    }
}
