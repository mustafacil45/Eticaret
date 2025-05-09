using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Eticaret.WebUI.Models;
using Eticaret.Data;
using Microsoft.EntityFrameworkCore;
using Eticaret.Core.Entities;

namespace Eticaret.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly DataBaseContext _context;

    public HomeController(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var model = new HomePageViewModel()
        {
            Sliders = await _context.Sliders.ToListAsync(),
            News = await _context.News.ToListAsync(),
            Products = await _context.Products.Where(p=>p.IsActive && p.IsHome).ToListAsync(),
        };
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult ContactUs()
    {
        return View();
    }
    [HttpPost]
    public IActionResult ContactUs(Contact contact)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Contacts.Add(contact);
                var sonuc = _context.SaveChanges();
                if(sonuc > 0)
                {
                    TempData["Message"] = "<div class = 'alert alert-success'>Mesaj�n�z G�nderilmi�tir!</div>";
                    return RedirectToAction("ContactUs");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Hata Olu�tu!");
            }
        }
        return View(contact);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
