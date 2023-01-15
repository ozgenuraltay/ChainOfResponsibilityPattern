using ChainOfResponsibilityPattern.ChainOfResponsibility;
using ChainOfResponsibilityPattern.DAL;
using ChainOfResponsibilityPattern.DAL.Entities;
using ChainOfResponsibilityPattern.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChainOfResponsibilityPattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public HomeController(ILogger<HomeController> logger, Context context, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail()
        {
            AppUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var products = await _context.Products.ToListAsync();
            var excelProcessHandler = new ExcelProcessHandler<Product>();
            var zipFileProcessHandler = new ZipFileProcessHandler<Product>();
            var sendEmailProcessHandler = new SendEmailProcessHandler(user);

            excelProcessHandler.SetNext(zipFileProcessHandler).SetNext(sendEmailProcessHandler);

             await excelProcessHandler.HandleAsync(products);
            return View(nameof(Index));
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
