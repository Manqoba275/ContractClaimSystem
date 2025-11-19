using ContractClaimSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContractClaimSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Home/Index - Main landing page with role selection
        public IActionResult Index()
        {
            _logger.LogInformation("Home page accessed");
            return View();
        }

        // GET: Home/Lecturer - Lecturer dashboard
        public async Task<IActionResult> Lecturer()
        {
            try
            {
                var claims = await _context.Claims
                    .OrderByDescending(c => c.SubmittedDate)
                    .ToListAsync();

                _logger.LogInformation("Lecturer dashboard accessed with {Count} claims", claims.Count);
                return View(claims);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading lecturer dashboard");
                // In a real application, you might want to handle this more gracefully
                return View(new List<Models.Claim>());
            }
        }

        // GET: Home/Coordinator - Programme Coordinator dashboard
        public async Task<IActionResult> Coordinator()
        {
            try
            {
                var claims = await _context.Claims
                    .OrderByDescending(c => c.SubmittedDate)
                    .ToListAsync();

                _logger.LogInformation("Coordinator dashboard accessed with {Count} claims", claims.Count);
                return View(claims);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading coordinator dashboard");
                return View(new List<Models.Claim>());
            }
        }

        // GET: Home/Manager - Academic Manager dashboard
        public async Task<IActionResult> Manager()
        {
            try
            {
                var claims = await _context.Claims
                    .OrderByDescending(c => c.SubmittedDate)
                    .ToListAsync();

                _logger.LogInformation("Manager dashboard accessed with {Count} claims", claims.Count);
                return View(claims);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading manager dashboard");
                return View(new List<Models.Claim>());
            }
        }

        // GET: Home/Error - Error page
        public IActionResult Error()
        {
            return View();
        }
    }
}