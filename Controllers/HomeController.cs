using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareHub.Data;
using System.Diagnostics;

namespace PetCareHub.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;
    public HomeController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        ViewBag.PetCount = await _db.Pets.CountAsync();
        ViewBag.AppointmentCount = await _db.Appointments.CountAsync();
        ViewBag.VaccineCount = await _db.Vaccinations.CountAsync();
        return View();
    }

    public IActionResult Privacy() => View();
    public IActionResult Error() => View();
}
