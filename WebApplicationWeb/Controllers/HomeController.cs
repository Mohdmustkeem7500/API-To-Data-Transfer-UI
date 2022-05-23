using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplicationWeb.Models;

namespace WebApplicationWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Employee> employees = new List<Employee>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001/");
            HttpResponseMessage response = await client.GetAsync("api/employees");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<List<Employee>>(results);
            }
            return View(employees);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Employee employee = await GetEmployeeByID(id);
            return View(employee);
        }

        private static async Task<Employee> GetEmployeeByID(int id)
        {
            Employee employee = new Employee();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001/");
            HttpResponseMessage response = await client.GetAsync($"api/employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<Employee>(results);
            }

            return employee;
        }

        public async Task<IActionResult> Delete(int id)
        {
            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001/");
            HttpResponseMessage response = await client.DeleteAsync($"api/employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        public  async Task<IActionResult> Create(Employee employee)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001/");
            var response = await client.PostAsJsonAsync("api/employees",employee);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)

        {
            Employee employee = await GetEmployeeByID(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)

        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001/");
            var response = await client.PutAsJsonAsync($"api/employees/{employee.Id}",employee);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
