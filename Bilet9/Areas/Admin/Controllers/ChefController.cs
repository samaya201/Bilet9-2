using System.Threading.Tasks;
using Bilet9.Context;
using Bilet9.Helper;
using Bilet9.Models;
using Bilet9.ViewModels.ChefViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bilet9.Areas.Admin.Controllers;
[Area("Admin")]
public class ChefController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly string _folderPath;

    public ChefController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
        _folderPath = Path.Combine(_environment.WebRootPath,"images");
    }

    public async Task<IActionResult> Index()
    {
        var chefs = await _context.Chefs.Select(chef => new ChefGetVM() 
        {
            Id=chef.Id,
            Name=chef.Name,
            ImagePath=chef.ImagePath,
            PositionTitle=chef.Position.Title
        }).ToListAsync();

        return View(chefs);
    }

    public async Task<IActionResult> Create()
    {
        await SendPositionWithViewBag();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(ChefCreateVM vm)
    {
        await SendPositionWithViewBag();
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        //Check position
        var isPositionExist = await _context.Positions.AnyAsync(x => x.Id == vm.PositionId);
        if (!isPositionExist)
        {
            return NotFound();
        }

        //Check image size
        if (!vm.Image.CheckSize(2))
        {
            ModelState.AddModelError("Image", "Image size  must be less than 2 mb");
            return View(vm);
        }
        //Check file type
        if (!vm.Image.CheckType("image"))
        {
            ModelState.AddModelError("Image", "File must be image type");
            return View(vm);
        }

        //Create image

        string uniqueFileName = await vm.Image.FileUploadToAsync(_folderPath);

        //Create chef
        Chef chef = new()
        {
            Name=vm.Name,
            PositionId=vm.PositionId,
            ImagePath=uniqueFileName
        };
        await _context.Chefs.AddAsync(chef);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");

    }

    public async Task<IActionResult> Update(int id)
    {
        var chef = await _context.Chefs.FindAsync(id);
        if (chef is null)
            return NotFound();

        ChefUpdateVM vm = new() 
        { 
            
             
        };

        return View(vm);
    }

    private async Task SendPositionWithViewBag()
    {
        var position = await _context.Positions.ToListAsync();
        ViewBag.Positions = position;
    }
}
