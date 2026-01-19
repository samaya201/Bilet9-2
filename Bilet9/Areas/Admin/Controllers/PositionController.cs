using Bilet9.Context;
using Bilet9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bilet9.Areas.Admin.Controllers;
[Area("Admin")]

public class PositionController(AppDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var positions = await _context.Positions.ToListAsync();
        return View(positions);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Position position)
    {
        if (!ModelState.IsValid)
        {
            return View(position);
        }

        Position newPosition = new() 
        {
            Title = position.Title
        };
        _context.Positions.Add(newPosition);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");

    }
    public async Task<IActionResult> UpdateAsync(int id)
    {
        var position =  await _context.Positions.FindAsync(id);
        return View(position);

    }
    [HttpPost]
    public async Task<IActionResult> Update(Position position)
    {
        var isExistPosition = await _context.Positions.FindAsync(position.Id);
        if (isExistPosition is null)
            return NotFound();
        isExistPosition.Title = position.Title;

        _context.Positions.Update(isExistPosition);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var position = await _context.Positions.FindAsync(id);
        if(position is null)
            return NotFound();
        _context.Positions.Remove(position);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
