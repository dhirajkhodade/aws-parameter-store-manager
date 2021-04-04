using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon.SimpleSystemsManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
        [BindProperty]
        public IFormFile UploadedFile { get; set; }

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

        public async Task OnPostUploadAsync()
        {
            if (UploadedFile == null || UploadedFile.Length == 0)
            {
                return;
            }
            using (var ms = new MemoryStream())
            {
                UploadedFile.CopyTo(ms);
                var jsonString = Encoding.ASCII.GetString(ms.ToArray());
                var paramList = JsonConvert.DeserializeObject<List<PutParameterRequest>>(jsonString);
                foreach (var param in paramList)
                {
                    var response=await _awsParameterService.CreateParameter(param);
                }
            }
        }
    }
}