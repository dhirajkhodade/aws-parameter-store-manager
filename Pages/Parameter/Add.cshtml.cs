using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace aws_parameter_store_manager.Pages
{
    public class AddModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IParameterStoreService _awsParameterService;

        public AddModel(ILogger<IndexModel> logger, IParameterStoreService awsParameterSerice)
        {
            _logger = logger;
            _awsParameterService = awsParameterSerice;
        }

        [BindProperty]
        public Amazon.SimpleSystemsManagement.Model.PutParameterRequest ParameterToCreate { get; set; }
        [BindProperty]
        public string ParameterType { get; set; }

        [TempData]
        public string Message { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                ParameterToCreate.Type = ParameterType;
                var response = await _awsParameterService.CreateParameter(ParameterToCreate);
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    Message = "New Parameter Added Successfully !";
                    return RedirectToPage("/Index");
                }                
            }
            return Page();
        }
    }
}