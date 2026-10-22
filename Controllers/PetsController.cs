using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareHub.Data;
using PetCareHub.Models;
using System.Security.Claims;

namespace PetCareHub.Controllers;

[Authorize]
public class PetsController : Controller
{
    private readonly ApplicationDbContext _db;
    public PetsController(ApplicationDbContext db) => _db = db;
    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    public async Task<IActionResult> Index() =>
        View(await _db.Pets.Where(p => p.OwnerId == UserId).OrderBy(p => p.Name).ToListAsync());

    public IActionResult Create() => View(new Pet());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Pet pet)
    {
        ModelState.Remove(nameof(pet.OwnerId));
        ModelState.Remove(nameof(pet.Owner));
        if (!ModelState.IsValid) return View(pet);
        pet.OwnerId = UserId;
        _db.Pets.Add(pet);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Pet profile added successfully!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var pet = await _db.Pets.FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == UserId);
        return pet == null ? NotFound() : View(pet);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Pet input)
    {
        if (id != input.Id) return NotFound();
        ModelState.Remove(nameof(input.OwnerId));
        ModelState.Remove(nameof(input.Owner));
        if (!ModelState.IsValid) return View(input);
        var pet = await _db.Pets.FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == UserId);
        if (pet == null) return NotFound();
        pet.Name = input.Name; pet.Species = input.Species; pet.Breed = input.Breed;
        pet.Age = input.Age; pet.Gender = input.Gender; pet.Notes = input.Notes;
        await _db.SaveChangesAsync();
        TempData["Success"] = "Pet profile updated.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var pet = await _db.Pets.FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == UserId);
        return pet == null ? NotFound() : View(pet);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var pet = await _db.Pets.FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == UserId);
        if (pet == null) return NotFound();
        _db.Pets.Remove(pet);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Pet profile deleted.";
        return RedirectToAction(nameof(Index));
    }
}
