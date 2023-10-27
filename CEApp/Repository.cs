using Azure.Core;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CEApp
{
    public class Repository : HttpClientCE
    {
        //Authentication
        private readonly string AuthenticationUrl = "api/authentication";
        private readonly string LoginUrl = "api/authentication/login";

        //Companies
        private readonly string ComapniesUrl = "api/companies";
        private readonly string ComapniesCollectionUrl = "api/companies/collection";

        //Employees
        private readonly string EmployeesUrl = "/employees";

        //Grades
        private readonly string GradesUrl = "api/grades";
        private readonly string GradesCollectionUrl = "api/grades/collection";

        //Students
        private readonly string StundetsUrl = "/students";

        private readonly HttpClient _httpClient;
        public Repository()
        {
            _httpClient = GetHttpClient();
        }
        public async Task<HttpResponseMessage> PostAuthenticationLogin(UserForAuthenticationDto login)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(LoginUrl, login);
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(await responseMessage.Content.ReadAsStringAsync());
            TokenStorage.Key = responseData.access_token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenStorage.Key);
            return responseMessage;
        }
        public async Task<HttpResponseMessage> PostAuthentication(UserForRegistrationDto register)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(AuthenticationUrl, register);
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(await responseMessage.Content.ReadAsStringAsync());
            TokenStorage.Key = responseData.access_token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenStorage.Key);
            return responseMessage;
        }
        public async Task<List<Company>> GetCompaniesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Company>>(ComapniesUrl);
        }
    }
}
