using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReaderyMVC.Data;
using ReaderyMVC.Models;

namespace ReaderyMVC.Controllers;

public class EditarController : Controller
{
    private readonly AppDbContext _context;

    public EditarController(AppDbContext context)
    {
        _context = context;
    }

    
}
