using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Amazon.SimpleSystemsManagement.Model;
using System.Net;

namespace aws_parameter_store_manager.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IParameterStoreService _awsParameterService;

        public IndexModel(ILogger<IndexModel> logger, IParameterStoreService awsParameterSerice)
        {
            _logger = logger;
            _awsParameterService = awsParameterSerice;
        }

        public IEnumerable<Amazon.SimpleSystemsManagement.Model.Parameter> Parameters { get; set; }
        public Pager Pager { get; set; }
        public SelectList PageSizeList { get; set; }
        public int PageSize { get; set; }
        [TempData]
        public string Message { get; set; }



        public async Task OnGet(int currentPage = 1)
        {

            PageSizeList = new SelectList(new[] { 1, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150 });
            PageSize = HttpContext.Session.GetInt32("PageSize") ?? 10;

            var allParameters = await _awsParameterService.GetAllParameters();
            Pager = new Pager(allParameters.Count, currentPage, PageSize);

            Parameters = allParameters.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize);
        }

        public async Task<JsonResult> OnGetParameters()
        {
            var allParameters = await _awsParameterService.GetAllParameters();
            return new JsonResult(allParameters);
        }

        public IActionResult OnPost(int pageSize)
        {
            HttpContext.Session.SetInt32("PageSize", pageSize);
            return Redirect("/");
        }

        public async Task<IActionResult> OnPostDelete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var response = await _awsParameterService.DeleteParameter(new DeleteParameterRequest()
                {
                    Name = id,
                });
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    Message = "Parameter Deleted Successfully !";
                   return Redirect("/");
                }
            }
            return Page();
        }
    }
}
