using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetCareHub.Data;
using PetCareHub.Models;
using System.Security.Claims;

namespace PetCareHub.Controllers;

[Authorize]
public class VaccinationsController : Controller
{
    private readonly ApplicationDbContext _db;
    public VaccinationsController(ApplicationDbContext db) => _db = db;
    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    public async Task<IActionResult> Index() => View(await _db.Vaccinations.Include(v => v.Pet)
        .Where(v => v.OwnerId == UserId).OrderBy(v => v.NextDueDate).ToListAsync());

    public async Task<IActionResult> Create()
    {
        await LoadPets();
        return View(new Vaccination());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Vaccination vaccination)
    {
        if (!await _db.Pets.AnyAsync(p => p.Id == vaccination.PetId && p.OwnerId == UserId))
            ModelState.AddModelError(nameof(vaccination.PetId), "Please select one of your own pets.");
        ModelState.Remove(nameof(vaccination.OwnerId));
        ModelState.Remove(nameof(vaccination.Pet));
        if (!ModelState.IsValid) { await LoadPets(vaccination.PetId); return View(vaccination); }
        vaccination.OwnerId = UserId;
        _db.Vaccinations.Add(vaccination);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Vaccination record saved.";
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadPets(int? selected = null) =>
        ViewBag.PetId = new SelectList(await _db.Pets.Where(p => p.OwnerId == UserId).ToListAsync(), "Id", "Name", selected);
}
