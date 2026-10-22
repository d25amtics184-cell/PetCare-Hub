using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetCareHub.Data;
using PetCareHub.Models;
using System.Security.Claims;

namespace PetCareHub.Controllers;

[Authorize]
public class AppointmentsController : Controller
{
    private readonly ApplicationDbContext _db;
    public AppointmentsController(ApplicationDbContext db) => _db = db;
    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    public async Task<IActionResult> Index() => View(await _db.Appointments
        .Include(a => a.Pet).Where(a => a.OwnerId == UserId)
        .OrderByDescending(a => a.AppointmentDate).ToListAsync());

    public async Task<IActionResult> Create()
    {
        await LoadPets();
        return View(new Appointment { AppointmentDate = DateTime.Now.AddDays(1) });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Appointment appointment)
    {
        if (!await _db.Pets.AnyAsync(p => p.Id == appointment.PetId && p.OwnerId == UserId))
            ModelState.AddModelError(nameof(appointment.PetId), "Please select one of your own pets.");
        ModelState.Remove(nameof(appointment.OwnerId));
        ModelState.Remove(nameof(appointment.Pet));
        if (!ModelState.IsValid) { await LoadPets(appointment.PetId); return View(appointment); }
        appointment.OwnerId = UserId;
        appointment.Status = "Pending";
        _db.Appointments.Add(appointment);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Appointment request submitted.";
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadPets(int? selected = null) =>
        ViewBag.PetId = new SelectList(await _db.Pets.Where(p => p.OwnerId == UserId).ToListAsync(), "Id", "Name", selected);
}
