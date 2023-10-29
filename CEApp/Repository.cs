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
using System.Windows;

namespace CEApp
{
    public class Repository : HttpClientCE
    {
        //Authentication
        private readonly string AuthenticationUrl = "api/authentication";
        private readonly string LoginUrl = "api/authentication/login";

        //Companies
        private readonly string ComapniesUrl = "api/companies";
        private readonly string ComapniesCollectionUrl = "api/companies/collection/({ids})?=";

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
        public async Task<HttpResponseMessage> PostAuthenticationRegister(UserForRegistrationDto register)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(AuthenticationUrl, register);
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(await responseMessage.Content.ReadAsStringAsync());
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenStorage.Key);
            return responseMessage;
        }
        public async Task<List<Company>> GetCompaniesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Company>>(ComapniesUrl);
        }
        public async Task<List<Company>> GetCompanyCollectionAsync(List<string> ids)
        {
            string comapniesCollectionFullUrl = ComapniesCollectionUrl;
            foreach (var id in ids)
            {
                comapniesCollectionFullUrl += "ids" + id.ToString() + "&";
            }
            return await _httpClient.GetFromJsonAsync<List<Company>>(comapniesCollectionFullUrl);
        }

        public async Task<HttpResponseMessage> PostCompanyAsync(CompanyForCreationDto company)
        {
            var response = await _httpClient.PostAsJsonAsync<CompanyForCreationDto>(ComapniesUrl, company);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show($"Company with name {company.Name} is created");
                return response;
            }
            else
            {

                MessageBox.Show(response.StatusCode.ToString());
                return response;
            }

        }
        public async Task<HttpResponseMessage> DeleteCompanyAsync(string id)
        {
            var response = await _httpClient.DeleteAsync(ComapniesUrl+ "/" +id);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show($"Company with id {id} is deleted");
                return response;
            }
            else
            {

                MessageBox.Show(response.StatusCode.ToString());
                return response;
            }
        }
    }
}
