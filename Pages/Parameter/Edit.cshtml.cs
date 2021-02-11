using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Amazon.SimpleSystemsManagement.Model;
using System.Net;

namespace aws_parameter_store_manager.Pages
{
    public class EditModel : PageModel
    {
          private readonly ILogger<EditModel> _logger;
        private readonly IParameterStoreService _awsParameterService;

        public EditModel(ILogger<EditModel> logger, IParameterStoreService awsParameterSerice)
        {
            _logger = logger;
            _awsParameterService = awsParameterSerice;
        }
        [BindProperty]
        public Amazon.SimpleSystemsManagement.Model.Parameter parameterToUpdate { get; set; }
        [BindProperty]
        public string ParameterType { get; set; }
        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
           var parameter = await _awsParameterService.GetParameter(new GetParameterRequest{
                Name= id,
                WithDecryption=true
            });
            parameterToUpdate = parameter.Parameter;
            ParameterType = parameter.Parameter.Type;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                parameterToUpdate.Type = ParameterType;
                var response = await _awsParameterService.UpdateParameter(parameterToUpdate);
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    Message = "Parameter Updated Successfully !";
                    return RedirectToPage("/Index");
                }                
            }
            return Page();
        }
    }
}