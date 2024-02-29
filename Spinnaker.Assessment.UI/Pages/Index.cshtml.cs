using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spinnaker.Assessment.UI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace Spinnaker.Assessment.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration,ILogger<IndexModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        #region Properties
        [BindProperty]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Identity number is required")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "Identity number must be a 13-digit number")]
        public string IdentityNumber { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be a 10-digit number")]
        public string PhoneNumber { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Country is required")]
        public int CountryId { get; set; }

        public List<SelectListItem> Countries { get; set; }


        public IQueryable<Customers> Customers { get; set; }

        #endregion

        public async Task OnGetAsync()
        {
            var baseUrl = _configuration["ApiSettings:BaseUrl"];

            using (var httpClient = new HttpClient())
            {                
                HttpResponseMessage response = await httpClient.GetAsync(baseUrl + "country");
               
                if (response.IsSuccessStatusCode)
                {
                   
                    string jsonContent = await response.Content.ReadAsStringAsync();

                  
                    List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(jsonContent);

                    
                    Countries = countries.Select(country => new SelectListItem
                    {
                        Value = country.Id.ToString(), 
                        Text = country.FriendlyName 
                    }).ToList();
                }
                else
                {
                    
                }
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);

                try
                {
                    var response = await httpClient.GetAsync("Customer");
                    if (response.IsSuccessStatusCode)
                    {

                        var responseBody = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<List<Customers>>(responseBody);

                        if (responseObject is not null)
                        {
                            Customers = responseObject.AsQueryable();
                        }
                    }
                    else
                    {
                        throw new Exception($"API request failed with status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while making the API request.", ex);
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {               
                return Page();
            }

            var requestData = new
            {
                Name,
                Surname,
                IdentityNumber,
                Email,
                PhoneNumber,
                CountryId
            };

            var baseUrl = _configuration["ApiSettings:BaseUrl"];
            var jsonRequestData = JsonConvert.SerializeObject(requestData);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");
                try
                {
                    var response = await httpClient.PostAsync("Customer", content);
                    if (response.IsSuccessStatusCode)
                    {
                        //add toast notification here
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error occurred while processing your request.");
                        
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                }
            }

            return Page();
        }
    }
}
