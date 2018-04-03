using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageGallery.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.Controllers
{
    public class RecipesController : Controller
    {
	    private readonly ImageGalleryDbContext _context;

	    public RecipesController(ImageGalleryDbContext context)
	    {
		    _context = context;
	    }

        public IActionResult Index()
        {
	        return View(_context.Images.ToList());
        }
    }
}